using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;
using System.Globalization;

namespace SZIGRAView.Board;
public partial class Board
{
    [Parameter]
    public int Width { get; set; }
    [Parameter]
    public int Height { get; set; }
    [Parameter]
    public OnFieldClicked OnFieldClicked { get; set; }
    [Parameter]
    public Func<bool> IsFinalMove { get; set; }
    [Parameter]
    public Func<bool> IsDraw { get; set; }
    [Parameter]
    public Func<(int x, int y)[]> GetWinnerCoords { get; set; }
    [Parameter]
    public string[,] GameState { get; set; }

    async Task Finish()
    {
        var points = GetWinnerCoords();
        foreach (var point in points)
        {
            await JsRuntime.InvokeAsync<object>(
                "addElementStyle", $"{point.x}-{point.y}",
                "background-color: #6CD94E; transition: all 0.5s ease; "
                );
        }
     
    }

    string BorderClass(int i, int j)
    {
        var borderClassName = "tictactoe-border-";
        var toReturn = "";
        if (i != Height - 1) toReturn += $" {borderClassName}bottom";
        if (j != 0) toReturn += $" {borderClassName}left";

        return toReturn;
    }
    string RowHeight()
    {
        return $"{(100 / (double)Height).ToString(CultureInfo.InvariantCulture)}%";
    }
    string RowWidth()
    {
        return $"{(100 / Width)}%";
    }
    string CalculateFontSize()
    {
        var fontSize = 12;
        return $"{fontSize}vh";
    }
    async Task Draw()
    {
        for(var i = 0; i < Height; i++)
        {
            for(var j = 0; j < Width; j++)
            {
                await JsRuntime.InvokeAsync<object>(
                    "addElementStyle", $"{i}-{j}",
                    "background-color: #5C6870; transition: all 0.5s ease; "
                    );

            }
        }

    }
    async Task FieldClicked(int x, int y)
    {
        if (IsFinalMove())
        {
            if (IsDraw())
            {
                await Draw();
            }
            else
            {
                await Finish();
            }

        }
        else
        {
            await OnFieldClicked(x, y);
            StateHasChanged();
        }
    }
}
