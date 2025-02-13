using AdR.Models;

namespace AdR.Interfaces
{
    public interface INotaRepository
    {
        MensagemServiceResult CreateNota(Nota nota);

        MensagemServiceResult EditNota(int numero, decimal? valor = null, DateOnly? data = null);

        MensagemServiceResult DeleteNota(int numero);

        List<Nota> ReadNota(IList<int> numeros);
    }
}
