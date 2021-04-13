using System;
using System.Collections.Generic;
using System.Text;

namespace GamingFramework.ConnectFour.Logic
{
    internal sealed class CounterFourAI
    {
        readonly int maximumDepth;
        readonly Random random;

        public CounterFourAI(DifficultyLevel difficultyLevel)
        {
            this.maximumDepth = (int)difficultyLevel;

            if (maximumDepth < (int)DifficultyLevel.Easy ||
                maximumDepth > (int)DifficultyLevel.Hard)
                throw new ArgumentOutOfRangeException("difficultyLevel");

            this.random = new Random(DateTime.Now.Millisecond);
        }

        public int GetBestMove(Board board, ActivePlayer player)
        {
            if (board == null)
                throw new ArgumentNullException("board");

            var node = new Node(board);
            var possibleMoves = getPossibleMoves(node);
            var scores = new double[possibleMoves.Count];
            Board updatedBoard;

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                board.MakePlay(player, possibleMoves[i], out updatedBoard);
                var variant = new Node(updatedBoard);
                createTree(getOpponent(player), variant, 0);
                scores[i] = scoreNode(variant, player, 0);
            }

            double maximumScore = double.MinValue;
            var goodMoves = new List<int>();

            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > maximumScore)
                {
                    goodMoves.Clear();
                    goodMoves.Add(i);
                    maximumScore = scores[i];
                }
                else if (scores[i] == maximumScore)
                {
                    goodMoves.Add(i);
                }
            }

            return possibleMoves[goodMoves[random.Next(0, goodMoves.Count)]];
        }

        private List<int> getPossibleMoves(Node node)
        {
            var moves = new List<int>();

            for (int i = 0; i < node.Board.BoardColumns; i++)
            {
                if (node.Board.GetCellState(0, i) == CellStates.Empty)
                {
                    moves.Add(i);
                }
            }

            return moves;
        }

        private void createTree(ActivePlayer player, Node rootNode, int depth)
        {
            if (depth >= maximumDepth)
                return;

            var moves = getPossibleMoves(rootNode);

            foreach (var move in moves)
            {
                Board updatedBoard;
                rootNode.Board.MakePlay(player, move, out updatedBoard);
                var variantNode = new Node(updatedBoard);
                createTree(getOpponent(player), variantNode, depth + 1);
                rootNode.Variants.Add(variantNode);
            }
        }

        private double scoreNode(Node nodo, ActivePlayer player, int depth)
        {
            double score = 0;

            if (Referee.CheckForVictory(player, nodo.Board))
            {
                if (depth == 0)
                {
                    score = double.PositiveInfinity;
                }
                else
                {
                    score += Math.Pow(10.0, maximumDepth - depth);
                }
            }
            else if (Referee.CheckForVictory(getOpponent(player), nodo.Board))
            {
                score += -Math.Pow(100
                    , maximumDepth - depth);
            }
            else
            {
                foreach (var varianteContrincante in nodo.Variants)
                {
                    score += scoreNode(varianteContrincante, player, depth + 1);
                }
            }

            return score;
        }

        private static ActivePlayer getOpponent(ActivePlayer jugador)
        {
            return jugador == ActivePlayer.Red ? ActivePlayer.Yellow : ActivePlayer.Red;
        }

        private class Node
        {
            readonly Board board;
            readonly List<Node> variants;

            public Board Board { get { return board; } }
            public List<Node> Variants { get { return variants; } }

            public Node(Board tablero)
            {
                this.board = tablero;
                this.variants = new List<Node>();
            }
        }
    }
}
