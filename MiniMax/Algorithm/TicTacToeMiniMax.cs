
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
        GameResultEnum winningResult;
        GameResultEnum losingResult;
        PlayerEnum playerToWin;
        PlayerEnum playerToLose;
        public TicTacToeMiniMax(
            GameResultEnum winningResult, 
            GameResultEnum losingResult, 
            PlayerEnum maximizingPlayer, 
            PlayerEnum minimizingPlayer, 
            double alpha, 
            double beta, 
            int maxDepth
        ) : base(alpha, beta, maxDepth)
        {
            this.winningResult = winningResult;
            this.losingResult = losingResult;
            this.playerToLose = minimizingPlayer;
            this.playerToWin = maximizingPlayer;
        }
        protected override double Evaluate(TicTacToeGameState gameState)
        {
            var gameResult = gameState.GetGameResult();

         
            if (gameResult.gameResult == winningResult)
            {
                return 1;
            }

            if (gameResult.gameResult == losingResult)
            {
                return -1;
            }

            return 0;
        }

        protected override IEnumerable<TicTacToeGameState> GetAvailableStates(TicTacToeGameState gameState, bool maximizingPlayer)
        {
            var playerToMove = maximizingPlayer ? playerToWin : playerToLose;

            return gameState.GetAvailableStates(playerToMove);
        }

        protected override bool IsTerminal(TicTacToeGameState gameState)
        {
            return gameState.GetGameResult().gameResult != GameResultEnum.GameOngoing;
        }
    }
}
