namespace SistemaRH.Models;
using System.ComponentModel.DataAnnotations;
public class Funcionario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public required string Nome { get; set; }

    public int EmpresaId { get; set; }

    [Required]
    public required Empresa Empresa { get; set; }

    public int DepartamentoId { get; set; }

    [Required]
    public required Departamento Departamento { get; set; }

    public required ICollection<Tarefa> Tarefas { get; set; }
} 