using ConsultorioMedico.Dominio.Entities;

namespace ConsultorioMedico.Aplicacao.InputModels
{
    public class ProntuariosInputModel
    {
        public Guid MedicoId { get; set; }
        public Guid PacienteId { get; set; }
        public string TextoProntuario { get; set; }

        public Prontuarios ToEntity()
                 => new(MedicoId, PacienteId, TextoProntuario);

    }
}
