using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using eSalud.Helpers;
using eSalud.Models;


var builder = WebApplication.CreateBuilder(args);

// Configuración EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Configuración Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Configuración JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseHttpsRedirection();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

/* La primera vez que corre el servidor verifica si existen administradores y si la tabla esta vacia crea el super admin */

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var context = services.GetRequiredService<ApplicationDbContext>();

    /* Si no existen los roles los creo */

    var provinciaExiste = await context.Provincias.AnyAsync();

    if(!provinciaExiste)
    {
        var provincias = new List<Provincia>
        {
            new Provincia { NombreProvincia = "Buenos Aires" },
            new Provincia { NombreProvincia = "Catamarca" },
            new Provincia { NombreProvincia = "Chaco" },
            new Provincia { NombreProvincia = "Chubut" },
            new Provincia { NombreProvincia = "Ciudad Autónoma de Buenos Aires" },
            new Provincia { NombreProvincia = "Córdoba" },
            new Provincia { NombreProvincia = "Corrientes" },
            new Provincia { NombreProvincia = "Entre Ríos" },
            new Provincia { NombreProvincia = "Formosa" },
            new Provincia { NombreProvincia = "Jujuy" },
            new Provincia { NombreProvincia = "La Pampa" },
            new Provincia { NombreProvincia = "La Rioja" },
            new Provincia { NombreProvincia = "Mendoza" },
            new Provincia { NombreProvincia = "Misiones" },
            new Provincia { NombreProvincia = "Neuquén" },
            new Provincia { NombreProvincia = "Río Negro" },
            new Provincia { NombreProvincia = "Salta" },
            new Provincia { NombreProvincia = "San Juan" },
            new Provincia { NombreProvincia = "San Luis" },
            new Provincia { NombreProvincia = "Santa Cruz" },
            new Provincia { NombreProvincia = "Santa Fe" },
            new Provincia { NombreProvincia = "Santiago del Estero" },
            new Provincia { NombreProvincia = "Tierra del Fuego" },
            new Provincia { NombreProvincia = "Tucumán" }
        };

        context.Provincias.AddRange(provincias);
        context.SaveChangesAsync();
    };


    string [] roles = {"ADMINISTRADOR", "MEDICO", "TECNICOIMAGENES", "PACIENTRE"};

    foreach(var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

    /* si la tabla admin esta vacia, crea un super admin generico  */

    var adminExiste = await context.Administradores.AnyAsync();

    if(!adminExiste)
    {
        var adminUser = new ApplicationUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            NombreCompleto = "Super Admin",
            Dni = "00000000"
        };

        var resultado = await userManager.CreateAsync(adminUser, "Admin1234+");

        if (resultado.Succeeded)
        {
            var administrador = new Administrador
            {
                NombreCompleto = adminUser.NombreCompleto,
                Email = adminUser.Email,
                DNI = adminUser.Dni,
            };

            context.Administradores.Add(administrador);
            await context.SaveChangesAsync();

            await userManager.AddToRoleAsync(adminUser, "ADMINISTRADOR");

            administrador.Legajo = LegajoHelper.GenerarLegajo("ADMINISTRADOR", administrador.AdministradorId);
            administrador.UserId = adminUser.Id;
            await context.SaveChangesAsync();
        }
    }

}





app.Run();