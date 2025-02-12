using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Models;

namespace AdR.Repositories
{
    public class NotaRepository(AdrDbContext context) : INotaRepository
    {
        readonly AdrDbContext db = context;
        public int CreateNota(Nota nota)
        {
            db.Add(nota);
            return db.SaveChanges();
        }

        public List<Nota> ReadNota(IList<int> numeros)
        {
            return db.Nota.Where(nota => numeros.Contains(nota.Numero)).ToList();
        }
    }
}
