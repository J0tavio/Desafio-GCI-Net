using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;
using SistemaRH.Models;

[ApiController]
[Route("[controller]")]
public class DepartamentosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DepartamentosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
    {
        return await _context.Departamentos.ToListAsync();
    }

    [HttpGet("Unico{id}")]
    public async Task<ActionResult<Departamento>> GetDepartamento(int id)
    {
        var departamento = await _context.Departamentos.FindAsync(id);

        if (departamento == null)
        {
            return NotFound();
        }

        return departamento;
    }

    [HttpPost("Adicionar")]
    public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
    {
        _context.Departamentos.Add(departamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.Id }, departamento);
    }

    [HttpPut("Atualizar{id}")]
    public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
    {
        if (id != departamento.Id)
        {
            return BadRequest();
        }

        _context.Entry(departamento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartamentoExists(id))
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
    public async Task<IActionResult> DeleteDepartamento(int id)
    {
        var departamento = await _context.Departamentos.FindAsync(id);
        if (departamento == null)
        {
            return NotFound();
        }

        _context.Departamentos.Remove(departamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DepartamentoExists(int id)
    {
        return _context.Departamentos.Any(e => e.Id == id);
    }
}
