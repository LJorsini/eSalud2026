using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Administrador
    {
        [Key]
        public int AdministradorId {get; set;}
        public string NombreCompleto {get; set;}
        public string Email {get; set;}
        public DateOnly? FechaNacimiento {get; set;}
        public string? DNI {get; set;}
        public Sexo? Sexo {get; set;}
        public string? Direccion {get; set;}
        public string? CP {get; set;}
        public int? LocalidadId {get; set;}
        public string? Telefono {get; set;}
        public string? UserId {get; set;}
        public bool? Activo {get; set;}
        public virtual Localidad? Localidad {get; set;}
        public ApplicationUser? User { get; set; }
        
    }
}