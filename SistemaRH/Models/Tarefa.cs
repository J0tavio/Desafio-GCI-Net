namespace SistemaRH.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public required string Descricao { get; set; }
        public required ICollection<Funcionario> Funcionarios { get; set; }
    }
}
