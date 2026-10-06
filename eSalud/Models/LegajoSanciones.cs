namespace eSalud.Models
{
    public class LegajoSanciones
    {
        public int Id {get; set;}
        public int LegajoId{get; set;}
        public Legajo? Legajo {get;set;}
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string? Observaciones { get; set; }

    }
}