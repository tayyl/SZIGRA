using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZIGRA.Algorithm;

namespace SZIGRA.Player
{
    public class TicTacToeComputerPlayer : PlayerBase
    {
        IAlgorithm<TicTacToeGameState> algorithm;

        public TicTacToeComputerPlayer(IAlgorithm<TicTacToeGameState> algorithm, PlayerEnum playerRole) 
            : base(playerRole)
        {
            this.algorithm = algorithm;
        }

        public override TicTacToeGameState MakeMove(TicTacToeGameState gameState, int x, int y)
        {
            return algorithm.FindBestMove(gameState);
        }
    }
}
