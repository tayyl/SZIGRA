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
        public TicTacToePlayer(PlayerEnum[,] gameState, PlayerEnum playerRole) : base(gameState, playerRole)
        {
        }

        public override bool MakeMove(int x, int y)
        {
            try
            {
                if (gameState[x, y] != PlayerEnum.None)
                {
                    return false;
                }
                gameState[x, y] = playerMark; //?? czy gracz powinien zmieniac stan gry?
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
