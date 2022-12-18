namespace BGLibrary.Models
{
    public class OrderByViewModel
    {
        public string? Operator { get; set; }
        public string? Column { get; set; }

        public bool IsValid() => Operator != null && Column != null;
    }
}