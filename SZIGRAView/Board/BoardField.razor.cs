using Microsoft.AspNetCore.Components;

namespace SZIGRAView.Board;
public delegate void OnFieldClicked(int x, int y, ref string content);
public partial class BoardField
{
    string Content = "";
    [Parameter]
    public int X { get; set; }
    [Parameter]
    public int Y { get; set; }
    [Parameter]
    public OnFieldClicked OnButtonClicked { get; set; }
}
