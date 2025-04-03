namespace SistemaRH.Models;
using System.ComponentModel.DataAnnotations;

public class Empresa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [RegularExpression(@"\d{14}", ErrorMessage = "O CNPJ deve ter 14 dígitos.")]
    public required string CNPJ { get; set; }

    public required ICollection<Funcionario> Funcionarios { get; set; }
} 


