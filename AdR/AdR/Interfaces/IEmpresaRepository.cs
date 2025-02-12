using AdR.Models;

namespace AdR.Interfaces
{
    public interface IEmpresaRepository
    {
        int CreateEmpresa(Empresa empresa);

        Empresa GetEmpresa(int id);
    }
}
