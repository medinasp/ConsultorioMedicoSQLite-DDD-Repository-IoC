using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Aplicacao.InputModels
{
    public class CadPacientesInputModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

        public CadPacientes ToEntity()
            => new(Nome, CPF);

    }
}
