using FLL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace FLL.Services
{
    public class ChronoService : IDisposable
    {
        private readonly List<Chrono> chronos = new();
        private readonly DataService data;
        private bool LockUpdateChrono = false;

        private readonly Thread thread;
        private bool isRunning = true;
        public bool AskUpdate { get; set; } = true;

        public Subject<bool> OnChange { get; private set; } = new Subject<bool>();

        public ChronoService(DataService data)
        {
            this.data = data;
            thread = new Thread(DoWork);
            thread.Start();
        }

        private void DoWork()
        {
            while (isRunning)
            {
                if (AskUpdate)
                {
                    AskUpdate = false;
                    UpdateChrono();
                }

                OnChange.OnNext(true);
                Thread.Sleep(200);
            }


        }

        private void UpdateChrono()
        {
            if (!LockUpdateChrono)
            {
                LockUpdateChrono = true;
                try
                {
                    chronos.ForEach(x => x.Found = false);
                    var newChronos = data.Db.Chronos.AsNoTracking().ToList();
                    foreach (var newChrono in newChronos)
                    {
                        var oldChrono = chronos.FirstOrDefault(x => x.ID == newChrono.ID);
                        if (oldChrono == null)
                        {
                            newChrono.Found = true;
                            chronos.Add(newChrono);
                        }
                        else
                        {
                            oldChrono.Name = newChrono.Name;
                            oldChrono.DurationS = newChrono.DurationS;
                            oldChrono.Found = true;
                        }
                    }
                    chronos.RemoveAll(x => !x.Found);
                }
                finally
                {
                    LockUpdateChrono = false;
                }
            }
        }

        public List<Chrono> GetChronos(Contest contest)
        {
            return chronos.Where(x => x.ContestID == contest.ID).ToList();
        }


        public void Reset(Chrono chrono)
        {
            chronos.FirstOrDefault(x => x.ID == chrono.ID)?.Reset();
        }

        public void Start(Chrono chrono)
        {
            chronos.FirstOrDefault(x => x.ID == chrono.ID)?.Start();
        }
        public void Stop(Chrono chrono)
        {
            chronos.FirstOrDefault(x => x.ID == chrono.ID)?.Stop();
        }

        public void Show(Chrono chrono)
        {
            chronos.FirstOrDefault(x => x.ID == chrono.ID)?.Show();
        }

        public void Hide(Chrono chrono)
        {
            chronos.FirstOrDefault(x => x.ID == chrono.ID)?.Hide();
        }

        public void Dispose()
        {
            isRunning = false;
        }

        internal Chrono? GetChrono(Contest contest, int? chronoID)
        {
            if (chronoID == null)
                return null;
            return chronos.FirstOrDefault(x => x.ContestID == contest.ID && x.ID == chronoID);
        }
    }
}
