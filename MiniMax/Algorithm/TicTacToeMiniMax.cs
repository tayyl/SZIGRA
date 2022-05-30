
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZIGRA.Enums;

namespace SZIGRA.Algorithm
{
    public class TicTacToeMiniMax : MiniMaxBase<TicTacToeGameState>
    {
        public TicTacToeMiniMax(double alpha, double beta, int maxDepth) : base(alpha, beta, maxDepth)
        {
        }
        protected override double Evaluate(TicTacToeGameState gameState, bool maximizingPlayer)
        {
            var gameResult = gameState.GetGameResult();

            var playerResult = maximizingPlayer ? GameResultEnum.Player1Won : GameResultEnum.Player2Won;
            var opponentResult = maximizingPlayer ? GameResultEnum.Player2Won : GameResultEnum.Player1Won;

            if (gameResult.gameResult == playerResult)
            {
                return 1;
            }

            if (gameResult.gameResult == opponentResult)
            {
                return -1;
            }

            return 0;
        }

        protected override IEnumerable<TicTacToeGameState> GetAvailableStates(TicTacToeGameState gameState, bool maximizingPlayer)
        {
            var playerToMove = maximizingPlayer ? PlayerEnum.Player2 : PlayerEnum.Player1;

            return gameState.GetAvailableStates(playerToMove);
        }
 
        protected override bool IsTerminal(TicTacToeGameState gameState)
        {
            return gameState.GetGameResult().gameResult != GameResultEnum.GameOngoing;
        }
    }
}
