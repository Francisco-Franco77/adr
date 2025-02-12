namespace AdR.Models
{
    public class MensagemServiceResult(bool sucesso, string mensagem)
    {
        public bool Sucesso { get; set; } = sucesso;
        public string Mensagem { get; set; } = mensagem;
    }
}
