using AdR.Models.Enums;

namespace AdR.Models
{
    public class Empresa(string cnpj, string nome, decimal faturamento, Ramo ramo)
    {
        public int Id { get; set; }
        public string? Cnpj { get; set; } = cnpj;
        public string? Nome { get; set; } = nome;
        public decimal Faturamento { get; set; } = faturamento;
        public Ramo Ramo { get; set; } = ramo;
    }
}
