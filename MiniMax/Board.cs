using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZIGRA.Abstract;

namespace SZIGRA
{
    public class Board : IBoard
    {
        public int Length => board.Length;
        readonly FieldStateEnum[] board;
        public Board(int boardSize)
        {
            board = new FieldStateEnum[boardSize];
        }
        public Board(FieldStateEnum[] board)
        {
            this.board = new FieldStateEnum[board.Length];
            board.CopyTo(board,0);
        }
        public IBoard Copy()
        {
            return new Board(board);
        }
        //plansza powinna byc 2d?
        public FieldStateEnum this[int key] { get => board[key]; set => board[key] = value; }

        public IEnumerable<int> GetEmptyFields()
        {
            for(var i=0; i<board.Length; i++)
            {
                if(board[i] == FieldStateEnum.Empty)
                yield return i;
            }
        }
        //osobny interfejs = uogolnic? 
        public WinnerEnum GetWinner()
        {
            //todo: sprawdzenie sekwencji wygrywajacych?
            throw new NotImplementedException();
        }

        public bool IsMoveLegal(int fieldIndex)
        {
            return board[fieldIndex] == FieldStateEnum.Empty;
        }
    }
}
