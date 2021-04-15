using GamingFramework.ConnectFour.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GamingFramework.ConnectFour
{
    internal class Player
    {
        public List<int> Moves;
        public static Player CreateHumanPlayer(ActivePlayer color, IODevice iODevice)
        {
            return new HumanConsolePlayer(color, iODevice);
        }

        public static Player CreateComputerPlayer(ActivePlayer color, DifficultyLevel difficultyLevel, IODevice iODevice)
        {
            return new ComputerConsolePlayer(color, difficultyLevel, iODevice);
        }

        private readonly ActivePlayer playerColor;
        private readonly IODevice iODevice;

        private Player(ActivePlayer colorJugador, IODevice iODevice)
        {
            if (colorJugador != ActivePlayer.Red && colorJugador != ActivePlayer.Yellow)
                throw new ArgumentOutOfRangeException("playerColor");

            this.playerColor = colorJugador;
            this.iODevice = iODevice;
            this.Moves = new List<int>();
        }

        public ActivePlayer Color { get { return playerColor; } }

        public virtual int RequestMove(Board board)
        {
            return -1;
        }

        private class ComputerConsolePlayer : Player
        {
            private readonly CounterFourAI engine;

            public ComputerConsolePlayer(ActivePlayer color, DifficultyLevel difficulty, IODevice iODevice)
                : base(color, iODevice)
            {
                engine = new CounterFourAI(difficulty);
            }

            public override int RequestMove(Board board)
            {
                Debug.Assert(board != null);

                var move = engine.GetBestMove(board, playerColor);
                iODevice.Output("Player {0}'s turn. Hmmm...I'll play: {1}", playerColor, move);
                iODevice.Output("");
                return move;
            }
        }

        private class HumanConsolePlayer : Player
        {
            public HumanConsolePlayer(ActivePlayer color, IODevice iOdevice)
                : base(color, iOdevice) { }

            public override int RequestMove(Board board)
            {
                Debug.Assert(board != null);

                while (true)
                {
                    var input = (string)iODevice.Request("Player {0}'s turn: ", playerColor);
                    iODevice.Output("");
                    var option = validateMove(input, board);                   
                    if (option != -1)
                    {
                        if (option < 0 || option >= board.BoardColumns)
                        {
                            iODevice.Output($"Column number must be within 0 and {board.BoardColumns - 1}. Try again.");
                        }
                        else
                        {
                            return option;
                        }
                    }
                    else
                    {
                        iODevice.Output("'{0}' is not a column number. Try again.", input);
                    }
                }
            }

            private int validateMove(string input,Board board)
            {
              var option = Helpers.GetGameOptionsFromString(input);
                if (option >= 1000)
                {
                    return RequestMove(board);
                }
                return option;
            }
        }
    }
}
