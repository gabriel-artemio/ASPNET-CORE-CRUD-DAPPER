namespace ApiDotnet_TCC.Models
{
    public class DadosGeladeira
    {
        public int id { get; set; }
        public double temp_sensor_1 { get; set; }
        public double temp_sensor_2 { get; set; }
        public double temp_sensor_externo { get; set; }
        public bool porta_aberta { get; set; }
        public DateTime timestamp { get; set; } = DateTime.UtcNow;
    }
}