using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Infra.InterfacesRepositories
{
    public interface IProntuariosRepository
    {
        Task<Prontuarios> GetById(string code);
        Task<IEnumerable<Prontuarios>> GetAllActives();
        Task<List<Prontuarios>> GetByNomeMedico(string name);
        Task<List<Prontuarios>> GetByNomePaciente(string name);
        Task<List<Prontuarios>> Add(Prontuarios prontuario);
        Task Update(Prontuarios prontuario);
        Task<bool> SoftDelete(Prontuarios prontuario);
        Task HardDelete(Prontuarios prontuario);
    }
}
