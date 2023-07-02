using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.ViewModels;
using ConsultorioMedico.Dominio.Entities;
using ConsultorioMedico.Infra.Repositories;

namespace ConsultorioMedico.Aplicacao.Services
{
    public class CadPacientesService : ICadPacientesService
    {
        private readonly ICadPacientesRepository _repository;
        //private static readonly List<CadPacientes> _cadPacientesList = new List<CadPacientes>();

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

        //public async Task<CadPacientesViewModel> Add(CadPacientesInputModel model)
        //{
        //    var cadPacientes = model.ToEntity();
        //    _cadPacientesList.Add(cadPacientes);

        //    var viewModel = new CadPacientesViewModel
        //    {
        //        Id = cadPacientes.Id,
        //        Nome = cadPacientes.Nome,
        //        CPF = cadPacientes.CPF
        //    };
        //    return await Task.FromResult(viewModel);

        //}

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

        //public async Task<IEnumerable<CadPacientesViewModel>> GetActives()
        //{
        //    var activeCadPacientes = _cadPacientesList.Where(m => m.Ativo);
        //    var viewModels = activeCadPacientes.Select(m => new CadPacientesViewModel
        //    {
        //        Id = m.Id,
        //        Nome = m.Nome,
        //        CPF = m.CPF
        //    });

        //    return await Task.FromResult(viewModels);
        //}

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

        //public async Task<IEnumerable<CadPacientesViewModel>> GetAll()
        //{
        //    var viewModels = _cadPacientesList.Select(m => new CadPacientesViewModel
        //    {
        //        Id = m.Id,
        //        Nome = m.Nome,
        //        CPF = m.CPF
        //    });

        //    return await Task.FromResult(viewModels);
        //}

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

        //public async Task<CadPacientesViewModel> GetByCode(string code)
        //{
        //    if (!Guid.TryParse(code, out var guid))
        //        return null;

        //    var cadPacientes = _cadPacientesList.FirstOrDefault(m => m.Id == guid);
        //    if (cadPacientes == null)
        //        return null;

        //    var cadPacientesViewModel = new CadPacientesViewModel
        //    {
        //        Id = cadPacientes.Id,
        //        Nome = cadPacientes.Nome,
        //        CPF = cadPacientes.CPF
        //    };

        //    return await Task.FromResult(cadPacientesViewModel);
        //}

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

        //public async Task<bool> HardDelete(string id)
        //{
        //    if (!Guid.TryParse(id, out var guid))
        //        return false;

        //    var cadPacientes = _cadPacientesList.FirstOrDefault(m => m.Id.ToString() == id);

        //    if (cadPacientes == null)
        //        return false;

        //    _cadPacientesList.Remove(cadPacientes);

        //    return true;

        //}

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

        //public async Task<bool> SoftDelete(string id)
        //{
        //    if (!Guid.TryParse(id, out var guid))
        //        return false;

        //    var cadPacientes = _cadPacientesList.FirstOrDefault(m => m.Id == guid && m.Ativo);
        //    if (cadPacientes == null)
        //        return false;

        //    cadPacientes.Excluir();

        //    return true;
        //}

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

        //public async Task<bool> Update(string id, CadPacientesInputModel model)
        //{
        //    if (!Guid.TryParse(id, out var guid))
        //        return false;

        //    var cadPacientes = _cadPacientesList.FirstOrDefault(m => m.Id.ToString() == id);

        //    if (cadPacientes == null)
        //        return false;

        //    cadPacientes.Update(model.Nome, model.CPF);
        //    return true;
        //}
    }

}
