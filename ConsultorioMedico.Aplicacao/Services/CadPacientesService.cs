using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.ViewModels;
using ConsultorioMedico.Infra.InterfacesRepositories;

namespace ConsultorioMedico.Aplicacao.Services
{
    public class CadPacientesService : ICadPacientesService
    {
        private readonly ICadPacientesRepository _repository;

        public CadPacientesService(ICadPacientesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CadPacientesViewModel> Add(CadPacientesInputModel model)
        {
            var cadPacientes = model.ToEntity();
            await _repository.Add(cadPacientes);

            var viewModel = new CadPacientesViewModel
            {
                Id = cadPacientes.Id,
                Nome = cadPacientes.Nome,
                CPF = cadPacientes.CPF,
                DataCriacao = cadPacientes.DataCriacao,
                Ativo = cadPacientes.Ativo
            };
            return viewModel;
        }

        public async Task<IEnumerable<CadPacientesViewModel>> GetActives()
        {
            var activeCadClientes = await _repository.GetActives();
            var viewModels = activeCadClientes.Select(m => new CadPacientesViewModel
            {
                Id = m.Id,
                Nome = m.Nome,
                CPF = m.CPF,
                Ativo = m.Ativo
            });

            return viewModels;
        }

        public async Task<IEnumerable<CadPacientesViewModel>> GetAll()
        {
            var getAllCadClientes = await _repository.GetAll();
            var viewModels = getAllCadClientes.Select(m => new CadPacientesViewModel
            {
                Id = m.Id,
                Nome = m.Nome,
                CPF = m.CPF,
                Ativo = m.Ativo
            });

            return viewModels;
        }

        public async Task<CadPacientesViewModel> GetByCode(string code)
        {
            if (!Guid.TryParse(code, out var guid))
                return null;

            var cadClientes = await _repository.GetByCode(guid.ToString());
            if (cadClientes == null)
                return null;

            var cadClientesViewModel = new CadPacientesViewModel
            {
                Id = cadClientes.Id,
                Nome = cadClientes.Nome,
                CPF = cadClientes.CPF
            };

            return cadClientesViewModel;
        }

        public async Task<bool> HardDelete(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return false;

            var cadPaciente = await _repository.GetByCode(guid.ToString());

            if (cadPaciente == null)
                return false;

            await _repository.HardDelete(cadPaciente);

            return true;
        }

        public async Task<bool> SoftDelete(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return false;

            var cadPaciente = await _repository.GetByCode(guid.ToString());

            if (cadPaciente == null || !cadPaciente.Ativo)
                return false;

            await _repository.SoftDelete(cadPaciente);

            return true;
        }

        public async Task<bool> Update(string id, CadPacientesInputModel model)
        {
            if (!Guid.TryParse(id, out var guid))
                return false;

            var cadPaciente = await _repository.GetByCode(guid.ToString());

            if (cadPaciente == null)
                return false;

            cadPaciente.Update(model.Nome, model.CPF);

            await _repository.Update(cadPaciente);

            return true;
        }
    }
}
