using eSalud.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser

{

    // Puedes agregar campos extra como:

    public string NombreCompleto { get; set; }
    public string? Dni { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public Sexo? Sexo { get; set; }
    public string? Direccion { get; set; }
    public int? LocalidadId { get; set; }
    public bool Activo { get; set; } = true;
    public Localidad? Localidad { get; set; }

}