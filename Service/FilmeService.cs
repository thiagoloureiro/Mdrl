using Data;

namespace Service
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public bool ChecaDisponibilidade(int filmeId)
        {
            return _filmeRepository.ChecaDisponibilidade(filmeId);
        }

        public bool Reservar(int filmeId, int clienteId)
        {
            return _filmeRepository.Reservar(filmeId, clienteId);
        }

        public void Devolver(int filmeId)
        {
            _filmeRepository.Devolver(filmeId);
        }
    }
}