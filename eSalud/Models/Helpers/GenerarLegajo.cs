namespace eSalud.Helpers
{
    public static class LegajoHelper
    {
        public static string GenerarLegajo(string rol, int id)
        {
            string prefijo = rol switch
            {
                "ADMINISTRADOR" => "A",
                "MEDICO" => "M",
                "TECNICOIMAGENES" => "T",
                "PACIENTE" => "p",
                _ => "X"
            };

            return $"{prefijo}{id.ToString("D4")}";
        }
    }
}