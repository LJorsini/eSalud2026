using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Administrador
    {
        [Key]
        public int AdministradorId { get; set; }
        public string? UserId { get; set; }
        public bool? Activo {get; set;} = true;
        public ApplicationUser User {get; set;}
        /* public string NombreCompleto {get; set;} */
        
        



    }
}