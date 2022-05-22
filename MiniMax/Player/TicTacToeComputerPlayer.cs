using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZIGRA.Algorithm;

namespace SZIGRA.Player
{
    public class TicTacToeComputerPlayer : TicTacToePlayer
    {
        IAlgorithm<PlayerEnum[,]> algorithm;
        public TicTacToeComputerPlayer(IAlgorithm<PlayerEnum[,]> algorithm, PlayerEnum[,] gameState, PlayerEnum playerRole)
            : base(gameState, playerRole)
        {
            this.algorithm = algorithm;
        }

        public override bool MakeMove(int x, int y)
        {
            try
            {
                var computerMove = CheckChange(algorithm.FindBestMove(gameState));
                if (computerMove.didChange)
                {
                    gameState[computerMove.x, computerMove.y] = playerMark;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        (bool didChange, int x, int y) CheckChange(PlayerEnum[,] newGameState)
        {
            for(var i = 0; i < newGameState.GetLength(0); i++)
            {
                for (var j = 0; j < newGameState.GetLength(1); j++)
                {
                    if(gameState[i, j] != newGameState[i, j])
                    {
                        return (true,i,j);
                    }
                }
            }
            return (false,default,default);
        }
    }
}
