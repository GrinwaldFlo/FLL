using static FLL.Services.MatchService;

namespace FLL.Data.Models
{
    public class MatchItem
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public TableItem? Table { get; set; }
        public TeamItem? Team1 { get; set; }
        public TeamItem? Team2 { get; set; }
    }
}
