using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;

namespace FLL.Pages.View
{
    public partial class ViewMatch : IDisposable
    {
        [Parameter] public int? RoundId { get; set; }

        private ContestMatch? contest;
        private RoundItem? round => contest?.Rounds.FirstOrDefault(x => x.RoundId == RoundId);
        [Inject] MatchService Match { get; set; } = null!;

        private IDisposable? onMatchChange;

        public void Dispose()
        {
            onMatchChange?.Dispose();
        }

        protected override void OnInitialized()
        {
            Set.Set("View match", false, FLL.Data.Types.AccessLevels.Viewer);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Set.Contest == null)
                return;
            await OnChange(Set.Contest?.ID ?? 0);
            onMatchChange = Match.OnChange.Subscribe(async (arg) => await OnChange(arg));
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
