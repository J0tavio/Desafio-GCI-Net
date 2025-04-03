namespace SistemaRH.Models;
using System.ComponentModel.DataAnnotations;

public class Departamento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public required string Nome { get; set; }

    public required ICollection<Funcionario> Funcionarios { get; set; }
}