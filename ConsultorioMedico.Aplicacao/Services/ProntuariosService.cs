using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.ViewModels;
using ConsultorioMedico.Dominio.Entities;
using ConsultorioMedico.Infra.Repositories;
using System.Net.Http;

namespace ConsultorioMedico.Aplicacao.Services
{
    public class ProntuariosService : IProntuariosService
    {
        //private readonly HttpClient _httpClient;
        private readonly IProntuariosRepository _prontuariosRepository;

        //public ProntuariosService(HttpClient httpClient, IProntuariosRepository prontuariosRepository)
        //{
        //    _httpClient = httpClient;
        //    _prontuariosRepository = prontuariosRepository;
        //}

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

        public async Task<List<ProntuariosViewModel>> CriarProntuarioPorId(ProntuariosInputModel model)
        {
            var medicoResponse = model.MedicoId;
            var pacienteResponse = model.PacienteId;

            var prontuario = new Prontuarios(model.MedicoId,model.PacienteId, model.TextoProntuario);

            //await _prontuariosRepository.Add(prontuario);

            var cadProntList = await _prontuariosRepository.Add(prontuario);
            if (cadProntList == null)
                return null;

            var viewModel = cadProntList.Select(c => new ProntuariosViewModel
            {
                Id = c.Id,
                IdPaciente = c.PacienteId,
                NomePaciente = c.PacienteNome,
                IdMedico = c.MedicoId,
                NomeMedico = c.MedicoNome,
                MedicoEspecialidade = c.MedicoEspecialidade,
                TextoProntuario = c.TextoProntuario
            }).ToList();

            return viewModel;
        }



        //public async Task<List<ProntuariosViewModel>> CriarProntuarioPorId(ProntuariosInputModel model)
        //{
        //    // Obter dados do médico da API externa
        //    var medicoResponse = await _httpClient.GetAsync($"https://localhost:7176/api/CadMedicos/{model.MedicoId}");
        //    if (!medicoResponse.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Erro ao obter os dados do médico");
        //    }

        //    var medicoContent = await medicoResponse.Content.ReadAsStringAsync();
        //    var deserealizadoMedico = JsonConvert.DeserializeObject<ExternalApiInputModel>(medicoContent);

        //    // Obter dados do paciente da API externa
        //    var pacienteResponse = await _httpClient.GetAsync($"https://localhost:7099/api/CadClientes/{model.PacienteId}");
        //    if (!pacienteResponse.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Erro ao obter os dados do paciente");
        //    }

        //    var pacienteContent = await pacienteResponse.Content.ReadAsStringAsync();
        //    var deserealizadoPaciente = JsonConvert.DeserializeObject<ExternalApiInputModel>(pacienteContent);

        //    var prontuario = new Prontuario(deserealizadoMedico.Id, deserealizadoMedico.nome, deserealizadoMedico.especialidade, deserealizadoPaciente.Id, deserealizadoPaciente.nome, model.TextoProntuario);

        //    // Persistir o prontuário no banco de dados
        //    var cadProntList = await _prontuariosRepository.Add(prontuario);
        //    if (cadProntList == null)
        //        return null;

        //    var viewModel = cadProntList.Select(c => new ProntuariosViewModel
        //    {
        //        Id = c.Id,
        //        IdPaciente = Guid.Parse(c.PacienteId),
        //        NomePaciente = c.PacienteNome,
        //        IdMedico = Guid.Parse(c.MedicoId),
        //        NomeMedico = c.MedicoNome,
        //        EspecialidadeMedico = c.MedicoEspecialidade,
        //        TextoProntuario = c.TextoProntuario
        //    }).ToList();

        //    return viewModel;

        //}

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






    //public class ProntuariosService : IProntuariosService
    //{
    //    private readonly ICadPacientesService _cadPacienteService;
    //    private readonly ICadMedicosService _cadMedicoService;
    //    private readonly IProntuariosRepository _prontuariosRepository;
    //    private static readonly List<Prontuarios> _prontuariosList = new List<Prontuarios>();

