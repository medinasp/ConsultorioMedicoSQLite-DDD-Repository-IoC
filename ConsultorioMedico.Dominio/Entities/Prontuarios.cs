namespace ConsultorioMedico.Dominio.Entities
{
    public class Prontuarios
    {
        public Guid Id { get; private set; }
        public Guid MedicoId { get; private set; }
        public string MedicoNome { get; set; }
        public string MedicoEspecialidade { get; set; }
        public Guid PacienteId { get; private set; }
        public string PacienteNome { get; set; }
        public string TextoProntuario { get; private set; }
        public bool Ativo { get; private set; }

        public CadMedicos Medico { get; private set; }
        public CadPacientes Paciente { get; private set; }

        protected Prontuarios() { }

        public Prontuarios(Guid medicoId, Guid pacienteId, string textoProntuario)
        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            TextoProntuario = textoProntuario;
            Ativo = true;
        }

        public void Excluir()
        {
            Ativo = false;
        }

        public void Update(string textoProntuario)
        {
            TextoProntuario = textoProntuario;
        }
    }
}