using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System;
using static FLL.Data.Types.JsonHandsOn;
using FLL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Subjects;

namespace FLL.Services
{
    public class MatchService
    {
        readonly ApplicationDbContext Db;
        readonly object lockDb = new();

        public Subject<int> OnChange { get; private set; } = new Subject<int>();

        public MatchService(IServiceProvider serviceProvider)
        {
            Db = serviceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
        }

        public bool SaveDb()
        {
            lock (lockDb)
            {
                return Db.SaveChanges() > 0;
            }
        }

        internal ContestMatch? GetContest(int contestID)
        {
            lock (lockDb)
            {
                var curContestMatch = Db.ContestMatches
                    .Include(x => x.Tables)
                    .Include(x => x.Rounds).ThenInclude(x => x.Matchs).ThenInclude(x => x.Team1)
                    .Include(x => x.Rounds).ThenInclude(x => x.Matchs).ThenInclude(x => x.Team2)
                    .Include(x => x.Rounds).ThenInclude(x => x.Matchs).ThenInclude(x => x.Table)
                    .Include(x => x.Teams)
                    .FirstOrDefault(x => x.ContestID == contestID);

                return curContestMatch;
            }
        }

        internal ContestMatch? CreateContest(int contestID, string name)
        {
            var curContestMatch = GetContest(contestID);
            if (curContestMatch != null)
                return curContestMatch;

            lock (lockDb)
            {
                switch (name)
                {
                    case "FLL_SWISS_2023":
                        curContestMatch = CreateFllSwiss2023(contestID);
                        break;
                    default:
                        break;
                }
                if (curContestMatch != null)
                {
                    Db.ContestMatches.Add(curContestMatch);
                    Db.SaveChanges();
                }
            }
            return curContestMatch;
        }


        private static ContestMatch CreateFllSwiss2023(int contestID)
        {
            ContestMatch c = new()
            {
                ContestID = contestID
            };
            c.Teams.Add(new() { TeamId = 1006, Name = "Robo-Hunter", School = "c-tech.ch gmbh" });
            c.Teams.Add(new() { TeamId = 1010, Name = "Bussibot - Fat Chicken", School = "EPS Bussigny" });
            c.Teams.Add(new() { TeamId = 1017, Name = "Bussibot - Cyber Ghost", School = "EPS Bussigny" });
            c.Teams.Add(new() { TeamId = 1018, Name = "Bussibot - Swama", School = "EPS Bussigny" });
            c.Teams.Add(new() { TeamId = 1028, Name = "mission possible", School = "Bezirksschule" });
            c.Teams.Add(new() { TeamId = 1042, Name = "Capricorns", School = "Bündner Kantonsschule Chur" });
            c.Teams.Add(new() { TeamId = 1071, Name = "FLL-Fluffys", School = "Verein FLL-Fluffys" });
            c.Teams.Add(new() { TeamId = 1148, Name = "i-Girls", School = "Kooperation phGR & ETHZ" });
            c.Teams.Add(new() { TeamId = 1159, Name = "Phœnix", School = "Phœnix" });
            c.Teams.Add(new() { TeamId = 1228, Name = "mindfactory", School = "mindfactory" });
            c.Teams.Add(new() { TeamId = 1363, Name = "ThurTech", School = "ThurTech" });
            c.Teams.Add(new() { TeamId = 1449, Name = "Saint Roch", School = "Etablissement secondaire de Villamont" });
            c.Teams.Add(new() { TeamId = 1152, Name = "PiRRRrrrrCarré", School = "Club de Sciences et Robotique" });
            c.Teams.Add(new() { TeamId = 1172, Name = "Smilebots", School = "Ated4Kids" });
            c.Teams.Add(new() { TeamId = 1436, Name = "Next Generation", School = "Swisscom" });

            c.Tables.Add(new TableItem() { TableId = 1, Name = "Table 1" });
            c.Tables.Add(new TableItem() { TableId = 2, Name = "Table 2" });

            c.Rounds.Add(new RoundItem() { RoundId = 1, Name = "Round 1", StartTime = new DateTime(2023, 03, 04, 12, 10, 0), MinBtwMatch = 6 });
            c.Rounds.Add(new RoundItem() { RoundId = 2, Name = "Round 2", StartTime = new DateTime(2023, 03, 04, 13, 15, 0), MinBtwMatch = 6 });
            c.Rounds.Add(new RoundItem() { RoundId = 3, Name = "Round 3", StartTime = new DateTime(2023, 03, 04, 14, 20, 0), MinBtwMatch = 6 });
            c.Rounds.Add(new RoundItem() { RoundId = 4, Name = "Quarter-finals", StartTime = new DateTime(2023, 03, 04, 15, 25, 0), MinBtwMatch = 6 });
            c.Rounds.Add(new RoundItem() { RoundId = 5, Name = "Semi-finals", StartTime = new DateTime(2023, 03, 04, 15, 51, 0), MinBtwMatch = 6 });
            c.Rounds.Add(new RoundItem() { RoundId = 6, Name = "Finals", StartTime = new DateTime(2023, 03, 04, 16, 05, 0), MinBtwMatch = 6 });

            BuildRound1(c, c.Rounds.First(x => x.RoundId == 1));
            BuildRound2(c, c.Rounds.First(x => x.RoundId == 2));
            BuildRound3(c, c.Rounds.First(x => x.RoundId == 3));
            BuildRound4(c, c.Rounds.First(x => x.RoundId == 4));
            BuildRound5(c, c.Rounds.First(x => x.RoundId == 5));
            BuildRound6(c, c.Rounds.First(x => x.RoundId == 6));

            foreach (var round in c.Rounds)
            {
                UpdateTime(round);
            }
            return c;
        }

