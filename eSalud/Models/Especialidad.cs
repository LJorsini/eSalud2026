using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class Especialidad
    {
        [Key]
        public int EspecialidadId {get; set;}
        public string NombreEspecialidad {get; set;}
        public virtual ICollection<MedicoEspecialidad>? MedicoEspecialidades {get;set;}
    }
}