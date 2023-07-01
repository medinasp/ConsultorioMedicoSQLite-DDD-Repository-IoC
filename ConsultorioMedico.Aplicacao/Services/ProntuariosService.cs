using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.ViewModels;
using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Aplicacao.Services
{
    public class ProntuariosService : IProntuariosService
    {
        private readonly ICadPacientesService _cadPacienteService;
        private readonly ICadMedicosService _cadMedicoService;
        private static readonly List<Prontuarios> _prontuariosList = new List<Prontuarios>();

        public ProntuariosService(ICadPacientesService cadPacientesService, ICadMedicosService cadMedicosService)
        {
            _cadPacienteService = cadPacientesService;
            _cadMedicoService = cadMedicosService;
        }

        public async Task<ProntuariosViewModel> CriarProntuarioPorId(ProntuariosInputModel model)
        {
            var prontuarios = model.ToEntity();
            _prontuariosList.Add(prontuarios);

            var pacientes = await _cadPacienteService.GetAll();
            var medicos = await _cadMedicoService.GetAll();

            var pacienteViewModel = pacientes.FirstOrDefault(p => p.Id == model.Paciente.Id);
            var medicoViewModel = medicos.FirstOrDefault(m => m.Id == model.Medico.Id);

            if (pacienteViewModel == null || medicoViewModel == null)
            {
                return null; // Tratar o caso em que o paciente ou médico não foi encontrado
            }

            var paciente = new CadPacientes(pacienteViewModel.Nome, pacienteViewModel.CPF);
            var medico = new CadMedicos(medicoViewModel.Nome, medicoViewModel.CPF, medicoViewModel.Especialidade);

            var prontuario = new Prontuarios(medico, paciente, model.TextoProntuario);

            var prontuarioViewModel = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdPaciente = paciente.Id,
                NomePaciente = paciente.Nome,
                CPFPaciente = paciente.CPF,
                IdMedico = medico.Id,
                NomeMedico = medico.Nome,
                CPFMedico = medico.CPF,
                EspecialidadeMedico = medico.Especialidade,
                TextoProntuario = prontuario.TextoProntuario
            };

            return prontuarioViewModel;
        }

        public async Task<ProntuariosViewModel> CriarProntuarioPorNome(ProntuariosInputModel model)
        {
            var prontuarios = model.ToEntity();
            _prontuariosList.Add(prontuarios);

            var pacientes = await _cadPacienteService.GetAll();
            var medicos = await _cadMedicoService.GetAll();

            var pacienteViewModel = pacientes.FirstOrDefault(p => p.Nome == model.Paciente.Nome);
            var medicoViewModel = medicos.FirstOrDefault(m => m.Nome == model.Medico.Nome);

            if (pacienteViewModel == null || medicoViewModel == null)
            {
                return null; // Tratar o caso em que o paciente ou médico não foi encontrado
            }

            var paciente = new CadPacientes(pacienteViewModel.Nome, pacienteViewModel.CPF);
            var medico = new CadMedicos(medicoViewModel.Nome, medicoViewModel.CPF, medicoViewModel.Especialidade);

            var prontuario = new Prontuarios(medico, paciente, model.TextoProntuario);

            var prontuarioViewModel = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdPaciente = paciente.Id,
                NomePaciente = paciente.Nome,
                CPFPaciente = paciente.CPF,
                IdMedico = medico.Id,
                NomeMedico = medico.Nome,
                CPFMedico = medico.CPF,
                EspecialidadeMedico = medico.Especialidade,
                TextoProntuario = prontuario.TextoProntuario
            };

            return prontuarioViewModel;
        }

        public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomeMedico(string nomeMedico)
        {
            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Medico.Nome == nomeMedico);

            if (prontuarios == null)
            {
                return null;
            }

            var prontuariosViewModel = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdMedico = prontuarios.Medico.Id,
                NomeMedico = prontuarios.Medico.Nome,
                EspecialidadeMedico = prontuarios.Medico.Especialidade,
                IdPaciente = prontuarios.Paciente.Id,
                NomePaciente = prontuarios.Paciente.Nome,
                TextoProntuario = prontuarios.TextoProntuario
            };

            return await Task.FromResult(prontuariosViewModel);
        }

        public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomePaciente(string nomePaciente)
        {
            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Paciente.Nome == nomePaciente);

            if (prontuarios == null)
            {
                return null;
            }

            var prontuariosViewModel = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdMedico = prontuarios.Medico.Id,
                NomeMedico = prontuarios.Medico.Nome,
                EspecialidadeMedico = prontuarios.Medico.Especialidade,
                IdPaciente = prontuarios.Paciente.Id,
                NomePaciente = prontuarios.Paciente.Nome,
                TextoProntuario = prontuarios.TextoProntuario
            };

            return await Task.FromResult(prontuariosViewModel);
        }
 
        public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomeMedicoAtivos(string nomeMedico)
        {
            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Medico.Nome == nomeMedico && p.Ativo == true);

            if (prontuarios == null)
            {
                return null;
            }

            var prontuariosViewModel = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdMedico = prontuarios.Medico.Id,
                NomeMedico = prontuarios.Medico.Nome,
                EspecialidadeMedico = prontuarios.Medico.Especialidade,
                IdPaciente = prontuarios.Paciente.Id,
                NomePaciente = prontuarios.Paciente.Nome,
                TextoProntuario = prontuarios.TextoProntuario
            };

            return await Task.FromResult(prontuariosViewModel);
        }

        public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomePacienteAtivos(string nomePaciente)
        {
            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Paciente.Nome == nomePaciente && p.Ativo == true);

            if (prontuarios == null)
            {
                return null;
            }

            var prontuariosViewModel = new ProntuariosViewModel
            {
                Id = prontuarios.Id,
                IdMedico = prontuarios.Medico.Id,
                NomeMedico = prontuarios.Medico.Nome,
                EspecialidadeMedico = prontuarios.Medico.Especialidade,
                IdPaciente = prontuarios.Paciente.Id,
                NomePaciente = prontuarios.Paciente.Nome,
                TextoProntuario = prontuarios.TextoProntuario
            };

            return await Task.FromResult(prontuariosViewModel);
        }

        public async Task<bool> EditarProntuario(string id, ProntuariosInputModel model)
        {
            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Id.ToString() == id);

            if (prontuarios == null)
            {
                return false;
            }

            prontuarios.Update(model.TextoProntuario);

            return true;
        }

        public async Task<bool> RemoverProntuarioSoft(string id)
        {
            if(!Guid.TryParse(id, out var guid))
                return false;

            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Id == guid && p.Ativo);

            if(prontuarios == null)
                return false;

            prontuarios.Excluir();

            return true;
        }

        public async Task<bool> RemoverProntuarioHard(string id)
        {
            var prontuarios = _prontuariosList.FirstOrDefault(p => p.Id.ToString() == id);

            if (prontuarios == null)
                return false;

            _prontuariosList.Remove(prontuarios);

            return true;
        }

    }
}
