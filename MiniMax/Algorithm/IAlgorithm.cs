using Enums;
using System;

namespace SZIGRA.Algorithm
{
    public interface IAlgorithm<T>
    {
        T FindBestMove(T gameState);
    }
}
