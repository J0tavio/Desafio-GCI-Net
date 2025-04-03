using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;
using SistemaRH.Models;

[ApiController]
[Route("[controller]")]
public class FuncionariosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FuncionariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Todos")]
    public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
    {
        return await _context.Funcionarios.ToListAsync();
    }

    [HttpGet("Unico{id}")]
    public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);

        if (funcionario == null)
        {
            return NotFound();
        }

        return funcionario;
    }

    [HttpPost("Adicionar")]
    public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
    {
        if (_context.Funcionarios.Any(f => f.EmpresaId == funcionario.EmpresaId && f.DepartamentoId == funcionario.DepartamentoId))
        {
            return BadRequest("O funcionário já está associado a uma empresa e um departamento.");
        }

        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
    }

    [HttpPut("Atualizar{id}")]
    public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
    {
        if (id != funcionario.Id)
        {
            return BadRequest();
        }

        if (_context.Funcionarios.Any(f => f.EmpresaId == funcionario.EmpresaId && f.DepartamentoId == funcionario.DepartamentoId && f.Id != id))
        {
            return BadRequest("O funcionário já está associado a uma empresa e um departamento.");
        }

        _context.Entry(funcionario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FuncionarioExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("Deletar{id}")]
    public async Task<IActionResult> DeleteFuncionario(int id)
    {
        var funcionario = await _context.Funcionarios.FindAsync(id);
        if (funcionario == null)
        {
            return NotFound();
        }

        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FuncionarioExists(int id)
    {
        return _context.Funcionarios.Any(e => e.Id == id);
    }
}
