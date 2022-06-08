using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZIGRA.Enums;

namespace SZIGRA
{
    public class TicTacToeGameState
    {
        PlayerEnum[,] gameState;
 
        public TicTacToeGameState(int width, int height)
        {
            gameState = new PlayerEnum[width, height];
        }
        TicTacToeGameState(PlayerEnum[,] gameState)
        {
            this.gameState = gameState;
        }
        public TicTacToeGameState Clone()
        {
            var newGameState = (PlayerEnum[,])gameState.Clone();

            return new TicTacToeGameState(newGameState);
        }
        public IEnumerable<TicTacToeGameState> GetAvailableStates(PlayerEnum player)
        {

            for (var i = 0; i < gameState.GetLength(0); i++)
            {
                for (var j = 0; j < gameState.GetLength(1); j++)
                {

                    if (gameState[i, j] == PlayerEnum.None)
                    {
                        var gameStateCopy = (PlayerEnum[,])gameState.Clone();
                        gameStateCopy[i, j] = player;
                        yield return new TicTacToeGameState(gameStateCopy);
                    }
                }
            }
        }
        public (GameResultEnum gameResult, (int x, int y)[] sequenceCoords) GetGameResult()
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
                    return (GameResultEnum.Player1Won, player1.h == gameStateRows ? GetWinningSequence(x => (i, x)) : GetWinningSequence(x => (x, i)));
                }
                if (player2.h == gameStateRows || player2.v == gameStateColumns)
                {
                    return (GameResultEnum.Player2Won, player2.h == gameStateRows ? GetWinningSequence(x => (i, x)) : GetWinningSequence(x => (x, i)));
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
                switch (gameState[gameStateRows - i - 1, i])
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
                return (GameResultEnum.Player1Won, player1.diag == gameStateRows ? GetWinningSequence(x => (x, x)) : GetWinningSequence(x => (gameStateRows - x - 1, x)));
            }
            if (player2.diag == gameStateRows || player2.antidiag == gameStateRows)
            {
                return (GameResultEnum.Player2Won, player2.diag == gameStateRows ? GetWinningSequence(x => (x, x)) : GetWinningSequence(x => (gameStateRows - x - 1, x)));
            }

            return (anyFieldIsEmpty ? GameResultEnum.GameOngoing : GameResultEnum.Draw, default);
        }
        (int x, int y)[] GetWinningSequence(Func<int, (int x, int y)> func) => Enumerable.Range(0, gameState.GetLength(0)).Select(func).ToArray();
        public override bool Equals(object obj)
        {
            var newGamestate = (TicTacToeGameState)obj;
            for (var i = 0; i < gameState.GetLength(0); i++)
            {
                for (var j = 0; j < gameState.GetLength(1); j++)
                {
                    if (gameState[i, j] != newGamestate[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public PlayerEnum this[int x, int y]
        {
            get => gameState[x, y];
            set => gameState[x, y] = value;
        }
        
    }
}
