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

[Route("api/[controller]")]
[ApiController]

public class ProvinciasController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProvinciasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<IActionResult>  ObtenerProvincias()
    {
        var provincias =  await _context.Provincias.OrderBy(p => p.NombreProvincia).ToListAsync();

        return Ok(provincias);
    }
}