

namespace FLL.Data.Models
{
    public class RoundItem
    {
        public int ID { get; set; }
        public int RoundId { get; set; }
        public string? Name { get; set; }

        public DateTime StartTime { get; set; }
        public double MinBtwMatch { get; set; }

        public List<MatchItem> Matchs { get; private set; } = new List<MatchItem>();
    }
}
