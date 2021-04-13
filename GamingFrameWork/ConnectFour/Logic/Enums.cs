using System;

namespace GamingFramework
{
    public enum ActivePlayer
    {
        Yellow = 1,
        Red = 2
    }

    public enum CellStates
    {
        Empty = 0,
        Yellow = ActivePlayer.Yellow,
        Red = ActivePlayer.Red
    }

    public enum DifficultyLevel
    {
        Easy = 1,
        Medium = 3,
        Hard = 4
    }

    public enum GameOptions
    {
        NewGame = 1000,
        Save = 1001,
        Resume = 1002,
        Help = 1003,
        Exit = 1004
    }
}