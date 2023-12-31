﻿namespace ConsultorioMedico.Aplicacao.ViewModels
{
    public class CadMedicosViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Especialidade { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
