using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.ViewModels;

namespace ConsultorioMedico.Aplicacao.InterfacesServices
{
    public interface ICadPacientesService
    {
        Task<CadPacientesViewModel> Add(CadPacientesInputModel model);
        Task<CadPacientesViewModel> GetByCode(string code);
        Task<IEnumerable<CadPacientesViewModel>> GetAll();
        Task<bool> Update(string id, CadPacientesInputModel model);
        Task<bool> SoftDelete(string id);
        Task<IEnumerable<CadPacientesViewModel>> GetActives();
        Task<bool> HardDelete(string id);
    }
}
