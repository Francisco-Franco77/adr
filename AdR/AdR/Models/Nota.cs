namespace AdR.Models
{
    public class Nota(int numero, decimal valor, DateOnly dataVencimento, int empresaId)
    {
        public int Id { get; set; }
        public int Numero { get; set; } = numero;
        public decimal Valor { get; set; } = valor;
        public DateOnly DataVencimento { get; set; } = dataVencimento;
        public int EmpresaId { get; set; } = empresaId;
    }
}