    //    public ProntuariosService(ICadPacientesService cadPacientesService, ICadMedicosService cadMedicosService)
    //    {
    //        _cadPacienteService = cadPacientesService;
    //        _cadMedicoService = cadMedicosService;
    //    }

    //    //public async Task<List<ProntuariosViewModel>> CriarProntuarioPorId(ProntuariosInputModel model)
    //    //{
    //    //    Obter dados do médico da API externa
    //    //   var medicoResponse = await _httpClient.GetAsync($"https://localhost:7176/api/CadMedicos/{model.MedicoId}");
    //    //    if (!medicoResponse.IsSuccessStatusCode)
    //    //    {
    //    //        throw new Exception("Erro ao obter os dados do médico");
    //    //    }

    //    //    var medicoContent = await medicoResponse.Content.ReadAsStringAsync();
    //    //    var deserealizadoMedico = JsonConvert.DeserializeObject<ExternalApiInputModel>(medicoContent);

    //    //    Obter dados do paciente da API externa
    //    //   var pacienteResponse = await _httpClient.GetAsync($"https://localhost:7099/api/CadClientes/{model.PacienteId}");
    //    //    if (!pacienteResponse.IsSuccessStatusCode)
    //    //    {
    //    //        throw new Exception("Erro ao obter os dados do paciente");
    //    //    }

    //    //    var pacienteContent = await pacienteResponse.Content.ReadAsStringAsync();
    //    //    var deserealizadoPaciente = JsonConvert.DeserializeObject<ExternalApiInputModel>(pacienteContent);

    //    //    var prontuario = new Prontuario(deserealizadoMedico.Id, deserealizadoMedico.nome, deserealizadoMedico.especialidade, deserealizadoPaciente.Id, deserealizadoPaciente.nome, model.TextoProntuario);

    //    //    Persistir o prontuário no banco de dados
    //    //   var cadProntList = await _prontuariosRepository.Add(prontuario);
    //    //    if (cadProntList == null)
    //    //        return null;

    //    //    var viewModel = cadProntList.Select(c => new ProntuariosViewModel
    //    //    {
    //    //        Id = c.Id,
    //    //        IdPaciente = Guid.Parse(c.PacienteId),
    //    //        NomePaciente = c.PacienteNome,
    //    //        IdMedico = Guid.Parse(c.MedicoId),
    //    //        NomeMedico = c.MedicoNome,
    //    //        EspecialidadeMedico = c.MedicoEspecialidade,
    //    //        TextoProntuario = c.TextoProntuario
    //    //    }).ToList();

    //    //    return viewModel;

    //    //}

    //    public async Task<CadMedicosViewModel> Add(CadMedicosInputModel model)
    //    {
    //        var cadMedico = model.ToEntity();
    //        await _repository.Add(cadMedico);

    //        var viewModel = new CadMedicosViewModel
    //        {
    //            Id = cadMedico.Id,
    //            Nome = cadMedico.Nome,
    //            CPF = cadMedico.CPF,
    //            Especialidade = cadMedico.Especialidade,
    //            DataCriacao = cadMedico.DataCriacao,
    //            Ativo = cadMedico.Ativo
    //        };
    //        return viewModel;
    //    }

    //    public async Task<List<ProntuariosViewModel>> CriarProntuarioPorId(ProntuariosInputModel model)
    //    {
    //        var prontuarios = model.ToEntity();
    //        await _prontuariosRepository.Add(prontuarios);

    //        var cadProntList = await _prontuariosRepository.Add(prontuarios);
    //        if (cadProntList == null)
    //            return null;

    //        var viewModel = cadProntList.Select(c => new ProntuariosViewModel
    //        {
    //            Id = c.Id,
    //            IdPaciente = c.PacienteId,
    //            NomePaciente = c.PacienteNome,
    //            IdMedico = c.MedicoId,
    //            NomeMedico = c.MedicoNome,
    //            EspecialidadeMedico = c.MedicoEspecialidade,
    //            TextoProntuario = c.TextoProntuario
    //        }).ToList();

