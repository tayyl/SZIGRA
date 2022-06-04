using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZIGRA.Player
{
    public class TicTacToePlayer : PlayerBase
    {
        public TicTacToePlayer(PlayerEnum playerRole) : base(playerRole)
        {
        }

        public override TicTacToeGameState MakeMove(TicTacToeGameState gameState, int x, int y)
        {
            if (gameState[x,y] == PlayerEnum.None)
            {
                var newState = gameState.Clone();
                newState[x, y] = playerMark;
                return newState;
            }
            return gameState;
        }
    }
}
