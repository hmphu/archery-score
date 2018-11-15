using System.Collections.Generic;

namespace ArcheryScore.Classes.Game
{
    public class Default : GameAbstract
    {
        protected override List<ScoreRange> GetGameBoard()
        {
            return new List<ScoreRange>
            {
                new ScoreRange{
                    arrowColor = "#000000",
                    color = "#ffc107",
                    radius = 25,
                    points = 10,
                    strokeColor = "#000000",
                    StrokeWidth = 0.5f
                },
                new ScoreRange{
                    arrowColor = "#000000",
                    color = "#ffc107",
                    radius = 50,
                    points = 10,
                    strokeColor = "#000000"
                },
                new ScoreRange{
                    arrowColor = "#000000",
                    color = "#ffc107",
                    radius = 100,
                    points = 9,
                    strokeColor = "#000000"
                },
                new ScoreRange{
                    arrowColor= "#ffffff",
                    color= "#dc3545",
                    radius= 150,
                    points= 8,
                    strokeColor= "#ffffff"
                },
                new ScoreRange{
                    arrowColor= "#ffffff",
                    color= "#dc3545",
                    radius= 200,
                    points= 7,
                    strokeColor= "#ffffff"
                },
                new ScoreRange{
                    arrowColor= "#ffffff",
                    color= "#17a2b8",
                    radius= 250,
                    points= 6,
                    strokeColor= "#ffffff"
                },
                new ScoreRange{
                    arrowColor= "#ffffff",
                    color= "#17a2b8",
                    radius= 300,
                    points= 5,
                    strokeColor= "#ffffff"
                },
                new ScoreRange{
                    arrowColor= "#ffffff",
                    color= "#000000",
                    radius= 350,
                    points= 4,
                    strokeColor= "#ffffff"
                },
                new ScoreRange{
                    arrowColor= "#ffffff",
                    color= "#000000",
                    radius= 400,
                    points= 3,
                    strokeColor= "#ffffff"
                },
                new ScoreRange{
                    arrowColor= "#000000",
                    color= "#ffffff",
                    radius= 450,
                    points= 2,
                    strokeColor= "#000000"
                },
                new ScoreRange{
                    arrowColor= "#000000",
                    color= "#ffffff",
                    radius= 500,
                    points= 1,
                    strokeColor= "#000000"
                }
            };
        }
    }
}
