using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZIGRA.Abstract
{
    public interface IGame
    {
        WinnerEnum Winner { get; }
        (bool moveIsLegal, bool gameEnded) MakeComputerMove();
        (bool moveIsLegal, bool gameEnded) MakePlayerMove(int fieldIdx);
    }
}
