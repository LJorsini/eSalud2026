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
                NombreEspecialidad = especialidad.NombreEspecialidad
            };

            vistaEspecialidad.Add(mostrarEspecialidad);
        }

        return Ok(vistaEspecialidad);
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
}