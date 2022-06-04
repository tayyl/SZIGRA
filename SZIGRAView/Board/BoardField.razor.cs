using Microsoft.AspNetCore.Components;

namespace SZIGRAView.Board;
public partial class BoardField
{
    string Content => GameState[X,Y];
    [Parameter]
    public int X { get; set; }
    [Parameter]
    public int Y { get; set; }
    [Parameter]
    public Func<int,int,Task> OnButtonClicked { get; set; }
    [Parameter]
    public string[,] GameState { get; set; }
}
