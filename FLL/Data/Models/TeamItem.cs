using System.ComponentModel.DataAnnotations.Schema;

namespace FLL.Data.Models
{
    public class TeamItem
    {
        public int ID { get; set; }
        public int TeamId { get; set; }
        public string? Name { get; set; }
        public string? School { get; set; }

        [NotMapped]
        public string? FullName => Name == School || School == null ? Name : $"{Name}-{School}";

        public override string ToString()
        {
            return $"{TeamId}-{Name}-{School}"; 
        }
    }
}
