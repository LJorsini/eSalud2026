
using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Administrador
    {
        [Key]
        public int AdministradirId {get; set;}
        public string NombreCompleto {get; set;}
        public string Email {get; set;}
        public DateOnly? FechaNacimineto {get; set;}
        public string? DNI {get; set;}
        public string? Legajo {get; set;}
    }
}