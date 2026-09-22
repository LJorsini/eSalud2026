using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace eSalud.Models
{
    public class VistaLocalidades
    {
        public int LocalidadId {get;set;}
        public string NombreLocalidad {get; set;}
        public string CP {get; set;}
        public string NombreProvincia {get; set;}
         
        public int ProvinciaId {get; set;}
    }
}