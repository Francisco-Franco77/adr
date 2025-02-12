using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Models;
using AdR.Models.Enums;
using AdR.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdR.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CadastroController([FromServices] CadastroService cadastroService) : ControllerBase
    {
        [HttpPost("Empresa")]
        public IActionResult PostEmpresa(string cnpj, string nome, decimal faturamento, Ramo ramo)
        {
            Empresa empresa = new(cnpj, nome, faturamento, ramo);
            MensagemServiceResult result = cadastroService.CadastroEmpresa(empresa);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        [HttpPost]
        [Route("Nota")]
        public IActionResult PostNota(int numero, decimal valor, DateOnly data, int idEmpresa)
        {
            Nota nota = new(numero, valor, data, idEmpresa);
            MensagemServiceResult result = cadastroService.CadastroNota(nota);
            if(result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }
    }
}
