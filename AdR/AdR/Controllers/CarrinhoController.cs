using AdR.Models;
using AdR.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdR.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CarrinhoController([FromServices] CarrinhoService carrinhoService) : ControllerBase
    {
        [HttpPost("Carrinho")]
        public IActionResult AdicionarCarrinho(int empresaId, int[] notas)
        {
            MensagemServiceResult result = carrinhoService.CreateCarrinhoFile(empresaId, notas);
            if(result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        [HttpGet("Carrinho")]
        public IActionResult VerificarCarrinho(int empresaId)
        {
            MensagemServiceResult result = carrinhoService.CheckCarrinhoFile(empresaId);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        [HttpDelete("Carrinho")]
        public IActionResult RemoverCarrinho(int empresaId, int[] notas)
        {
            MensagemServiceResult result = carrinhoService.DeleteCarrinhoItem(empresaId, notas);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        [HttpGet("Checkout")]
        public IActionResult Checkout(int empresaId)
        {
            MensagemServiceResult result = carrinhoService.Checkout(empresaId);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }
    }
}
