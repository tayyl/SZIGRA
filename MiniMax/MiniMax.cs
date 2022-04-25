
using Enums;
using System.Linq;
using SZIGRA.Abstract;

namespace Minimax
{
    public class MiniMax : IAlgorithm
    {
        int Alpha { get; set; }
        int Beta { get; set; }
        int ScoreIncrement { get; set; }
        int MaxDepth { get; set; }
        public MiniMax(int alpha, int beta, int scoreIncrement, int maxDepth)
        {
            Alpha = alpha;
            Beta = beta;    
            ScoreIncrement = scoreIncrement;
        }
        public int FindBestMove(IBoard board)
        {
            var bestMove = -1;
            var bestScore = int.MinValue;

            foreach (var index in board.GetEmptyFields())
            {
                var newBoard = board.Copy();     
                newBoard[index] = FieldStateEnum.OccupiedByComputer;
                var score = -AlphaBetaPruning(board, machineMove: false, depth: 0, -Beta, -Alpha);
                

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = index;
                }
            }

            return bestMove;
        }
        private int AlphaBetaPruning(IBoard board, bool machineMove, int depth, int alpha, int beta)
        {
            var winner = board.GetWinner();
            if (winner != WinnerEnum.None || depth == MaxDepth)
                return GetScoreForWinner(winner, machineMove, depth);

            var emptyFields = board.GetEmptyFields();
            if (!emptyFields.Any())
                return 0;

            var maxScore = alpha;
            foreach (int index in board.GetEmptyFields())
            {
                var newBoard = board.Copy();     
                newBoard[index] = machineMove ? FieldStateEnum.OccupiedByComputer : FieldStateEnum.OccupiedByPlayer;
                var score = -AlphaBetaPruning(newBoard, !machineMove, depth + 1, -beta, -maxScore);

                if (score > maxScore)
                {
                    maxScore = score;

                    if (maxScore >= beta)
                        break;
                }
            }

            return maxScore;
        }
        //osobny intefejs - uogolnic?
        //zaleznie od gry powinna sie odbyc inna ewaluacja?
        //powinien dostac plansze, zeby moc ocenic?
        int GetScoreForWinner(WinnerEnum winner, bool machineMove, int depth)
        {
            var player = machineMove ? WinnerEnum.Computer : WinnerEnum.Player;

            return winner == player ? ScoreIncrement - depth : -ScoreIncrement + depth;
        }
    }
}