using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using System;
using static FLL.Data.Types.JsonHandsOn;

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

        private string GetPath(RoundItem round)
        {
            return $"/view/match/{Set.Contest?.ShortName}/{Set.Contest?.ViewGuid}/{round.RoundId}";
        }

        private string GetPath(string item)
        {
            return item switch
            {
                "Team1" => $"/view/team/{Set.Contest?.ShortName}/{Set.Contest?.ViewGuid}/1",
                "Team2" => $"/view/team/{Set.Contest?.ShortName}/{Set.Contest?.ViewGuid}/2",
                "Table" => $"/view/team/{Set.Contest?.ShortName}/{Set.Contest?.ViewGuid}/0",
                _ => ""
            };
        }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await OnChange(Set.Contest?.ID ?? 0);
            onMatchChange = Match.OnChange.Subscribe(async (arg) => await OnChange(arg));
        }

        private void ChangeTable(MatchItem match, object newTable)
        {
            if (newTable is TableItem table)
            {
                match.Table = table;
                Match.SaveDb();
                if (Set.Contest != null)
                    Match.OnChange.OnNext(Set.Contest.ID);
            }
        }

        private void ChangeTime(RoundItem round, DateTime? newTime)
        {
            if (newTime is not null)
            {
                round.StartTime = newTime.Value;
                MatchService.UpdateTime(round);
                Match.SaveDb();
                if (Set.Contest != null)
                    Match.OnChange.OnNext(Set.Contest.ID);
            }
        }

        private void ChangeMinBtwMatch(RoundItem round, double? newMinBtwMatch)
        {
            if (newMinBtwMatch is not null)
            {
                round.MinBtwMatch = newMinBtwMatch.Value;
                Match.SaveDb();
                if (Set.Contest != null)
                    Match.OnChange.OnNext(Set.Contest.ID);
            }
        }

        private void ChangeTime(RoundItem round, MatchItem match, DateTime? newTime)
        {
            if (newTime is not null)
            {
                match.StartTime = newTime.Value;

                bool startUpdateTime = false;
                MatchItem? lastMatch = null;
                foreach (var matchItem in round.Matchs)
                {
                    if (match.ID == matchItem.ID)
                    {
                        startUpdateTime = true;
                        lastMatch = matchItem;
                    }
                    else if(startUpdateTime)
                    {
                        if(lastMatch != null)
                            matchItem.StartTime = lastMatch.StartTime.AddMinutes(round.MinBtwMatch);
                        lastMatch = matchItem;
                    }
                }

                Match.SaveDb();
                if (Set.Contest != null)
                    Match.OnChange.OnNext(Set.Contest.ID);
            }
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
            if (Set.Contest != null)
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
