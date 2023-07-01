using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Aplicacao.InputModels
{
    public class CadMedicosInputModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Especialidade { get; set; }

        public CadMedicos ToEntity()
            => new(Nome, CPF, Especialidade);
    }
}
