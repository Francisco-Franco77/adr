using AdR.Models;

namespace AdR.Interfaces
{
    public interface INotaRepository
    {
        int CreateNota(Nota nota);

        List<Nota> ReadNota(IList<int> numeros);
    }
}
