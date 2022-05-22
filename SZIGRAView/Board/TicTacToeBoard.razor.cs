using Microsoft.AspNetCore.Components;

namespace SZIGRAView.Board;
public partial class TicTacToeBoard
{
    [Parameter]
    public int Width { get; set; }
    [Parameter]
    public int Height { get; set; }
    /// <summary>
    /// Is called after every click on board field
    /// </summary>
    [Parameter]
    public OnFieldClicked OnFieldClicked { get; set; }
    /// <summary>
    /// Is called after every click on board field, if true calls 'IsDraw' & 'OnWinnerMark'
    /// </summary>
    [Parameter]
    public Func<bool> IsFinalMove { get; set; }
    /// <summary>
    /// Indicates if there is no winner
    /// </summary>
    [Parameter]
    public Func<bool> IsDraw { get; set; }
    /// <summary>
    /// Marks winning sequence, from field (x1,y1) to (x2,y2)
    /// </summary>
    [Parameter]
    public Func<(int x1, int y1, int x2, int y2)> GetWinnerCoords{ get; set; }

    string borderClass(int i, int j)
    {
        var borderClassName = "tictactoe-border-";
        var toReturn = "";
        if (i != Height - 1) toReturn += $" {borderClassName}bottom";
        if (j != 0) toReturn += $" {borderClassName}left";


        return toReturn;
    }
    string rowHeight()
    {
        return $"{(100 / Height)}%";
    }
    void FieldClicked(int x, int y, ref string fieldContent)
    {
        OnFieldClicked(x, y, ref fieldContent);
        if (IsFinalMove())
        {
            if (IsDraw())
            {
                //pokazanie remisu
            }
            else
            {
                //todo: zaznaczenie wygrywajacej sekwencji 
                var markCoordinates = GetWinnerCoords();
            }
            
        }
    }
}