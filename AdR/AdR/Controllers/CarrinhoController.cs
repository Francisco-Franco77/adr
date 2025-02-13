using AdR.Models;
using AdR.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdR.Controllers
{
    /// <summary>
    /// Responsável por operações de edição do carrinho e pela função de checkout
    /// </summary>
    /// <param name="carrinhoService"></param>
    [ApiController]
    [Route("Api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CarrinhoController([FromServices] CarrinhoService carrinhoService) : ControllerBase
    {
        /// <summary>
        /// Adiciona itens no carrinho, criando um arquivo de carrinho para a empresa se não houver
        /// </summary>
        /// <param name="empresaId">Identidificador da Empresa</param>
        /// <param name="notas">Numeros das notas a serem adicionadas no carrinho</param>
        /// <returns></returns>
        [HttpPost("Carrinho")]
        public IActionResult AdicionarCarrinho(int empresaId, int[] notas)
        {
            MensagemServiceResult result = carrinhoService.CreateCarrinhoFile(empresaId, notas);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        /// <summary>
        /// Retorna os itens contidos no carrinho da empresa
        /// </summary>
        /// <param name="empresaId">Identidificador da Empresa</param>
        /// <returns></returns>
        [HttpGet("Carrinho")]
        public IActionResult VerificarCarrinho(int empresaId)
        {
            MensagemServiceResult result = carrinhoService.CheckCarrinhoFile(empresaId);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        /// <summary>
        /// Remove itens do carrinho
        /// </summary>
        /// <param name="empresaId">Identidificador da Empresa</param>
        /// <param name="notas">Numeros das notas a serem removidos do carrinho</param>
        /// <returns></returns>
        [HttpDelete("Carrinho")]
        public IActionResult RemoverCarrinho(int empresaId, int[] notas)
        {
            MensagemServiceResult result = carrinhoService.DeleteCarrinhoItem(empresaId, notas);
            if (result.Sucesso)
                return Ok(result.Mensagem);
            else
                return BadRequest(result.Mensagem);
        }

        /// <summary>
        /// Realiza o processo de checkout
        /// </summary>
        /// <param name="empresaId">Identidificador da Empresa</param>
        /// <returns></returns>
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
