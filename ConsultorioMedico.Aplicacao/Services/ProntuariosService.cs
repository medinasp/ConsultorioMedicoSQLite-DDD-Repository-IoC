using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.ViewModels;
using ConsultorioMedico.Infra.InterfacesRepositories;

namespace ConsultorioMedico.Aplicacao.Services
{
    public class ProntuariosService : IProntuariosService
    {
        private readonly IProntuariosRepository _prontuariosRepository;

        public ProntuariosService(IProntuariosRepository prontuariosRepository)
        {
            _prontuariosRepository = prontuariosRepository;
        }

        public async Task<ProntuariosViewModel> ConsultaPorCodigo(string code)
        {
            if (!Guid.TryParse(code, out var guid))
                return null;

            var prontuarios = await _prontuariosRepository.GetById(guid.ToString());

            if (prontuarios == null)
                return null;

            var prontuariosViewModelList = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdPaciente = prontuarios.PacienteId,
                NomePaciente = prontuarios.PacienteNome,
                IdMedico = prontuarios.MedicoId,
                NomeMedico = prontuarios.MedicoNome,
                MedicoEspecialidade = prontuarios.MedicoEspecialidade,
                TextoProntuario = prontuarios.TextoProntuario
            };

            return prontuariosViewModelList;
        }

        public async Task<List<ProntuariosViewModel>> ConsultarProntuarioPorNomeMedico(string nomeMedico)
        {
            var prontuariosList = await _prontuariosRepository.GetByNomeMedico(nomeMedico);

            if (prontuariosList == null)
                return null;

            var prontuariosViewModelList = prontuariosList.Select(m => new ProntuariosViewModel
            {
                Id = m.Id,
                IdPaciente = m.PacienteId,
                NomePaciente = m.PacienteNome,
                IdMedico = m.MedicoId,
                NomeMedico = m.MedicoNome,
                MedicoEspecialidade = m.MedicoEspecialidade,
                TextoProntuario = m.TextoProntuario
            }).ToList();

            return prontuariosViewModelList;
        }

        public async Task<List<ProntuariosViewModel>> ConsultarProntuarioPorNomePaciente(string nomePaciente)
        {
            var prontuariosList = await _prontuariosRepository.GetByNomePaciente(nomePaciente);

            if (prontuariosList == null)
                return null;

            var prontuariosViewModelList = prontuariosList.Select(m => new ProntuariosViewModel
            {
                Id = m.Id,
                IdPaciente = m.PacienteId,
                NomePaciente = m.PacienteNome,
                IdMedico = m.MedicoId,
                NomeMedico = m.MedicoNome,
                MedicoEspecialidade = m.MedicoEspecialidade,
                TextoProntuario = m.TextoProntuario
            }).ToList();

            return prontuariosViewModelList;
        }

        public async Task<ProntuariosViewModel> CriarProntuarioPorId(ProntuariosInputModel model)
        {
            var prontuario = model.ToEntity();
            var cadProntList = await _prontuariosRepository.Add(prontuario);

            if (cadProntList == null)
                return null;

            var viewModel = new ProntuariosViewModel
            {
                Id = prontuario.Id,
                IdPaciente = prontuario.PacienteId,
                NomePaciente = prontuario.PacienteNome,
                IdMedico = prontuario.MedicoId,
                NomeMedico = prontuario.MedicoNome,
                MedicoEspecialidade = prontuario.MedicoEspecialidade,
                TextoProntuario = prontuario.TextoProntuario
            };

            return viewModel;
        }

        public async Task<bool> EditarProntuario(string id, ProntuariosInputModel model)
        {
            if (!Guid.TryParse(id, out var guid)) return false;

            var prontuario = await _prontuariosRepository.GetById(guid.ToString());

            if (prontuario == null) return false;

            prontuario.Update(model.TextoProntuario);

            await _prontuariosRepository.Update(prontuario);

            return true;
        }

        public async Task<IEnumerable<ProntuariosViewModel>> GetAll()
        {
            var getAllProntuarios = await _prontuariosRepository.GetAllActives();

            var viewModels = getAllProntuarios.Select(m => new ProntuariosViewModel
            {
                Id = m.Id,
                IdPaciente = m.PacienteId,
                NomePaciente = m.PacienteNome,
                IdMedico = m.MedicoId,
                NomeMedico = m.MedicoNome,
                MedicoEspecialidade = m.MedicoEspecialidade,
                TextoProntuario = m.TextoProntuario
            });

            return viewModels;
        }

        public async Task<bool> RemoverProntuarioHard(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return false;

            var prontuarios = await _prontuariosRepository.GetById(guid.ToString());

            if (prontuarios == null)
                return false;

            await _prontuariosRepository.HardDelete(prontuarios);

            return true;
        }

        public async Task<bool> RemoverProntuarioSoft(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return false;

            var prontuarios = await _prontuariosRepository.GetById(guid.ToString());

            if (prontuarios == null || !prontuarios.Ativo)
                return false;

            await _prontuariosRepository.SoftDelete(prontuarios);

            return true;
        }
    }
}