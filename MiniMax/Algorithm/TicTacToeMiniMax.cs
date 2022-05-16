
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZIGRA.Algorithm
{
    public class TicTacToeMiniMax : MiniMaxBase<FieldStateEnum[,]>
    {
        public TicTacToeMiniMax(double alpha, double beta, int maxDepth) : base(alpha, beta, maxDepth)
        {
        }

        public override double Evaluate(FieldStateEnum[,] gameState, bool maximizingPlayer)
        {
            var playerToEvaluate = maximizingPlayer ? FieldStateEnum.OccupiedByPlayer2 : FieldStateEnum.OccupiedByPlayer1;
            var opponent = maximizingPlayer ? FieldStateEnum.OccupiedByPlayer1 : FieldStateEnum.OccupiedByPlayer2;

            if (HasWon(opponent, gameState))
            {
                return double.NegativeInfinity;
            }

            if (HasWon(playerToEvaluate, gameState))
            {
                return double.PositiveInfinity;
            }

            if (HasEmptyFields(gameState))
            {
                return 0;
            }

            //jak oceniac?
        }
        //czy na pewno w tej klasie?

        double EvaluateWindow(FieldStateEnum[] windowGameState, FieldStateEnum player)
        {
            //jaka heurystyka??
            var emptyFields = 0;
            var playerFields = 0;
            var opponentFields = 0;
            for (var i = 0; i < windowGameState.Length; i++)
            {
                switch (windowGameState[i])
                {
                    case FieldStateEnum.OccupiedByPlayer1: playerFields++; break;
                    case FieldStateEnum.OccupiedByPlayer2: opponentFields++; break;
                    case FieldStateEnum.Empty: emptyFields++; break;
                };
            }

            //winning sequence
            if(playerFields == windowGameState.Length)
            {
                return 100;
            }

            //almost winning
            if(playerFields == windowGameState.Length - 1 && emptyFields == 1)
            {
                return 5;
            }

            //close to losing
            if(emptyFields == 1 && opponentFields == windowGameState.Length - 1)
            {
                return -4;
            }
            
            //normal move
            return 2;
        }
        public override IEnumerable<FieldStateEnum[,]> GetAvailableStates(FieldStateEnum[,] gameState, bool maximizingPlayer)
        {
            var playerToMove = maximizingPlayer ? FieldStateEnum.OccupiedByPlayer2 : FieldStateEnum.OccupiedByPlayer1;

            for (var i = 0; i < gameState.GetLength(0); i++)
            {
                for (var j = 0; j < gameState.GetLength(1); j++)
                {
                    if (gameState[i, j] == FieldStateEnum.Empty)
                    {
                        var boardCopy = (FieldStateEnum[,])gameState.Clone();
                        boardCopy[i, j] = playerToMove;

                        yield return boardCopy;
                    }
                }
            }
        }
        //czy metoda, w której są już użyte zasady gry powinna na pewno być w minimax?
        //czy to powinna być odpowiedzialność innej klasy? (np. game)
        bool HasWon(FieldStateEnum player, FieldStateEnum[,] gameState)
        {
            /*
             Winning combinations:
             
                XXX --- ---
                --- XXX ---
                --- --- XXX
            
                X-- -X- --X
                X-- -X- --X
                X-- -X- --X

                X-- --X 
                -X- -X- 
                --X X-- 

             */
            var rightSlope = 0;
            var leftSlope = 0;
            //horizontal && vertical 
            for (var i = 0; i < gameState.GetLength(0); i++)
            {
                var horizontal = 0;
                var vertical = 0;

                for (var j = 0; j < gameState.GetLength(1); j++)
                {
                    if (gameState[i, j] == player)
                    {
                        horizontal++;
                    }
                    if (gameState[j, i] == player)
                    {
                        vertical++;
                    }
                }

                if (horizontal == gameState.GetLength(0) || vertical == gameState.GetLength(0))
                {
                    return true;
                }

                if (gameState[i, i] == player)
                {
                    rightSlope++;
                }
                if (gameState[gameState.GetLength(0) - i, i] == player)
                {
                    leftSlope++;
                }
            }
            if (rightSlope == gameState.GetLength(0) || leftSlope == gameState.GetLength(0))
            {
                return true;
            }
            return false;
        }
        bool HasEmptyFields(FieldStateEnum[,] gameState)
        {
            for (var i = 0; i < gameState.GetLength(0); i++)
            {
                for (var j = 0; j < gameState.GetLength(1); j++)
                {
                    if (gameState[i, j] == FieldStateEnum.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public override bool IsTerminal(FieldStateEnum[,] gameState)
        {
            //any of players has won
            if (HasWon(FieldStateEnum.OccupiedByPlayer1, gameState) || HasWon(FieldStateEnum.OccupiedByPlayer2, gameState))
                return true;

            //no move available
            return HasEmptyFields(gameState) == false;
        }
    }
}
