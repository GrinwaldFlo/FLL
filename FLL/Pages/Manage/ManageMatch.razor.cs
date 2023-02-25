﻿using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace FLL.Pages.Manage
{
    public partial class ManageMatch : IDisposable
    {

        [Inject] MatchService Match { get; set; } = null!;

        private ContestMatch? contest;
        private IDisposable? onMatchChange;

        protected override void OnInitialized()
        {
            Set.Set("Manage match contest", true, FLL.Data.Types.AccessLevels.Manager);

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

        private void ChangeTeam1(MatchItem match, object newTeam)
        {
            if (newTeam is TeamItem team)
            {
                match.Team1 = team;
                Match.SaveDb();
            }
            else if (newTeam is null)
            {
                match.Team1 = null;
                Match.SaveDb();
            }
            if (Set.Contest != null)
                Match.OnChange.OnNext(Set.Contest.ID);
        }
        private void ChangeTeam2(MatchItem match, object newTeam)
        {
            if (newTeam is TeamItem team)
            {
                match.Team2 = team;
                Match.SaveDb();
            }
            else if (newTeam is null)
            {
                match.Team2 = null;
                Match.SaveDb();
            }
            if(Set.Contest != null)
                Match.OnChange.OnNext(Set.Contest.ID);
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