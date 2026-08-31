
using eSalud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// Si desarrollamdos una API pura, especialmente para consumir desde frontend o apps móviles:
//Usamos a modo organizativo [Route("api/[controller]")]

// Si desarrollamos algo interno, pequeño o una app híbrida (MVC + API):
//Usamos [Route("[controller]")]

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    

    public AuthController
    (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ApplicationDbContext context
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }


    [HttpPost("registrar")]

    public async Task<IActionResult> RegistrarUsuario(Registro DatosdatosUsuarios)
    {

        

        try
        {
            var adminExiste = await _context.Administradores.Where(a => a.DNI == DatosdatosUsuarios.DNI && a.Email == DatosdatosUsuarios.Email).FirstOrDefaultAsync();

        var userRegistrado = new ApplicationUser
        {
            UserName = DatosdatosUsuarios.Email,
            Email = DatosdatosUsuarios.Email,
            NombreCompleto = DatosdatosUsuarios.NombreCompleto
        };

        var administrador = new Administrador
        {
            NombreCompleto = DatosdatosUsuarios.NombreCompleto,
            Email = DatosdatosUsuarios.Email,
            DNI = DatosdatosUsuarios.DNI

        };

        if(adminExiste != null)
        {
            return Ok("El asministrador ya existe");
        }

        var resultado = await _userManager.CreateAsync(userRegistrado, DatosdatosUsuarios.Password = "Admin1234+");

        
        if (resultado.Succeeded)
        {
            _context.Administradores.Add(administrador);
            await _context.SaveChangesAsync();

            administrador.Legajo = GenerarLegajo("Administrador", administrador.AdministradirId);
            await _context.SaveChangesAsync();


            return Ok("Usuario Registrado");
        } else
        {
           return BadRequest(resultado.Errors);
        }
        }
        catch (Exception error)
        {
            return StatusCode(500, "Ocurrio un error inesperado");
        }
    }

    private string GenerarLegajo (string rol, int id)
    {
        string prefijo = rol switch
        {
            "Administrador" => "A",
            "Medico" => "M",
            "TecnicoImagenes" => "T",
            "Paciente" => "p",
            _ => "X"
        };

        return $"{prefijo}{id.ToString("D4")}";
    }
}