    //        return viewModel;
    //    }

    //    public async Task<ProntuariosViewModel> CriarProntuarioPorId(ProntuariosInputModel model)
    //    {
    //        var prontuarios = model.ToEntity();
    //        _prontuariosList.Add(prontuarios);

    //        var pacientes = await _cadPacienteService.GetAll();
    //        var medicos = await _cadMedicoService.GetAll();

    //        var pacienteViewModel = pacientes.FirstOrDefault(p => p.Id == model.Paciente.Id);
    //        var medicoViewModel = medicos.FirstOrDefault(m => m.Id == model.Medico.Id);

    //        if (pacienteViewModel == null || medicoViewModel == null)
    //        {
    //            return null; // Tratar o caso em que o paciente ou médico não foi encontrado
    //        }

    //        var paciente = new CadPacientes(pacienteViewModel.Nome, pacienteViewModel.CPF);
    //        var medico = new CadMedicos(medicoViewModel.Nome, medicoViewModel.CPF, medicoViewModel.Especialidade);

    //        var prontuario = new Prontuarios(medico, paciente, model.TextoProntuario);

    //        var cadProntList = await _prontuariosRepository.Add(prontuario);
    //        if (cadProntList == null)
    //            return null;

    //        var viewModel = cadProntList.Select(c => new ProntuariosViewModel
    //        {
    //            Id = c.Id,
    //            IdPaciente = c.PacienteId,
    //            NomePaciente = c.PacienteNome,
    //            IdMedico = c.MedicoId,
    //            NomeMedico = c.MedicoNome,
    //            EspecialidadeMedico = c.MedicoEspecialidade,
    //            TextoProntuario = c.TextoProntuario
    //        }).ToList();

    //        return viewModel;
    //    }

    //    public async Task<ProntuariosViewModel> CriarProntuarioPorNome(ProntuariosInputModel model)
    //    {
    //        var prontuarios = model.ToEntity();
    //        _prontuariosList.Add(prontuarios);

    //        var pacientes = await _cadPacienteService.GetAll();
    //        var medicos = await _cadMedicoService.GetAll();

    //        var pacienteViewModel = pacientes.FirstOrDefault(p => p.Nome == model.Paciente.Nome);
    //        var medicoViewModel = medicos.FirstOrDefault(m => m.Nome == model.Medico.Nome);

    //        if (pacienteViewModel == null || medicoViewModel == null)
    //        {
    //            return null; // Tratar o caso em que o paciente ou médico não foi encontrado
    //        }

    //        var paciente = new CadPacientes(pacienteViewModel.Nome, pacienteViewModel.CPF);
    //        var medico = new CadMedicos(medicoViewModel.Nome, medicoViewModel.CPF, medicoViewModel.Especialidade);

    //        var prontuario = new Prontuarios(medico, paciente, model.TextoProntuario);

    //        var prontuarioViewModel = new ProntuariosViewModel
    //        {
    //            Id = prontuarios.Id,
    //            IdPaciente = paciente.Id,
    //            NomePaciente = paciente.Nome,
    //            CPFPaciente = paciente.CPF,
    //            IdMedico = medico.Id,
    //            NomeMedico = medico.Nome,
    //            CPFMedico = medico.CPF,
    //            EspecialidadeMedico = medico.Especialidade,
    //            TextoProntuario = prontuario.TextoProntuario
    //        };

    //        return prontuarioViewModel;
    //    }

    //    public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomeMedico(string nomeMedico)
    //    {
    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Medico.Nome == nomeMedico);

    //        if (prontuarios == null)
    //        {
    //            return null;
    //        }

