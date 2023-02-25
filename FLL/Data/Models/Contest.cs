using FLL.Data.Types;
using FLL.Utils;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLL.Data.Models
{
    public class Contest

    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(10)]
        public string ShortName { get; set; } = null!;

        public DateTimeOffset? DateFrom { get; set; }

        public DateTimeOffset? DateTo { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public Guid AdminGuid { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ViewGuid { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserGuid { get; set; } = Guid.NewGuid();

        [NotMapped]
        public string? IsValid =>
            string.Join("\r\n", new string?[] {
                Name.LengthValidationStr(3, 50),
                ShortName.LengthValidationStr(3, 10),
                
                //DateFrom == null ? "Date is required" : null,
                //DateTo == null ? "Date is required" : null,
            }.Where(x => x != null));

        internal AccessLevels GetAccessLevel(Guid guid)
        {
            if (guid == AdminGuid) return AccessLevels.Admin;
            if (guid == UserGuid) return AccessLevels.Manager;
            if (guid == ViewGuid) return AccessLevels.Viewer;
            return AccessLevels.None;
        }

        [NotMapped] internal string UrlAdmin => $"admin/{ShortName}/{AdminGuid}";
        [NotMapped] internal string UrlUser => $"user/{ShortName}/{UserGuid}";
        [NotMapped] internal string UrlView => $"view/{ShortName}/{ViewGuid}";
    }
}
