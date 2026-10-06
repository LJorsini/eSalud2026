using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Legajo
    {
        [Key]
        public int LegajoId {get; set;}
        public string? UsuarioId {get; set;}
        public ApplicationUser? Usuario {get; set;}
        public string? NumeroLegajo {get; set;}
        public string? Observaciones {get; set;}

        List<LegajoVacaciones>? Vacaciones {get;set;}
        List<LegajoSanciones>? Sanciones {get;set;}

        



    }
}