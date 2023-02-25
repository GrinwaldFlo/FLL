using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace FLL.Data.Models
{
    public class Chrono
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Name { get; set; }
        public int DurationS { get; set; }

        public int ContestID { get; set; }
        public virtual Contest? Contest { get; set; }

        [NotMapped]
        internal bool Found { get; set; }

        [NotMapped]
        public bool Running => Stopwatch.IsRunning;

        [NotMapped]
        public bool IsVisible { get; set; }

        [NotMapped]
        internal Stopwatch Stopwatch { get; set; } = new();

        [NotMapped]
        public double Remaining => Math.Max(DurationS - Stopwatch.ElapsedMilliseconds / 1000.0, 0);

        internal void Reset()
        {
            Stopwatch.Reset();
        }
        internal void Start()
        {
            Stopwatch.Start();
        }

        internal void Stop()
        {
            Stopwatch.Stop();
        }

        internal void Show()
        {
            IsVisible = true;
        }

        internal void Hide()
        {
            IsVisible = false;
        }
    }
}
