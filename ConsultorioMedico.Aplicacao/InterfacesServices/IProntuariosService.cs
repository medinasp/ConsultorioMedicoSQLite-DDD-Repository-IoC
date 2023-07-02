using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.ViewModels;

namespace ConsultorioMedico.Aplicacao.InterfacesServices
{
    public interface IProntuariosService
    {

        Task<IEnumerable<ProntuariosViewModel>> GetAll();
        Task<List<ProntuariosViewModel>> CriarProntuarioPorId(ProntuariosInputModel model);
        Task<ProntuariosViewModel> ConsultaPorCodigo(string code);
        Task<List<ProntuariosViewModel>> ConsultarProntuarioPorNomeMedico(string nomeMedico);
        Task<List<ProntuariosViewModel>> ConsultarProntuarioPorNomePaciente(string nomePaciente);
        Task<bool> EditarProntuario(string id, ProntuariosInputModel model);
        Task<bool> RemoverProntuarioSoft(string id);
        Task<bool> RemoverProntuarioHard(string id);


        //Task<IEnumerable<ProntuariosViewModel>> GetAll();
        //Task<List<ProntuariosViewModel>> CriarProntuarioPorId(ProntuariosInputModel model);
        //Task<ProntuariosViewModel> ConsultaPorCodigo(string code);
        //Task<List<ProntuariosViewModel>> ConsultarProntuarioPorNomeMedico(string nomeMedico);
        //Task<List<ProntuariosViewModel>> ConsultarProntuarioPorNomePaciente(string nomePaciente);
        //Task<bool> EditarProntuario(string id, ProntuariosInputModel model);
        //Task<bool> RemoverProntuarioSoft(string id);
        //Task<bool> RemoverProntuarioHard(string id);

    }
}
