namespace ConsultorioMedico.Dominio.Entities
{
    public abstract class EntityBase
    {
        public EntityBase(string nome, string cpf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            CPF = cpf;
            DataCriacao = DateTime.Now;
            Ativo = true;
        }

        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string CPF { get; protected set; }
        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; protected set; }
    }
}
