namespace API.Classes
{
    public class JsonResult
    {
        public decimal P1S { get; set; }
        public decimal P2S { get; set; }
        public decimal P3S { get; set; }
        public decimal M1S { get; set; }
        public decimal M2S { get; set; }
        public decimal M3S { get; set; }
        public decimal X { get; set; }
        public decimal S { get; set; }
        public Dictionary<DateTime, decimal> ResultDict { get; set; } 
        public decimal Coef { get; set; }

    }
}
