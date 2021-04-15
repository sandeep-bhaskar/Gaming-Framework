using System;
using System.Collections.Generic;
using System.Text;

namespace GamingFramework.ConnectFour.Logic
{
    public class Board
    {
        public int BoardRows { get; set; }
        public int BoardColumns { get; set; }

        private readonly CellStates[,] cells;
        private readonly int numberOfEmptyCells;

        public  Board(int boardRows,int boardColumns)
        {
            if (boardRows > 0 && boardColumns > 0)
            {
                this.BoardRows = boardRows;
                this.BoardColumns = boardColumns;
            }
            cells = new CellStates[BoardRows, BoardColumns];
            numberOfEmptyCells = BoardRows * BoardColumns;
        }

        private Board(Board board, int numberOfEmptyCells,int rows ,int cols)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            if (numberOfEmptyCells < 0 || numberOfEmptyCells > rows * cols)
                throw new ArgumentOutOfRangeException("numberOfEmptyCells");

            cells = new CellStates[rows, cols];

            if (board != null)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        cells[i, j] = board.cells[i, j];
                    }
                }
            }

            this.numberOfEmptyCells = numberOfEmptyCells;
            this.BoardRows = rows;
            this.BoardColumns = cols;
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

            if (column < 0 || column >= this.BoardColumns) throw new ArgumentOutOfRangeException("column");

            return cells[row, column];
        }

        public bool MakePlay(ActivePlayer player, int column, out Board board)
        {
            if (column < 0 || column >= this.BoardColumns) throw new ArgumentOutOfRangeException("column");

            if (cells[0, column] != CellStates.Empty)
            {
                board = this;
                return false;
            }

            board = new Board(this, numberOfEmptyCells - 1,this.BoardRows,this.BoardColumns);

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
            for (int i = 0; i < this.BoardColumns; i++)
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
                    var str = string.Empty;
                    if (cells[i, j] == CellStates.Empty) 
                    {
                        str = "| · ";
                    } else if (cells[i, j] == CellStates.Red)
                    {
                        str = "| X ";
                    } else {
                        str = "| O ";
                    }

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
