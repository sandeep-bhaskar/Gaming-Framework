using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamingFramework.ConnectFour.Logic
{
    public class Game
    {
        private Board board;
        private Player humanPlayer1, humanPlayer2, computerPlayer, activePlayer;
        private IODevice iODevice;
        private readonly bool computerPlaysFirst;
        private readonly bool isComputerPlaying;
        public bool IsGameSaved;

        public static Game CreateConsoleGame(DifficultyLevel difficultyLevel, ActivePlayer colorOrdenador, bool computerPlaysFirst, bool isComputerPlaying, int rows, int columns)
        {
            return new ConsoleGame(difficultyLevel, colorOrdenador, computerPlaysFirst, IODevice.CreateConsoleDevice(), isComputerPlaying, rows, columns);
        }

        private Game(DifficultyLevel difficultyLevel, ActivePlayer computerColor, bool computerPlaysFirst, IODevice iODevice, bool isComputerPlaying, int rows, int columns)
        {
            this.board = new Board(rows, columns);
            this.iODevice = iODevice;
            this.isComputerPlaying = isComputerPlaying;
            this.computerPlaysFirst = computerPlaysFirst;
        }

        public ActivePlayer ActivePlayerColor { get { return this.activePlayer.Color; } }
        public Board Board { get { return board; } }
        public IODevice UserInterface { get { return iODevice; } }

        private void changeActivePlayer()
        {
            if (isComputerPlaying)
            {
                if (activePlayer == computerPlayer)
                {
                    activePlayer = humanPlayer1;
                }
                else
                {
                    activePlayer = computerPlayer;
                }
            }
            else
            {
                if (activePlayer == humanPlayer1)
                {
                    activePlayer = humanPlayer2;
                }
                else
                {
                    activePlayer = humanPlayer1;
                }
            }
        }

        public virtual void Play()
        {
          activePlayer.RequestMove(board);
        }

        private class ConsoleGame : Game
        {
            public ConsoleGame(DifficultyLevel difficultyLevel, ActivePlayer computerColor, bool computerPlaysFirst, IODevice iODevice, bool isComputerPlaying, int rows, int columns)
                : base(difficultyLevel, computerColor, computerPlaysFirst, iODevice, isComputerPlaying, rows, columns)
            {
                humanPlayer1 = Player.CreateHumanPlayer(computerColor == ActivePlayer.Red ? ActivePlayer.Yellow : ActivePlayer.Red, iODevice);

                if (isComputerPlaying)
                {
                    computerPlayer = Player.CreateComputerPlayer(computerColor, difficultyLevel, iODevice);
                    if (computerPlaysFirst)
                    {
                        activePlayer = computerPlayer;
                    }
                    else
                    {
                        activePlayer = humanPlayer1;
                    }
                }
                else
                {
                    activePlayer = humanPlayer1;
                    humanPlayer2 = Player.CreateHumanPlayer(humanPlayer1.Color == ActivePlayer.Red ? ActivePlayer.Yellow : ActivePlayer.Red, iODevice);
                }

                this.iODevice = iODevice;
            }

            public override void Play()
            {
                while (true)
                {
                    iODevice.Output("");
                    iODevice.Output(board.ToString());
                    iODevice.Output("");

                    int move = activePlayer.RequestMove(board);

                    if (!board.MakePlay(activePlayer.Color, move, out board))
                    {
                        iODevice.Output("Row is full. Try again.");
                        continue;
                    }

                    if (Referee.CheckForVictory(activePlayer.Color, board))
                    {
                        iODevice.Output(board.ToString());
                        iODevice.Output("");
                        if (isComputerPlaying)
                        {
                            if (activePlayer == computerPlayer)
                            {
                                iODevice.Accept("I'm sorry player {0}. I won again...", humanPlayer1.Color);
                            }
                            else
                            {
                                iODevice.Accept("Congratulations player {0}! ¡You won!", humanPlayer1.Color);
                            }
                        }
                        else
                        {
                            if (activePlayer == humanPlayer1)
                            {
                                iODevice.Accept("I'm sorry player {0}. I won again...", humanPlayer2.Color);
                            }
                            else
                            {
                                iODevice.Accept("Congratulations player {0}! ¡You won!", humanPlayer2.Color);
                            }
                        }
                        break;
                    }

                    if (board.NumberOfEmptyCells == 0)
                    {
                        iODevice.Output(board.ToString());
                        iODevice.Output("");
                        iODevice.Accept("¡Draw! I didnt loose...again");
                        break;
                    }
                    this.activePlayer.Moves.Add(move);
                    changeActivePlayer();
                }
            }
        }
    }
}
