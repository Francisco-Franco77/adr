using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Models;

namespace AdR.Repositories
{
    public class NotaRepository(AdrDbContext context) : INotaRepository
    {
        readonly AdrDbContext db = context;
        public MensagemServiceResult CreateNota(Nota nota)
        {
            try
            {
                db.Add(nota);
                db.SaveChanges();
                return new MensagemServiceResult(true, "Nota de número: " + nota.Numero + " cadastrada");
            }
            catch (Exception ex)
            {
                return new MensagemServiceResult(false, "Nota de número: " + nota.Numero + " não pode ser cadastrada. Erro: " + ex.Message);
            }
        }

        public MensagemServiceResult EditNota(int numero, decimal? valor = null, DateOnly? data = null)
        {
            try
            {
                List<Nota> oldNota = this.ReadNota([numero]);
                oldNota.First().Valor = (valor != null) ? (decimal)valor : oldNota.First().Valor;
                oldNota.First().DataVencimento = (data != null) ? (DateOnly)data : oldNota.First().DataVencimento;
                db.Nota.Update(oldNota.First());
                db.SaveChanges();
                return new MensagemServiceResult(true, "Nota de número: " + numero + " atualizada");
            }
            catch (Exception ex)
            {
                return new MensagemServiceResult(false, "Não foi possível atualizar a nota. Erro: " + ex.Message);
            }

        }

        public MensagemServiceResult DeleteNota(int numero)
        {
            try
            {
                List<Nota> nota = this.ReadNota([numero]);
                db.Nota.Remove(nota.First());
                db.SaveChanges();
                return new MensagemServiceResult(true, "Nota de número: " + numero + " removida");
            }
            catch (Exception ex)
            {
                return new MensagemServiceResult(false, "Não foi possível remover a nota. Erro: " + ex.Message);
            }
        }

        public List<Nota> ReadNota(IList<int> numeros)
        {
            return db.Nota.Where(nota => numeros.Contains(nota.Numero)).ToList();
        }
    }
}
