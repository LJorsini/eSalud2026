using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Provincia
    {
        [Key]
        public int ProvinciaId {get; set;}
        public string NombreProvincia {get; set;}
        public virtual ICollection<Localidad>? Localidades{get; set;}
    }
}