using System.ComponentModel.DataAnnotations;

namespace eSalud.Models
{
    public class MedicoEspecialidad
    {
        [Key]
        public int MedicoEspecialidadId {get; set;}
        public int MedicoId {get; set;}
        public int EspecialidadId {get; set;}
        public virtual Medico? Medico {get;set;}
        public virtual Especialidad? Especialidad {get; set;}
        
    }
}