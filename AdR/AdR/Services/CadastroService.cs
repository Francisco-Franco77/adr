using AdR.Interfaces;
using AdR.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdR.Services
{
    public class CadastroService([FromServices] IEmpresaRepository empresaRepository, INotaRepository notaRepository)
    {
        public MensagemServiceResult CadastroEmpresa(Empresa empresa)
        {
            return empresaRepository.CreateEmpresa(empresa);
        }
        public MensagemServiceResult CadastroNota(Nota nota)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            if (nota.DataVencimento.DayNumber < dataAtual.DayNumber)
                return new MensagemServiceResult(false, "Data de vencimento da nota é anterior à data atual");
            return notaRepository.CreateNota(nota);
        }
        public MensagemServiceResult EditNota(int numero, decimal? valor = null, DateOnly? data = null)
        {
            if (data != null)
            {
                DateOnly dataNova = (DateOnly)data;
                DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
                if (dataNova.DayNumber < dataAtual.DayNumber)
                    return new MensagemServiceResult(false, "Data de vencimento da nota é anterior à data atual");
            }

            return notaRepository.EditNota(numero, valor, data);
        }
        public MensagemServiceResult DeleteNota(int numero)
        {
            return notaRepository.DeleteNota(numero);
        }
    }
}
