using ConsultorioMedico.Aplicacao.InputModels;
using ConsultorioMedico.Aplicacao.InterfacesServices;
using ConsultorioMedico.Aplicacao.ViewModels;
using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Aplicacao.Services
{
    public class CadMedicosService : ICadMedicosService
    {
        //private readonly ICadClientesRepository _repository;
        private static readonly List<CadMedicos> _cadMedicosList = new List<CadMedicos>();

        public async Task<CadMedicosViewModel> Add(CadMedicosInputModel model)
        {
            var cadMedicos = model.ToEntity();
            _cadMedicosList.Add(cadMedicos);

            var viewModel = new CadMedicosViewModel
            {
                Id = cadMedicos.Id,
                Nome = cadMedicos.Nome,
                CPF = cadMedicos.CPF,
                Especialidade = cadMedicos.Especialidade
            };
            return await Task.FromResult(viewModel);
        }

        public async Task<CadMedicosViewModel> GetByCode(string code)
        {
            if (!Guid.TryParse(code, out var guid))
                return null;

            var cadMedicos = _cadMedicosList.FirstOrDefault(m => m.Id == guid);
            if (cadMedicos == null)
                return null;

            var cadMedicosViewModel = new CadMedicosViewModel
            {
                Id = cadMedicos.Id,
                Nome = cadMedicos.Nome,
                CPF = cadMedicos.CPF,
                Especialidade = cadMedicos.Especialidade,
            };

            return await Task.FromResult(cadMedicosViewModel);
        }

        public async Task<IEnumerable<CadMedicosViewModel>> GetAll()
        {
            var viewModels = _cadMedicosList.Select(m => new CadMedicosViewModel
            {
                Id = m.Id,
                Nome = m.Nome,
                CPF = m.CPF,
                Especialidade = m.Especialidade,
            });

            return await Task.FromResult(viewModels);
        }

        public async Task<bool> Update(string id, CadMedicosInputModel model)
        {
            var cadMedicos = _cadMedicosList.FirstOrDefault(m => m.Id.ToString() == id);

            if (cadMedicos == null)
                return false;

            cadMedicos.Update(model.Nome, model.CPF, model.Especialidade);

            return true;
        }

        public async Task<bool> SoftDelete(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return false;

            var cadMedicos = _cadMedicosList.FirstOrDefault(m => m.Id == guid && m.Ativo);
            if (cadMedicos == null)
                return false;

            cadMedicos.Excluir();

            return true;
        }

        public async Task<IEnumerable<CadMedicosViewModel>> GetActives()
        {
            var activeCadMedicos = _cadMedicosList.Where(m => m.Ativo);
            var viewModels = activeCadMedicos.Select(m => new CadMedicosViewModel
            {
                Id = m.Id,
                Nome = m.Nome,
                CPF = m.CPF,
                Especialidade = m.Especialidade
            });

            return await Task.FromResult(viewModels);
        }

        public async Task<bool> HardDelete(string id)
        {
            var cadMedicos = _cadMedicosList.FirstOrDefault(m => m.Id.ToString() == id);

            if (cadMedicos == null)
                return false;

            _cadMedicosList.Remove(cadMedicos);

            return true;
        }

    }
}
