using eSalud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class EspecialidadesController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public EspecialidadesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<VistaEspecialidades>>> ObtenerEspecialidades()
    {
        List<VistaEspecialidades> vistaEspecialidad = new List<VistaEspecialidades>();

        var especialidades = await _context.Especialidades.ToArrayAsync();

        foreach (var especialidad in especialidades)
        {
            var mostrarEspecialidad = new VistaEspecialidades
            {
                EspecialidadId = especialidad.EspecialidadId,
                NombreEspecialidad = especialidad.NombreEspecialidad
            };

            vistaEspecialidad.Add(mostrarEspecialidad);
        }

        return Ok(vistaEspecialidad);
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<VistaEspecialidades>> ObtenerLocalidad(int id)
    {
        var especialidad = await _context.Especialidades.Where(e => e.EspecialidadId == id)
                           .FirstOrDefaultAsync();

        if(especialidad == null)
        {
            return NotFound("");
        }

        var mostrarespecialidad = new VistaEspecialidades
        {
            EspecialidadId = especialidad.EspecialidadId,
            NombreEspecialidad = especialidad.NombreEspecialidad
        };
        return Ok(mostrarespecialidad);
    }

    [HttpPost]

    public async Task<ActionResult> CrearEspecialidad(Especialidad especialidad)
    {

        especialidad.NombreEspecialidad = especialidad.NombreEspecialidad.Trim().ToUpper();

        var especialidadExiste = await _context.Especialidades.Where(e => e.NombreEspecialidad == especialidad.NombreEspecialidad).FirstOrDefaultAsync();

        if(especialidadExiste != null)
        {
            return BadRequest("La especialidad ya existe");
        }

        var nuevaEspecialidad = new Especialidad
        {
            NombreEspecialidad = especialidad.NombreEspecialidad
        };

        _context.Especialidades.Add(nuevaEspecialidad);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]

    public async Task<ActionResult> EditarEspecialidad(int id, Especialidad especialidad)
    {
        var especialidadExiste = await _context.Especialidades.Where(e => e.EspecialidadId == id).FirstOrDefaultAsync();

        if(especialidadExiste == null)
        {
            return BadRequest("La especialidad noexiste");
        }

        especialidadExiste.NombreEspecialidad = especialidad.NombreEspecialidad;
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}