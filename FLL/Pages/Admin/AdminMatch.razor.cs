using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace FLL.Pages.Admin
{
    public partial class AdminMatch : IDisposable
    {

        [Inject] MatchService Match { get; set; } = null!;

        private ContestMatch? contest;
        private IDisposable? onMatchChange;

        protected override void OnInitialized()
        {
            Set.Set("Create match contest", true, FLL.Data.Types.AccessLevels.Admin);

            if (!ValidateAccess())
                return;
            base.OnInitialized();

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await OnChange(Set.Contest?.ID ?? 0);
            onMatchChange = Match.OnChange.Subscribe(async (arg) => await OnChange(arg));
        }

        private void CreateContest(string name)
        {
            if (Set.Contest == null)
                return;
            contest = Match.CreateContest(Set.Contest.ID, name);
            Match.OnChange.OnNext(Set.Contest.ID);
            StateHasChanged();
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                onMatchChange?.Dispose();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
