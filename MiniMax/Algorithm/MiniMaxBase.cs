using System.Collections.Generic;
using SZIGRA.Algorithm;

namespace SZIGRA.Algorithm
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Your game state type, ex. int[,] for two dimensional board</typeparam>
    public abstract class MiniMaxBase<T> : IAlgorithm<T>
    {
        double Alpha { get; set; }
        double Beta { get; set; }
        int MaxDepth { get; set; }
        public MiniMaxBase(double alpha, double beta, int maxDepth)
        {
            Alpha = alpha;
            Beta = beta;
            MaxDepth = maxDepth;
        }
        public T FindBestMove(T gameState)
        {
            return AlphaBetaPruning(gameState, true, MaxDepth, Alpha, Beta).gameState;
        }
        private (T gameState, double score) AlphaBetaPruning(T gameState, bool maximizingPlayer, int depth, double alpha, double beta)
        {
            if (depth == 0 || IsTerminal(gameState))
                return (gameState, Evaluate(gameState,maximizingPlayer));

            if (maximizingPlayer)
            {
                var value = (state: default(T), score: double.NegativeInfinity);

                foreach (var nextState in GetAvailableStates(gameState, maximizingPlayer))
                {
                    var newState = AlphaBetaPruning(nextState, false, depth - 1, alpha, beta);
                    value = newState.score >= value.score ? newState : newState;

                    if (value.score >= beta) break;
                    alpha = alpha >= value.score ? alpha : value.score;
                }

                return value;
            }
            else
            {
                var value = (state: default(T), score: double.PositiveInfinity);

                foreach (var nextState in GetAvailableStates(gameState, maximizingPlayer))
                {
                    var newState = AlphaBetaPruning(nextState, true, depth - 1, alpha, beta);
                    value = newState.score <= value.score ? newState : value;

                    if (value.score <= alpha) break;
                    beta = beta <= value.score ? beta : value.score;
                }
                return value;
            }

        }
        public abstract bool IsTerminal(T gameState);
        public abstract double Evaluate(T gameState, bool maximizingPlayer);
        public abstract IEnumerable<T> GetAvailableStates(T gameState, bool maximizingPlayer);
    }
}