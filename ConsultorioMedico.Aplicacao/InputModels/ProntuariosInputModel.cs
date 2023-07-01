using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Aplicacao.InputModels
{
    public class ProntuariosInputModel
    {
        public CadMedicos Medico { get; set; }
        public CadPacientes Paciente { get; set; }
        public string TextoProntuario { get; set; }

        public Prontuarios ToEntity()
            => new(Medico, Paciente, TextoProntuario);

    }
}