    //        var prontuariosViewModel = new ProntuariosViewModel
    //        {
    //            Id = prontuarios.Id,
    //            IdMedico = prontuarios.Medico.Id,
    //            NomeMedico = prontuarios.Medico.Nome,
    //            EspecialidadeMedico = prontuarios.Medico.Especialidade,
    //            IdPaciente = prontuarios.Paciente.Id,
    //            NomePaciente = prontuarios.Paciente.Nome,
    //            TextoProntuario = prontuarios.TextoProntuario
    //        };

    //        return await Task.FromResult(prontuariosViewModel);
    //    }

    //    public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomePaciente(string nomePaciente)
    //    {
    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Paciente.Nome == nomePaciente);

    //        if (prontuarios == null)
    //        {
    //            return null;
    //        }

    //        var prontuariosViewModel = new ProntuariosViewModel
    //        {
    //            Id = prontuarios.Id,
    //            IdMedico = prontuarios.Medico.Id,
    //            NomeMedico = prontuarios.Medico.Nome,
    //            EspecialidadeMedico = prontuarios.Medico.Especialidade,
    //            IdPaciente = prontuarios.Paciente.Id,
    //            NomePaciente = prontuarios.Paciente.Nome,
    //            TextoProntuario = prontuarios.TextoProntuario
    //        };

    //        return await Task.FromResult(prontuariosViewModel);
    //    }

    //    public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomeMedicoAtivos(string nomeMedico)
    //    {
    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Medico.Nome == nomeMedico && p.Ativo == true);

    //        if (prontuarios == null)
    //        {
    //            return null;
    //        }

    //        var prontuariosViewModel = new ProntuariosViewModel
    //        {
    //            Id = prontuarios.Id,
    //            IdMedico = prontuarios.Medico.Id,
    //            NomeMedico = prontuarios.Medico.Nome,
    //            EspecialidadeMedico = prontuarios.Medico.Especialidade,
    //            IdPaciente = prontuarios.Paciente.Id,
    //            NomePaciente = prontuarios.Paciente.Nome,
    //            TextoProntuario = prontuarios.TextoProntuario
    //        };

    //        return await Task.FromResult(prontuariosViewModel);
    //    }

    //    public async Task<ProntuariosViewModel> ConsultarProntuarioPorNomePacienteAtivos(string nomePaciente)
    //    {
    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Paciente.Nome == nomePaciente && p.Ativo == true);

    //        if (prontuarios == null)
    //        {
    //            return null;
    //        }

    //        var prontuariosViewModel = new ProntuariosViewModel
    //        {
    //            Id = prontuarios.Id,
    //            IdMedico = prontuarios.Medico.Id,
    //            NomeMedico = prontuarios.Medico.Nome,
    //            EspecialidadeMedico = prontuarios.Medico.Especialidade,
    //            IdPaciente = prontuarios.Paciente.Id,
    //            NomePaciente = prontuarios.Paciente.Nome,
    //            TextoProntuario = prontuarios.TextoProntuario
    //        };

    //        return await Task.FromResult(prontuariosViewModel);
    //    }

    //    public async Task<bool> EditarProntuario(string id, ProntuariosInputModel model)
    //    {
    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Id.ToString() == id);

    //        if (prontuarios == null)
    //        {
    //            return false;
    //        }

    //        prontuarios.Update(model.TextoProntuario);

    //        return true;
    //    }

    //    public async Task<bool> RemoverProntuarioSoft(string id)
    //    {
    //        if(!Guid.TryParse(id, out var guid))
    //            return false;

    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Id == guid && p.Ativo);

    //        if(prontuarios == null)
    //            return false;

    //        prontuarios.Excluir();

    //        return true;
    //    }

    //    public async Task<bool> RemoverProntuarioHard(string id)
    //    {
    //        var prontuarios = _prontuariosList.FirstOrDefault(p => p.Id.ToString() == id);

    //        if (prontuarios == null)
    //            return false;

    //        _prontuariosList.Remove(prontuarios);

    //        return true;
    //    }

    //}
}