using Microsoft.AspNetCore.Components;

namespace SZIGRAView.Board;
public delegate Task OnFieldClicked(int x, int y);
public partial class BoardField
{
    string Content => GameState[X,Y];
    [Parameter]
    public int X { get; set; }
    [Parameter]
    public int Y { get; set; }
    [Parameter]
    public OnFieldClicked OnButtonClicked { get; set; }
    [Parameter]
    public string FontSize { get; set; }
    [Parameter]
    public string[,] GameState { get; set; }
}
