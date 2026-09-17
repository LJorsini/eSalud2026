using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Medico
    {
        [Key]
        public int MedicoId {get; set;}
        public string NombreCompleto {get; set;}
        public string Email {get; set;}
        public DateOnly? FechaNacimiento {get; set;}
        public string? DNI {get; set;}
        public string? Direccion {get; set;}
        public int? LocalidadId {get; set;}
        public string? CP {get; set;}
        public string? Telefono {get; set;}
        public string? Legajo {get; set;}
        public string? MP {get; set;}
        public string? UserId {get; set;}
        public virtual Localidad Localidad {get; set;}
    }
}