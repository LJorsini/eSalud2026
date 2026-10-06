namespace eSalud.Models
{
    public class LegajoVacaciones
    {
        public int Id {get; set;}
        public int LegajoId {get; set;}
        public Legajo? Legajo {get; set;}
        public DateOnly? Desde {get; set;}
        public DateOnly? Hasta{get; set;}


    }
}