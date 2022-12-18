namespace BGLibrary.Models
{
    public class Plot
    {

        protected List<string> intColumns = new List<string>()
        {
            "MaxPlayers", "MaxPlayTime", "MinPlayers",
            "MinPlayTime", "YearPublished", "Average",
            "AverageWeight"
        };

        public int LeftLim { get; set; }
        public int RightLim { get; set; }

        public bool isValidLimits() => LeftLim < RightLim;
    }


    public class Count : Plot
    {
        public string Xlabel { get; set; }
        public bool isValidLabel() => this.intColumns.Contains(Xlabel);
    }

    public class AVG : Plot
    {
        public string Ylabel { get; set; }
        public bool isValidLabel() => this.intColumns.Contains(Ylabel);
    }
            
    public class TimeBased : Plot
    {
        public string Ylabel { get; set; }
        public string Value { get; set; }
        public bool isValidLabel() => this.intColumns.Contains(Ylabel);
    }
}