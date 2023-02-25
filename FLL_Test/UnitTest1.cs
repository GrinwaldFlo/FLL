using FLL.Data.Types;
using Newtonsoft.Json;

namespace FLL_Test
{
    public class UnitTest1
    {
        [Fact]
        public void WriteJson()
        {
            JsonHandsOn json = new()
            {
                Teams = new()
                {
                    new(){Name = "Team 1", Id = 1000},
                    new(){Name = "Team 2", Id = 1001},
                    new(){Name = "Team 3", Id = 1002},
                    new(){Name = "Team 4", Id = 1003},
                },
                Rounds = new List<JsonHandsOn.Round>()
                {
                    new JsonHandsOn.Round()
                    {
                        Name = "PRELIMINARY ROUND I",
                        Matches = new List<JsonHandsOn.Round.Match>()
                        {
                            new JsonHandsOn.Round.Match()
                            {
                                TeamId1 = 1001,  
                                Score1 = 789,
                                TeamId2 = 1003,
                                Score2 = 111
                            },
                            new JsonHandsOn.Round.Match()
                            {
                                TeamId1 = 1000,
                                Score1 = 44,
                                TeamId2 = 1002,
                                Score2 = 11
                            }
                        }
                    },
                    new JsonHandsOn.Round()
                    {
                        Name = "PRELIMINARY ROUND II",
                        Matches = new List<JsonHandsOn.Round.Match>()
                        {
                            new JsonHandsOn.Round.Match()
                            {
                                TeamId1 = 1000,

                                TeamId2 = 1001,
                                
                            },
                            new JsonHandsOn.Round.Match()
                            {
                                TeamId1 = 1002,

                                TeamId2 = 1003,
                                
                            }
                        }
                    }
                }
            };

            File.WriteAllText("jsonSample1.json", JsonConvert.SerializeObject(json, Formatting.Indented));
        }
    }
}