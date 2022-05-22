using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZIGRA.Player
{
    public abstract class PlayerBase : IPlayer
    {
        protected PlayerEnum[,] gameState;
        protected PlayerEnum playerMark;
        public PlayerBase(PlayerEnum[,] gameState, PlayerEnum playerRole)
        {
            this.playerMark = playerRole;
            this.gameState = gameState;
        }
        public abstract bool MakeMove(int x, int y);
    }
}
