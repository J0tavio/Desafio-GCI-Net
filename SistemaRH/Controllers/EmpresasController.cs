using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;
using SistemaRH.Models;

[ApiController]
[Route("[controller]")]
public class EmpresasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmpresasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Todas")]
    public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
    {
        return await _context.Empresas.ToListAsync();
    }

    [HttpGet("Unica{id}")]
    public async Task<ActionResult<Empresa>> GetEmpresa(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);

        if (empresa == null)
        {
            return NotFound();
        }

        return empresa;
    }

    [HttpPost("Adicionar")]
    public async Task<ActionResult<Empresa>> PostEmpresa([FromBody] Empresa empresa)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_context.Empresas.Any(e => e.CNPJ == empresa.CNPJ))
        {
            return BadRequest("Já existe uma empresa com este CNPJ.");
        }

        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
    }

    [HttpPut("Atualizar{id}")]
    public async Task<IActionResult> PutEmpresa(int id, [FromBody] Empresa empresa)
    {
        if (id != empresa.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_context.Empresas.Any(e => e.CNPJ == empresa.CNPJ && e.Id != id))
        {
            return BadRequest("Já existe uma empresa com este CNPJ.");
        }

        _context.Entry(empresa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmpresaExists(id))
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
    public async Task<IActionResult> DeleteEmpresa(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa == null)
        {
            return NotFound();
        }

        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmpresaExists(int id)
    {
        return _context.Empresas.Any(e => e.Id == id);
    }
}
