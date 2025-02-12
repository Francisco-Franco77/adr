using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Models;

namespace AdR.Repositories
{
    public class EmpresaRepository(AdrDbContext db) : IEmpresaRepository
    {
        public int CreateEmpresa(Empresa empresa)
        {
            db.Add(empresa);
            return db.SaveChanges();
        }

        public Empresa GetEmpresa(int id)
        {
            return db.Find<Empresa>(id);
        }
    }
}
