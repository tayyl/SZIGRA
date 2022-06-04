using System.Collections.Generic;
using System.Linq;
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
                return (default(T), Evaluate(gameState));

            if (maximizingPlayer)
            {
                var stateToReturn = default(T);

                foreach (var nextState in GetAvailableStates(gameState, maximizingPlayer))
                {
                    var newScore = AlphaBetaPruning(nextState, false, depth - 1, alpha, beta).score;
                    stateToReturn = alpha < newScore ? nextState : stateToReturn;
                    alpha = alpha < newScore ? newScore : alpha;
                    if (alpha >= beta) break;
                }

                return (stateToReturn, alpha);
            }
            else
            {
                var stateToReturn = default(T);

                foreach (var nextState in GetAvailableStates(gameState, maximizingPlayer))
                {
                    var newScore = AlphaBetaPruning(nextState, true, depth - 1, alpha, beta).score;
                    stateToReturn = beta > newScore ? nextState : stateToReturn;
                    beta = beta > newScore ? newScore : beta;
                    if (alpha >= beta) break;
                }
                return (stateToReturn, beta);
            }
        }
        protected abstract bool IsTerminal(T gameState);
        protected abstract double Evaluate(T gameState);
        protected abstract IEnumerable<T> GetAvailableStates(T gameState, bool maximizingPlayer);
    }
}