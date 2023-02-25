namespace FLL.Data.Models
{
    public class ContestMatch
    {
        public int ID { get; set; }

        public int ContestID { get; set; }
        public virtual Contest? Contest { get; set; }


        public List<RoundItem> Rounds { get; set; } = new();
    }
}
