using Enums;
using System;
using SZIGRA;
using SZIGRA.Abstract;

namespace TicTacToe
{
    internal class Program
    {

        static void ShowBoard(IBoard board)
        {
            string EnumToString(FieldStateEnum fieldStateEnum) => fieldStateEnum switch
            {
                FieldStateEnum.OccupiedByComputer => "O",
                FieldStateEnum.OccupiedByPlayer => "X",
                _ => " ",
            };
            for (var i = 0; i < board.Length; i++)
            {
                if (i > 0 && i % 3 == 0) Console.WriteLine("\n-----------");
                Console.Write(EnumToString(board[i]) + " |");
            }
        }
        static void Main(string[] args)
        {
            //var minimax = new MiniMax(1000, 1000, 10,9);
            //var board = new Board(9);
            //var tictactoeGame = new Game(minimax, board);

            //ShowBoard(board);
        }
    }
}
