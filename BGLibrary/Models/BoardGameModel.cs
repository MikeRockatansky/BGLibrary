using System.Text.Json.Serialization;

namespace BGLibrary.Models
{
    public class BoardGame
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public int? MaxPlayers { get; set; }
        public int? MaxPlayTime { get; set; }
        public int? MinAge { get; set; }
        public int? MinPlayers { get; set; }
        public int? MinPlayTime { get; set; }
        public string? Name { get; set; }
        public int? YearPublished { get; set; }
        public int? Description { get; set; }

        public override string ToString()
        {
            return $"{Name} - {YearPublished}";
        }
    }
    public class BoardGamePage
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? YearPublished { get; set; }
        public string? BoardGameArtist { get; set; }
        public string? BoardGameDesigner { get; set; }
    }
}