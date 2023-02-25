namespace FLL.Data.Types
{
    public class JsonHandsOn
    {
        public class Team
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }

        public List<Team>? Teams { get; set; }

        public class Round
        {
            public string? Name { get; set; }
            public List<Match>? Matches { get; set; }
            public class Match
            {
             
                public int? TeamId1 { get; set; }
                public int? Score1 { get; set; }
                public int? TeamId2 { get; set; }
                public int? Score2 { get; set; }
            }
        }
        public List<Round>? Rounds { get; set; }
    }
}
