using Enums;
using System.Collections.Generic;

namespace SZIGRA.Abstract
{
    public interface IBoard
    {
        int Length { get; }
        IBoard Copy();
        IEnumerable<int> GetEmptyFields();
        bool IsMoveLegal(int fieldIndex);
        WinnerEnum GetWinner();
        FieldStateEnum this[int key] { get; set; }
    }
}
