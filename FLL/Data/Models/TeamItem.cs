namespace FLL.Data.Models
{
    public class TeamItem
    {
        public int ID { get; set; }
        public int TeamId { get; set; }
        public string? Name { get; set; }
        public string? School { get; set; }

        public override string ToString()
        {
            return $"{TeamId}-{Name}-{School}"; 
        }
    }
}
