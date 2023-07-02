using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Infra.Repositories
{
    public interface ICadPacientesRepository
    {
        Task Add(CadPacientes cadPacientes);
        Task<CadPacientes> GetByCode(string code);
        Task<IEnumerable<CadPacientes>> GetAll();
        Task Update(CadPacientes cadPacientes);
        Task<IEnumerable<CadPacientes>> GetActives();
        Task<bool> SoftDelete(CadPacientes cadPacientes);
        Task HardDelete(CadPacientes cadPacientes);
    }
}
