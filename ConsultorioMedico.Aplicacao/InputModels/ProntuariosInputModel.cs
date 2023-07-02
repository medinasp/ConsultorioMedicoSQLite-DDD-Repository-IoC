namespace ConsultorioMedico.Aplicacao.InputModels
{
    public class ProntuariosInputModel
    {
        public Guid MedicoId { get; set; }
        //public string MedicoNome { get; set; }
        //public string MedicoEspecialidade { get; set; }
        public Guid PacienteId { get; set; }
        //public string PacienteNome { get; set; }
        public string TextoProntuario { get; set; }

    }
}
