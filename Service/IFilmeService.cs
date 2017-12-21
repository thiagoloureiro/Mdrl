namespace Service
{
    public interface IFilmeService
    {
        bool ChecaDisponibilidade(int filmeId);

        bool Reservar(int filmeId, int clienteId);
    }
}