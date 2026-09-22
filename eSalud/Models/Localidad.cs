using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Localidad
    {
        [Key]
        public int LocalidadId {get; set;}
        public string NombreLocalidad {get; set;}
        public string? CP {get; set;}
        public int? ProvinciaId {get; set;}
        public virtual Provincia? Provincia {get; set;}
        
    }
}