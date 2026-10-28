namespace eSalud.Models
{
    public class VistaAdministradores
    {
    public int AdministradorId { get; set; }
    public string NombreCompleto { get; set; }
    public string? Dni { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public Sexo? Sexo { get; set; }
    public string? Direccion { get; set; }
    public int? LocalidadId { get; set; }
    public string NombreLocalidad {get;set;}
    public int ProProvinciaId {get; set;}
    public int NombreProvincia{get; set;}
    

    }
}