using AdR.Models;
using AdR.Models.Enums;
using AdR.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdR.Controllers
{
    /// <summary>
    /// Responsável por funções de criaçao e edição dos dados de empresas e de notas fiscais
    /// </summary>
    /// <param name="cadastroService"></param>
    [ApiController]
    [Route("Api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CadastroController([FromServices] CadastroService cadastroService) : ControllerBase
    {
        /// <summary>
        /// Realiza cadastro de empresa
        /// </summary>
        /// <param name="cnpj">CNPJ da empresa</param>
        /// <param name="nome">Nome da empresa</param>
        /// <param name="faturamento">Valor de faturamento mensal da empresa</param>
        /// <param name="ramo">Ramo da empresa:
        /// 1- Serviços
        /// 2- Produtos</param>
        /// <returns></returns>
        [HttpPost("Empresa")]
        public IActionResult CreateEmpresa(string cnpj, string nome, decimal faturamento, Ramo ramo)
        {
            Empresa empresa = new(cnpj, nome, faturamento, ramo);
            MensagemServiceResult result = cadastroService.CadastroEmpresa(empresa);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        /// <summary>
        /// Realiza cadastro de uma nota fiscal
        /// </summary>
        /// <param name="numero">Número da nota</param>
        /// <param name="valor">Valor da nota</param>
        /// <param name="data">Data de vencimento da nota em formato YYYY-MM-DD</param>
        /// <param name="idEmpresa">Identidificador da Empresa criadora da nota na tabela</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Nota")]
        public IActionResult CreateNota(int numero, decimal valor, DateOnly data, int idEmpresa)
        {
            Nota nota = new(numero, valor, data, idEmpresa);
            MensagemServiceResult result = cadastroService.CadastroNota(nota);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        /// <summary>
        /// Edita valor ou data de uma nota
        /// </summary>
        /// <param name="numero">Número da nota a ser editada</param>
        /// <param name="valor">Novo valor desejado para a nota (deixar em branco se deseja manter o mesmo)</param>
        /// <param name="data">Nova data de vencimento desejada para a nota (deixar em branco se deseja manter a mesma)</param>
        /// <returns></returns>
        [HttpPut("Nota")]
        public IActionResult EditNota(int numero, decimal? valor = null, DateOnly? data = null)
        {
            MensagemServiceResult result = cadastroService.EditNota(numero, valor, data);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        /// <summary>
        /// Remove uma nota cadastrada do banco
        /// </summary>
        /// <param name="numero">Número da nota a ser removida</param>
        /// <returns></returns>
        [HttpDelete("Nota")]
        public IActionResult DeleteNota(int numero)
        {
            MensagemServiceResult result = cadastroService.DeleteNota(numero);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }
    }
}
