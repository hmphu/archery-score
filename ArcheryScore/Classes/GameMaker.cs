using System;
using System.Diagnostics.Contracts;
using ArcheryScore.Classes;

namespace ArcheryScore.Classes
{
    public static class GameMaker
    {
        public static IGame CreateGame(GameType t )
        {
            switch (t){
                case GameType.Default:
                    return new Game.Default();
                case GameType.Easy:
                    return new Game.Easy();
                default:
                    return new Game.Default();
            }
        }
    }
}
