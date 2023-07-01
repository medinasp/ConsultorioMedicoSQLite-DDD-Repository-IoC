using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.ViewModels;

namespace ConsultorioMedico.Aplicacao.InterfacesServices
{
    public interface IProntuariosService
    {
        Task<ProntuariosViewModel> CriarProntuarioPorId(ProntuariosInputModel model);
        Task<ProntuariosViewModel> CriarProntuarioPorNome(ProntuariosInputModel model);
        Task<ProntuariosViewModel> ConsultarProntuarioPorNomeMedico(string nomeMedico);
        Task<ProntuariosViewModel> ConsultarProntuarioPorNomeMedicoAtivos(string nomeMedico);
        Task<ProntuariosViewModel> ConsultarProntuarioPorNomePaciente(string nomePaciente);
        Task<ProntuariosViewModel> ConsultarProntuarioPorNomePacienteAtivos(string nomePaciente);
        Task<bool> EditarProntuario(string id, ProntuariosInputModel model);
        Task<bool> RemoverProntuarioSoft(string id);
        Task<bool> RemoverProntuarioHard(string id);

    }
}
