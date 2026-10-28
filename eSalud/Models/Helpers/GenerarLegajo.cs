namespace eSalud.Helpers
{
    public static class LegajoHelper
    {
        public static string GenerarLegajo(int id)
        {
            /* string prefijo = rol switch
            {
                "ADMINISTRADOR" => "A",
                "MEDICO" => "M",
                "TECNICOIMAGENES" => "T",
                "PACIENTE" => "p",
                _ => "X"
            }; */

            return $"{id.ToString("D4")}";
        }
    }
}