using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Infra.InterfacesRepositories
{
    public interface ICadMedicosRepository
    {
        Task Add(CadMedicos cadMedico);
        Task<CadMedicos> GetByCode(string code);
        Task<List<CadMedicos>> GetByName(string name);
        Task<IEnumerable<CadMedicos>> GetAll();
        Task Update(CadMedicos cadMedico);
        Task<IEnumerable<CadMedicos>> GetActives();
        Task<bool> SoftDelete(CadMedicos cadMedico);
        Task HardDelete(CadMedicos cadMedico);
    }
}
