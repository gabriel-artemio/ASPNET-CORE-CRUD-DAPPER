namespace ApiDotnet_TCC.Models
{
    public class DadosGeladeira
    {
        public int id { get; set; }
        public double temp1 { get; set; }
        public double temp2 { get; set; }
        public double tempExterna { get; set; }
        public bool porta { get; set; }
        public int processado { get; set; }
        public DateTime timestamp { get; set; } = DateTime.Now;
        public int hora { get; set; } = DateTime.Now.Hour;
    }
}