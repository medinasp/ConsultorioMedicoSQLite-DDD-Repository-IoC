namespace ConsultorioMedico.Dominio.Entities
{
    public class CadPacientes : EntityBase
    {
        protected CadPacientes() : base("", "")
        {
        }

        public CadPacientes(string nome, string cpf) : base(nome, cpf)
        {}

        public void Excluir()
        {
            Ativo = false;
        }

        public void Update(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }
    }
}
