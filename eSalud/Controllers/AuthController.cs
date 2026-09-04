
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
    private readonly RoleManager<IdentityRole> _rolManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    

    

    public AuthController
    (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> rolManager,
        IConfiguration configuration,
        ApplicationDbContext context
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
        _rolManager = rolManager;
    }


    [HttpPost("registrar")]

    public async Task<IActionResult> RegistrarUsuario(Registro DatosdatosUsuarios)
    {

        

        try
        {
            //Craacion de roles, verifico si existen y si no los crea
            var verificarCrearAdmin = _context.Roles.Where(r => r.Name == "ADMINISTRADOR").SingleOrDefault();

            if(verificarCrearAdmin == null)
            {
                var roleResult = await _rolManager.CreateAsync(new IdentityRole("ADMINISTRADOR"));
            }

            var verificarCrearMedico = _context.Roles.Where(r => r.Name == "MEDICO").SingleOrDefault();

            if(verificarCrearMedico == null)
            {
                var roleResult = await _rolManager.CreateAsync(new IdentityRole("MEDICO"));
            }

            var verificarCrearTecnico = _context.Roles.Where(r => r.Name == "TECNICOIMAGENES").SingleOrDefault();

            if(verificarCrearTecnico == null)
            {
                var roleResult = await _rolManager.CreateAsync(new IdentityRole("TECNICOIMAGENES"));
            }

            var verificarCrearPAciente = _context.Roles.Where(r => r.Name == "PACIENTE").SingleOrDefault();

            if(verificarCrearPAciente == null)
            {
                var roleResult = await _rolManager.CreateAsync(new IdentityRole("PACIENTE"));

            }





            /* var adminExiste = await _context.Administradores.Where(a => a.DNI == DatosdatosUsuarios.DNI && a.Email == DatosdatosUsuarios.Email).FirstOrDefaultAsync(); */
            var dniExiste = await _userManager.Users.Where(a => a.Dni == DatosdatosUsuarios.DNI).FirstOrDefaultAsync();
            var emailExiste = await _userManager.FindByEmailAsync(DatosdatosUsuarios.Email);
            /* var emailExiste1 = await _userManager.Users.Where(a => a.Dni == DatosdatosUsuarios.DNI).FirstOrDefaultAsync(); */

        if(dniExiste != null)
        {
            return BadRequest("El DNI ya esta registrado");
        }

        if(emailExiste != null)
        {
            return BadRequest("El email ya esta registrado");  
        }
            

        var userRegistrado = new ApplicationUser
        {
            UserName = DatosdatosUsuarios.Email,
            Email = DatosdatosUsuarios.Email,
            NombreCompleto = DatosdatosUsuarios.NombreCompleto,
            Dni = DatosdatosUsuarios.DNI
        };

        var administrador = new Administrador
        {
            NombreCompleto = DatosdatosUsuarios.NombreCompleto,
            Email = DatosdatosUsuarios.Email,
            DNI = DatosdatosUsuarios.DNI

        };

        

        var resultado = await _userManager.CreateAsync(userRegistrado, DatosdatosUsuarios.Password = "Admin1234+");

        
        if (resultado.Succeeded)
        {
            _context.Administradores.Add(administrador);
            await _context.SaveChangesAsync();

            administrador.Legajo = GenerarLegajo("ADMINISTRADOR", administrador.AdministradirId);
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
            "ADMINISTRADOR" => "A",
            "MEDICO" => "M",
            "TECNICOIMAGENES" => "T",
            "PACIENTE" => "p",
            _ => "X"
        };

        return $"{prefijo}{id.ToString("D4")}";
    }

    [HttpPost("login")]

    public async Task<IActionResult> Login(Login login)
    {

        var user = await _userManager.FindByEmailAsync(login.Email);
        if(user != null && await _userManager.CheckPasswordAsync(user, login.Password))
        {
            //SI EL USUARIO ES ENCONTRADO Y LA CONTRASEÑA ES CORRECTA
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

             //RECUPERAMOS LA KEY SETEADA EN EL APPSETTING
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //ARMAMOS EL OBJETO CON LOS ATRIBUTOS PARA GENERAR EL TOKEN
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // GENERAMOS EL REFRESH TOKEN
            var refreshToken = GenerarRefreshToken();
            //GUARDAMOS EN BASE DE DATOS EL REFRESH TOKEN
            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);

            return Ok(new
            {
                success = true,
                token = jwt,
                refreshToken = refreshToken
            });
        }
        //return Unauthorized("Credenciales inválidas");
        //return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        return Ok(new 
    { 
        success = false, 
        message = "Usuario o contraseña incorrectos" 
    });
    }

    private string GenerarRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    }