        internal static void UpdateTime(RoundItem round)
        {
            DateTime time = round.StartTime;
            foreach (var match in round.Matchs)
            {
                match.StartTime = time;
                time = time.AddMinutes(round.MinBtwMatch);
            }
        }


        private static void BuildRound1(ContestMatch c, RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1363), Team2 = c.GetTeam(1152) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1228), Team2 = c.GetTeam(1010) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1018), Team2 = c.GetTeam(1172) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1006), Team2 = c.GetTeam(1449) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1017), Team2 = c.GetTeam(1436) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(0), Team2 = c.GetTeam(1028) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1071), Team2 = c.GetTeam(1152) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1363), Team2 = c.GetTeam(1042) });
        }

        private static void BuildRound3(ContestMatch c, RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1159), Team2 = c.GetTeam(1148) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1028), Team2 = c.GetTeam(1006) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1172), Team2 = c.GetTeam(1228) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1152), Team2 = c.GetTeam(1071) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1363), Team2 = c.GetTeam(1018) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1010), Team2 = c.GetTeam(0) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1159), Team2 = c.GetTeam(1436) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1148), Team2 = c.GetTeam(1449) });
        }

        private static void BuildRound2(ContestMatch c, RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1017), Team2 = c.GetTeam(1042) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(0), Team2 = c.GetTeam(1228) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1028), Team2 = c.GetTeam(1071) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1436), Team2 = c.GetTeam(1172) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1449), Team2 = c.GetTeam(1006) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1018), Team2 = c.GetTeam(1159) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(1010), Team2 = c.GetTeam(1017) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(1148), Team2 = c.GetTeam(1042) });
        }
        private static void BuildRound4(ContestMatch c, RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
        }

        private static void BuildRound5(ContestMatch c, RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
        }
        private static void BuildRound6(ContestMatch c, RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { Table = c.Tables[0], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
            round.Matchs.Add(new MatchItem() { Table = c.Tables[1], Team1 = c.GetTeam(0), Team2 = c.GetTeam(0) });
        }
    }
}
