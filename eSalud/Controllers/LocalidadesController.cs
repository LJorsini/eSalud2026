using eSalud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class LocalidadesController : Controller
{
    private readonly ApplicationDbContext _context;

    public LocalidadesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<VistaLocalidades>>> ObtenerLocalidades()
    {
        List<VistaLocalidades> vistaLocalidad = new List<VistaLocalidades>();

        var localidades = await _context.Localidades
                          .Include(p => p.Provincia)
                          .OrderBy(n => n.NombreLocalidad)
                          .ToListAsync();

        foreach (var localidad in localidades)
        {
            var mostrarLocalidad = new VistaLocalidades
            {
                LocalidadId = localidad.LocalidadId,
                NombreLocalidad = localidad.NombreLocalidad,
                ProvinciaId = localidad.Provincia.ProvinciaId,
                NombreProvincia = localidad.Provincia.NombreProvincia,
            };

            vistaLocalidad.Add(mostrarLocalidad);
        }
        return Ok(vistaLocalidad);
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<VistaLocalidades>> GetLocalidad(int id)
    {
        var localidadExiste = await _context.Localidades.Where(l => l.LocalidadId == id)
                        .Include(p => p.Provincia)
                        .FirstOrDefaultAsync();

        if (localidadExiste == null)
        {
            return NotFound();
        }

        var vistaLocalidad = new VistaLocalidades
        {
            LocalidadId = localidadExiste.LocalidadId,
            NombreLocalidad = localidadExiste.NombreLocalidad,
            CP = localidadExiste.CP,
            ProvinciaId = localidadExiste.Provincia.ProvinciaId,
            NombreProvincia = localidadExiste.Provincia.NombreProvincia,
        };
        return Ok(vistaLocalidad);
    }

    [HttpPost]

    public async Task<IActionResult> GuardarLocalidad(Localidad localidad)
    {
        localidad.NombreLocalidad = localidad.NombreLocalidad.Trim().ToUpper();
        localidad.CP = localidad.CP = localidad.CP.Trim().ToUpper();

        var localidadExiste = await _context.Localidades
                              .Where(l => l.NombreLocalidad == localidad.NombreLocalidad && l.ProvinciaId == localidad.ProvinciaId)
                              .FirstOrDefaultAsync();
        
        

        if(localidadExiste != null)
        {
           return BadRequest("La localidad ya existe");
        }

         var nuevaLocalidad = new Localidad
            {
                NombreLocalidad = localidad.NombreLocalidad,
                ProvinciaId = localidad.ProvinciaId,
                CP = localidad.CP,
            };

            _context.Localidades.Add(nuevaLocalidad);
            await _context.SaveChangesAsync();
        return Ok();
    }
}