using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZIGRA.Player
{
    public interface IPlayer<T>
    {
        public T MakeMove(T gameState, int x, int y);
    }
}
