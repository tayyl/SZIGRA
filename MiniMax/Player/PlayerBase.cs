using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZIGRA.Player
{
    public abstract class PlayerBase : IPlayer<TicTacToeGameState>
    {
        protected PlayerEnum playerMark;
        public PlayerBase(PlayerEnum playerRole)
        {
            this.playerMark = playerRole;
        }
        public abstract TicTacToeGameState MakeMove(TicTacToeGameState gameState);
    }
}
