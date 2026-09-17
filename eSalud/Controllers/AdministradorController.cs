using eSalud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AdministradorController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public AdministradorController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("obtenerProvincias")]

    public IActionResult ObtenerProvincias()
    {
        var provincias = _context.Provincias.OrderByDescending(p => p.NombreProvincia).ToList();

        return Ok(provincias);
    }

    public IActionResult ObtenerLocalidades()
    {
        var localidades = _context.Localidades.OrderByDescending(l => l.NombreLocalidad).ToList();

        return Ok(localidades);
    }
}