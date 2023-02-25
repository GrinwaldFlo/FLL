namespace FLL.Data.Models
{
    public class ContestMatch
    {
        public int ID { get; set; }

        public int ContestID { get; set; }
        public virtual Contest? Contest { get; set; }


        public List<RoundItem> Rounds { get; set; } = new();
        public List<TeamItem> Teams { get; set; } = new();
        public List<TableItem> Tables { get; set; } = new();

        internal TeamItem? GetTeam(int v)
        {
            return Teams.FirstOrDefault(x => x.TeamId == v);
        }
    }
}
