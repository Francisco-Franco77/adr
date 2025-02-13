using AdR.DatabaseContext;
using AdR.Interfaces;
using AdR.Models;

namespace AdR.Repositories
{
    public class EmpresaRepository(AdrDbContext db) : IEmpresaRepository
    {
        public MensagemServiceResult CreateEmpresa(Empresa empresa)
        {
            try
            {
                db.Add(empresa);
                db.SaveChanges();
                return new MensagemServiceResult(true, "Empresa de cnpj: " + empresa.Cnpj + " cadastrada com id: " + empresa.Id);
            }
            catch (Exception ex)
            {
                return new MensagemServiceResult(false, "Empresa não pode ser cadastrada. Erro: " + ex.Message);
            }
        }

        public Empresa GetEmpresa(int id)
        {
            return db.Find<Empresa>(id);
        }
    }
}
