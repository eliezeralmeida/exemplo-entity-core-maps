using System;

namespace ExemplosEntity.OneToMany
{
    public class Desenvolvedor : Entity
    {
        public string Nome { get; set; }
        public Guid ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
    }
}