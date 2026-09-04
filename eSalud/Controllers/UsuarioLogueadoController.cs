using eSalud.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

public class UsuarioLogueado : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _rolManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    
    public UsuarioLogueado
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


    [HttpGet]

    public async Task<IActionResult> Usuario()
    {
        var UsuarioLogueado = _userManager.Users;
        return Ok();
    }
}



    

    
