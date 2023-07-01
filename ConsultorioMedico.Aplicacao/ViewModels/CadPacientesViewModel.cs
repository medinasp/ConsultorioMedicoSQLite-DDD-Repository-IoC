namespace ConsultorioMedico.Aplicacao.ViewModels
{
    public class CadPacientesViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
