using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Legajo
    {
        [Key]
        public int LegajoId {get; set;}
        public string UserId {get; set;}
        public string? Observaciones {get; set;}
        public DateOnly FechaIngreso {get; set;}
        public ApplicationUser? User {get; set;}  //Relacion de referencia
        public List<LegajoVacaciones>? Vacaciones {get;set;}
        public List<LegajoSanciones>? Sanciones {get;set;}

    }
}