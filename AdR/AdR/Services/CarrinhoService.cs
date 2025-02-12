using AdR.Interfaces;
using AdR.Models;
using AdR.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace AdR.Services
{
    public class CarrinhoService([FromServices] IEmpresaRepository empresaRepository, INotaRepository notaRepository)
    {
        public MensagemServiceResult CreateCarrinhoFile(int empresaId, int[] notas)
        {
            string carrinhoPath = this.FormCarrinhoPath(empresaId);

            try
            {
                using (StreamWriter writer = File.AppendText(carrinhoPath))
                {
                    foreach (int nota in notas)
                    {
                        writer.WriteLine(nota);
                    }
                }
                return new MensagemServiceResult(true, "Carrinho atualizado em: "+empresaId+".txt");
            }
            catch
            {
                return new MensagemServiceResult(false, "Carrinho não pode ser criado ou atualizado");
            }
        }

        public MensagemServiceResult CheckCarrinhoFile(int empresaId)
        {
            string carrinhoPath = this.FormCarrinhoPath(empresaId);
            IList<int> ids = [];
            try
            {
                using (FileStream fileStream = File.OpenRead(carrinhoPath))
                {
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            ids = ids.Append<int>(int.Parse(line)).ToArray();
                        }
                    }
                }
                string mensagem = JsonConvert.SerializeObject(new
                {
                    notas_carrinho = ids
                });
                return new MensagemServiceResult(true, mensagem);
            }
            catch
            {
                return new MensagemServiceResult(false, "Carrinho não encontrado");
            }
        }

        public MensagemServiceResult DeleteCarrinhoItem(int empresaId, int[] notasDeletadas)
        {
            string carrinhoPath = this.FormCarrinhoPath(empresaId);
            IList<int> notasMantidas = [];
            try
            {
                using (FileStream fileStream = File.OpenRead(carrinhoPath))
                {
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!notasDeletadas.Contains(int.Parse(line)))
                            {
                                notasMantidas = notasMantidas.Append<int>(int.Parse(line)).ToArray();
                            }
                        }
                    }
                }
                File.WriteAllText(carrinhoPath,string.Empty);
            }
            catch
            {
                return new MensagemServiceResult(false, "Carrinho não encontrado");
            }

            try
            {
                using (StreamWriter writer = File.AppendText(carrinhoPath))
                {
                    foreach (int nota in notasMantidas)
                    {
                        writer.WriteLine(nota);
                    }
                }
                return new MensagemServiceResult(true, "Itens foram deletados do carrinho");
            }
            catch
            {
                return new MensagemServiceResult(false, "Itens foram deletados mas carrinho não pode ser recriado");
            }
        }

        public MensagemServiceResult Checkout(int empresaId)
        {
            string carrinhoPath = this.FormCarrinhoPath(empresaId);
            IList<int> notaIds = [];
            Empresa empresa = empresaRepository.GetEmpresa(empresaId);
            if (empresa == null)
                return new MensagemServiceResult(false, "Empresa não encontrada");

            try
            {
                using (FileStream fileStream = File.OpenRead(carrinhoPath))
                {
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            notaIds = notaIds.Append<int>(int.Parse(line)).ToArray();
                        }
                    }
                }
            }
            catch
            {
                return new MensagemServiceResult(false, "Carrinho não encontrado");
            }

            List<Nota> notas = notaRepository.ReadNota(notaIds);
            List<ValorNota> respostaNotas = [];
            decimal limiteCredito = this.CalculaLimiteCredito(empresa.Faturamento, empresa.Ramo);
            decimal valorTotal = 0, valorLiquidoTotal = 0;
            foreach(Nota nota in notas)
            {
                decimal valorLiquido = this.CalculaValorLiquido(nota.DataVencimento, nota.Valor);
                valorTotal += nota.Valor;
                valorLiquidoTotal += valorLiquido;
                respostaNotas.Add(new(nota.Numero, nota.Valor, valorLiquido));
            }

            if (limiteCredito < valorTotal)
            {
                string mensagemErro = JsonConvert.SerializeObject(new
                {
                    empresa = empresa.Nome,
                    cnpj = empresa.Cnpj,
                    limite = limiteCredito,
                    checkout = false,
                    mensagem = "Checkout recusado. Valor total "+valorTotal+" é maior que seu limite de crédito."
                });
                return new MensagemServiceResult(false, mensagemErro);
            }
            string mensagem = JsonConvert.SerializeObject(new
            {
                empresa = empresa.Nome,
                cnpj = empresa.Cnpj,
                limite = limiteCredito,
                notas_fiscais = respostaNotas,
                total_liquido = valorLiquidoTotal,
                total_bruto = valorTotal,
                checkout = true
            });
            return new MensagemServiceResult(false, mensagem);
        }

        private string FormCarrinhoPath(int empresaId)
        {
            string directory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(directory, "Carrinhos");
            string filename = empresaId + ".txt";
            return Path.Combine(filePath, filename);
        }

        private decimal CalculaLimiteCredito(decimal faturamento, Ramo ramo)
        {
            const decimal Margen_Geral_1 = 0.50M;
            const decimal Margen_Servicos_2 = 0.55M;
            const decimal Margen_Produtos_2 = 0.60M;
            const decimal Margen_Servicos_3 = 0.60M;
            const decimal Margen_Produtos_3 = 0.65M;

            if (faturamento < 10000)
            {
                return 0;
            }
            else if (faturamento >= 10000 && faturamento <= 50000)
            {
                return faturamento * Margen_Geral_1;
            }
            else if (faturamento > 50000 && faturamento <= 100000)
            {
                if (ramo == Ramo.Servicos)
                    return faturamento * Margen_Servicos_2;
                else
                    return faturamento * Margen_Produtos_2;
            }
            else
            {
                if (ramo == Ramo.Servicos)
                    return faturamento * Margen_Servicos_3;
                else
                    return faturamento * Margen_Produtos_3;
            }
        }

        private decimal CalculaValorLiquido(DateOnly vencimento, decimal valorBruto)
        {
            const double taxa = 0.0465;
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            double prazo = vencimento.DayNumber - dataAtual.DayNumber;
            decimal valorLiquido = valorBruto/(decimal)Math.Pow((1 + taxa), (prazo / 30));
            return Math.Round(valorLiquido,2);
        }
    }
}
