using System;
using System.Collections.Generic;
using System.Text;

namespace GamingFramework.ConnectFour.Logic
{
    public class Board
    {
        public readonly int BoardRows = 6, BoardColumns = 7;

        private readonly CellStates[,] cells;
        private readonly int numberOfEmptyCells;

        public  Board(int boardRows,int boardColumns)
        {
            if (boardRows > 0 && boardColumns > 0)
            {
                BoardRows = boardRows;
                BoardColumns = boardColumns;
            }
            cells = new CellStates[BoardRows, BoardColumns];
            numberOfEmptyCells = BoardRows * BoardColumns;
        }

        private Board(Board board, int numberOfEmptyCells)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (numberOfEmptyCells < 0 || numberOfEmptyCells > BoardRows * BoardColumns)
                throw new ArgumentOutOfRangeException("numberOfEmptyCells");

            cells = new CellStates[BoardRows, BoardColumns];

            if (board != null)
            {
                for (int i = 0; i < BoardRows; i++)
                {
                    for (int j = 0; j < BoardColumns; j++)
                    {
                        cells[i, j] = board.cells[i, j];
                    }
                }
            }

            this.numberOfEmptyCells = numberOfEmptyCells;
        }

        public int NumberOfEmptyCells
        {
            get
            {
                return numberOfEmptyCells;
            }
        }

        public CellStates GetCellState(int row, int column)
        {
            if (row < 0 || row >= BoardRows)
                throw new ArgumentOutOfRangeException("row");

            if (column < 0 || column >= BoardColumns) throw new ArgumentOutOfRangeException("column");

            return cells[row, column];
        }

        public bool MakePlay(ActivePlayer player, int column, out Board board)
        {
            if (column < 0 || column >= BoardColumns) throw new ArgumentOutOfRangeException("column");

            if (cells[0, column] != CellStates.Empty)
            {
                board = this;
                return false;
            }

            board = new Board(this, numberOfEmptyCells - 1);

            int i;

            for (i = BoardRows - 1; i > -1; i--)
            {
                if (cells[i, column] == CellStates.Empty)
                    break;
            }

            board.cells[i, column] = (CellStates)player;
            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var header = string.Empty;
            var divisor = string.Empty;
            for (int i = 0; i < BoardColumns; i++)
            {
               header = $"{header}   {i}";
                divisor = $"{divisor}----";
            }
            builder.AppendLine(header);
            builder.AppendLine(divisor);

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    var str = cells[i, j] == CellStates.Empty ? "| · " : (cells[i, j] == CellStates.Red ? "| X " : "| O ");
                    builder.Append(str);
                }

                builder.Append('|');
                builder.AppendLine();
                builder.AppendLine(divisor);
            }

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
