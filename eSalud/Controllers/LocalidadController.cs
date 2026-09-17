using eSalud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
public class LocalidadController : Controller
{
    private readonly ApplicationDbContext _context;

    public LocalidadController(ApplicationDbContext context)
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
}