using AdR.Models;

namespace AdR.Interfaces
{
    public interface IEmpresaRepository
    {
        MensagemServiceResult CreateEmpresa(Empresa empresa);

        Empresa GetEmpresa(int id);
    }
}
