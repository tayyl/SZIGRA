using Enums;
using System;
using System.Linq;
using SZIGRA.Abstract;

namespace SZIGRA
{
    public class Game : IGame
    {
		public WinnerEnum Winner { get; private set; }
		IBoard Board { get; set; }
		IAlgorithm Algorithm { get; set; }

		public Game(IAlgorithm algorithm, IBoard board)
		{
			Algorithm = algorithm;
			Board = board;
		}
		public (bool moveIsLegal, bool gameEnded) MakePlayerMove(int fieldIdx)
		{
			if (!Board.IsMoveLegal(fieldIdx)) return (false,false);

			return PerformMove(fieldIdx, FieldStateEnum.OccupiedByPlayer);
		}
		public (bool moveIsLegal, bool gameEnded) MakeComputerMove()
		{
			var bestMove = Algorithm.FindBestMove(Board);

			if (bestMove > -1)
				return PerformMove(bestMove, FieldStateEnum.OccupiedByComputer);
			else
				return (false, true);
		}
		(bool moveIsLegal, bool gameEnded) PerformMove(int fieldIdx, FieldStateEnum fieldState)
		{
			Board[fieldIdx] = fieldState;
			Winner = Board.GetWinner();
			return (true, IsFinal());
		}
		bool IsFinal()
		{
			if (Winner == WinnerEnum.None)
			{
				if (Board.GetEmptyFields().Any())
					return false;
				Winner = WinnerEnum.Draw;
			}

			return true;
		}
	}
}
