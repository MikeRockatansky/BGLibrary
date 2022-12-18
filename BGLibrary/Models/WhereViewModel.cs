using Microsoft.AspNetCore.Components.Forms;

namespace BGLibrary.Models
{
    public class WhereViewModel
    {
        public string? Operator { get; set; }
        public string? Column { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }

        public bool IsValid()
        {
            if (Value != null && Operator != "Null")
                if(Type == "string" && !int.TryParse(Value, out int number1) || Type == "int" && int.TryParse(Value, out int number2))
                    return true;
            return false;
        }
    }
}