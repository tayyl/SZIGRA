
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZIGRA.Enums;

namespace SZIGRA.Algorithm
{
    public class TicTacToeMiniMax : MiniMaxBase<PlayerEnum[,]>
    {
        public TicTacToeMiniMax(double alpha, double beta, int maxDepth) : base(alpha, beta, maxDepth)
        {
        }
        protected override double Evaluate(PlayerEnum[,] gameState, bool maximizingPlayer)
        {
            var gameResult = GetGameResult(gameState);

            var playerResult = maximizingPlayer ? GameResultEnum.Player1Won : GameResultEnum.Player2Won;
            var opponentResult = maximizingPlayer ? GameResultEnum.Player2Won : GameResultEnum.Player1Won;

            if (gameResult == playerResult)
            {
                return 1;
            }

            if (gameResult == opponentResult)
            {
                return -1;
            }

            return 0;
        }

        protected override IEnumerable<PlayerEnum[,]> GetAvailableStates(PlayerEnum[,] gameState, bool maximizingPlayer)
        {
            var playerToMove = maximizingPlayer ? PlayerEnum.Player2 : PlayerEnum.Player1;

            for (var i = 0; i < gameState.GetLength(0); i++)
            {
                for (var j = 0; j < gameState.GetLength(1); j++)
                {

                    if (gameState[i, j] == PlayerEnum.None)
                    {
                        var gameStateCopy = (PlayerEnum[,])gameState.Clone();
                        gameStateCopy[i, j] = playerToMove;
                        yield return gameStateCopy;
                    }
                }
            }
        }
        //?? wyjąć to gdzieś wyżej?
        public GameResultEnum GetGameResult(PlayerEnum[,] gameState)
        {
            var anyFieldIsEmpty = false;
            (int h, int v, int diag, int antidiag) player1 = (0, 0, 0, 0);
            (int h, int v, int diag, int antidiag) player2 = (0, 0, 0, 0);

            var gameStateRows = gameState.GetLength(0);
            var gameStateColumns = gameState.GetLength(1);
            for (var i = 0; i < gameStateRows; i++)
            {
                player1.h = player1.v = player2.h = player2.v = 0;
                for (var j = 0; j < gameStateColumns; j++)
                {
                    switch (gameState[i, j])
                    {
                        case PlayerEnum.None:
                            anyFieldIsEmpty = true;
                            break;
                        case PlayerEnum.Player1:
                            player1.h++;
                            break;
                        case PlayerEnum.Player2:
                            player2.h++;
                            break;
                    }
                    switch (gameState[j, i])
                    {
                        case PlayerEnum.None:
                            anyFieldIsEmpty = true;
                            break;
                        case PlayerEnum.Player1:
                            player1.v++;
                            break;
                        case PlayerEnum.Player2:
                            player2.v++;
                            break;
                    }
                }

                if (player1.h == gameStateRows || player1.v == gameStateColumns)
                {
                    return GameResultEnum.Player1Won;
                }
                if (player2.h == gameStateRows || player2.v == gameStateColumns)
                {
                    return GameResultEnum.Player2Won;
                }

                switch (gameState[i, i])
                {
                    case PlayerEnum.Player1:
                        player1.diag++;
                        break;
                    case PlayerEnum.Player2:
                        player2.diag++;
                        break;
                }
                switch (gameState[gameStateRows - i, i])
                {
                    case PlayerEnum.Player1:
                        player1.antidiag++;
                        break;
                    case PlayerEnum.Player2:
                        player2.antidiag++;
                        break;
                }
            }

            if (player1.diag == gameStateRows || player1.antidiag == gameStateRows)
            {
                return GameResultEnum.Player1Won;
            }
            if (player2.diag == gameStateRows || player2.antidiag == gameStateRows)
            {
                return GameResultEnum.Player2Won;
            }

            return anyFieldIsEmpty ? GameResultEnum.GameOngoing : GameResultEnum.Draw;
        }

        protected override bool IsTerminal(PlayerEnum[,] gameState)
        {
            return GetGameResult(gameState) != GameResultEnum.GameOngoing;
        }
    }
}
