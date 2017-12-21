namespace Data
{
    public interface IFilmeRepository
    {
        bool ChecaDisponibilidade(int filmeId);

        bool Reservar(int filmeId, int clienteId);

        void Devolver(int filmeId);
    }
}