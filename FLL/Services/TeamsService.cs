using System.Diagnostics.Metrics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System;
using static FLL.Data.Types.JsonHandsOn;

namespace FLL.Services
{
    public class TeamsService
    {
        private string dataPath = "teamdata.json";

        private int cntMatch = 1;
        
        


        public TeamsService()
        {
            if(File.Exists(dataPath)) 

            Teams.Add(new TeamItem(1006, "Robo-Hunter", "c-tech.ch gmbh"));
            Teams.Add(new TeamItem(1010, "Bussibot - Fat Chicken", "EPS Bussigny"));
            Teams.Add(new TeamItem(1017, "Bussibot - Cyber Ghost", "EPS Bussigny"));
            Teams.Add(new TeamItem(1018, "Bussibot - Swama", "EPS Bussigny"));
            Teams.Add(new TeamItem(1028, "mission possible", "Bezirksschule"));
            Teams.Add(new TeamItem(1042, "Capricorns", "Bündner Kantonsschule Chur"));
            Teams.Add(new TeamItem(1071, "FLL-Fluffys", "Verein FLL-Fluffys"));
            Teams.Add(new TeamItem(1148, "i-Girls", "Kooperation phGR & ETHZ"));
            Teams.Add(new TeamItem(1159, "Phœnix", "Phœnix"));
            Teams.Add(new TeamItem(1228, "mindfactory", "mindfactory"));
            Teams.Add(new TeamItem(1363, "ThurTech", "ThurTech"));
            Teams.Add(new TeamItem(1449, "Saint Roch", "Etablissement secondaire de Villamont"));
            Teams.Add(new TeamItem(1152, "PiRRRrrrrCarré", "Club de Sciences et Robotique"));
            Teams.Add(new TeamItem(1172, "Smilebots", "Ated4Kids"));
            Teams.Add(new TeamItem(1436, "Next Generation", "Swisscom"));

            Tables.Add(new TableItem() { ID = 1, Name = "Table 1" });
            Tables.Add(new TableItem() { ID = 2, Name = "Table 2" });

            Rounds.Add(new RoundItem() { ID = 1, Name = "Round 1", StartTime = new DateTime(2023, 03, 04, 12, 10, 0), MinBtwMatch = 6 });
            Rounds.Add(new RoundItem() { ID = 2, Name = "Round 2", StartTime = new DateTime(2023, 03, 04, 13, 15, 0), MinBtwMatch = 6 });
            Rounds.Add(new RoundItem() { ID = 3, Name = "Round 3", StartTime = new DateTime(2023, 03, 04, 14, 20, 0), MinBtwMatch = 6 });
            Rounds.Add(new RoundItem() { ID = 4, Name = "Quarter-finals", StartTime = new DateTime(2023, 03, 04, 15, 25, 0), MinBtwMatch = 6 });
            Rounds.Add(new RoundItem() { ID = 5, Name = "Semi-finals", StartTime = new DateTime(2023, 03, 04, 15, 51, 0), MinBtwMatch = 6 });
            Rounds.Add(new RoundItem() { ID = 6, Name = "Finals", StartTime = new DateTime(2023, 03, 04, 16, 05, 0), MinBtwMatch = 6 });

            BuildRound1(Rounds[0]);
            BuildRound2(Rounds[1]);
            BuildRound3(Rounds[2]);
            BuildRound4(Rounds[3]);
            BuildRound5(Rounds[4]);
            BuildRound6(Rounds[5]);
        }

        private void BuildRound3(RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
        }

        private void BuildRound2(RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
        }
        private void BuildRound4(RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
        }

        private void BuildRound5(RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
        }
        private void BuildRound6(RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(0), Team2 = GetTeam(0) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(0) });
        }

        private TeamItem? GetTeam(int id) => Teams.FirstOrDefault(x => x.ID == id);


        private void BuildRound1(RoundItem round)
        {
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(1363), Team2 = GetTeam(1152) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(1228), Team2 = GetTeam(1010) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(1018), Team2 = GetTeam(1172) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(1006), Team2 = GetTeam(1449) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(1017), Team2 = GetTeam(1436) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(0), Team2 = GetTeam(1028) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[0], Team1 = GetTeam(1071), Team2 = GetTeam(1152) });
            round.Matchs.Add(new MatchItem() { ID = cntMatch++, Table = Tables[1], Team1 = GetTeam(1363), Team2 = GetTeam(1042) });
        }
    }
}
