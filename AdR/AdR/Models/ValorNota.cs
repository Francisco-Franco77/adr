namespace AdR.Models
{
    public class ValorNota(int numero, decimal bruto, decimal liquido)
    {
        public int Numero { get; set; } = numero;
        public decimal ValorBruto { get; set; } = bruto;
        public decimal ValorLiquido { get; set; } = liquido;
    }
}
