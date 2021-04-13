using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GamingFramework.ConnectFour.Logic
{
        internal static class Referee
        {
            public const int requiredCellsInARow = 4;

            public static bool CheckForVictory(ActivePlayer player, Board tablero)
            {
                if (tablero == null)
                    throw new ArgumentNullException("board");

                for (int i = 0; i < tablero.BoardRows; i++)
                {
                    for (int j = 0; j < tablero.BoardColumns; j++)
                    {
                        if (tablero.GetCellState(i, j) == (CellStates)player)
                        {
                            if (checkForVictory(tablero, i, j))
                                return true;
                        }
                    }
                }

                return false;
            }

            private static bool checkForVictory(Board board, int fila, int columna)
            {
                bool searchRight, searchLeft, searchUp, searchDown;

                searchRight = columna <= board.BoardColumns - requiredCellsInARow;
                searchLeft = columna >= requiredCellsInARow - 1;
                searchUp = fila > board.BoardRows - requiredCellsInARow;
                searchDown = fila <= board.BoardRows - requiredCellsInARow;

                if (searchRight)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila, columna + 1),
                                        board.GetCellState(fila, columna + 2),
                                        board.GetCellState(fila, columna + 3)))
                        return true;
                }

                if (searchLeft)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila, columna - 1),
                                        board.GetCellState(fila, columna - 2),
                                        board.GetCellState(fila, columna - 3)))
                        return true;
                }

                if (searchUp)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila - 1, columna),
                                        board.GetCellState(fila - 2, columna),
                                        board.GetCellState(fila - 3, columna)))
                        return true;
                }

                if (searchDown)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila + 1, columna),
                                        board.GetCellState(fila + 2, columna),
                                        board.GetCellState(fila + 3, columna)))
                        return true;
                }

                if (searchLeft && searchUp)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila - 1, columna - 1),
                                        board.GetCellState(fila - 2, columna - 2),
                                        board.GetCellState(fila - 3, columna - 3)))
                        return true;
                }

                if (searchLeft && searchDown)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila + 1, columna - 1),
                                        board.GetCellState(fila + 2, columna - 2),
                                        board.GetCellState(fila + 3, columna - 3)))
                        return true;
                }

                if (searchRight && searchUp)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila - 1, columna + 1),
                                        board.GetCellState(fila - 2, columna + 2),
                                        board.GetCellState(fila - 3, columna + 3)))
                        return true;
                }

                if (searchRight && searchDown)
                {
                    if (checkCells(board.GetCellState(fila, columna),
                                        board.GetCellState(fila + 1, columna + 1),
                                        board.GetCellState(fila + 2, columna + 2),
                                        board.GetCellState(fila + 3, columna + 3)))

                        return true;
                }

                return false;
            }

            private static bool checkCells(params CellStates[] celdas)
            {
                Debug.Assert(celdas.Length == requiredCellsInARow);

                for (int i = 1; i < requiredCellsInARow; i++)
                {
                    if (celdas[i] != celdas[0])
                        return false;
                }

                return true;
            }
        }

    }
