using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;

namespace FLL.Pages.View
{
    public partial class ViewTeam : IDisposable
    {
        [Parameter] public int? TeamNum { get; set; }

        private ContestMatch? contest;
        [Inject] MatchService Match { get; set; } = null!;

        private Thread? thread;
        private bool isRunning = true;

        private IDisposable? onMatchChange;

        private string? ShownValue;



        public void Dispose()
        {
            onMatchChange?.Dispose();
            isRunning = false;
        }

        protected override void OnInitialized()
        {
            Set.Set("View match", false, FLL.Data.Types.AccessLevels.Viewer);

            if (!ValidateAccess())
                return;

            thread = new Thread(DoWork);
            thread.Start();
           
            base.OnInitialized();
        }

        private void DoWork()
        {
            while (isRunning)
            {
                UpdateValue();

                InvokeAsync(StateHasChanged);
                Thread.Sleep(10000);
            }
        }

        private void UpdateValue()
        {
            double curTime = DateTime.Now.Hour * 60 + DateTime.Now.Minute + 1;
            var round = contest?.Rounds.OrderByDescending(x => x.StartTime).FirstOrDefault(x => x.StartTime.Hour * 60 + x.StartTime.Minute < curTime);
            var match = round?.Matchs.OrderByDescending(x => x.StartTime).FirstOrDefault(x => x.StartTime.Hour * 60 + x.StartTime.Minute < curTime);

            if (TeamNum == 0)
                ShownValue = match?.Table?.Name;
            else if (TeamNum == 1)
                ShownValue = $"{match?.Team1?.Name}-{match?.Team1?.School}";
            else if (TeamNum == 2)
                ShownValue = match?.Team2.FullName;
        }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Set.Contest == null)
                return;
            await OnChange(Set.Contest?.ID ?? 0);
            onMatchChange = Match.OnChange.Subscribe(async (arg) => await OnChange(arg));
            UpdateValue();
        }

        private async Task OnChange(int value)
        {
            if (value == Set.Contest?.ID)
            {
                if (Set.Contest != null)
                {
                    contest = Match.GetContest(Set.Contest.ID);
                }
                await InvokeAsync(StateHasChanged);
            }
        }

    }
}
