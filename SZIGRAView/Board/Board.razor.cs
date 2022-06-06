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
    public Func<int,int,Task> OnFieldClicked { get; set; }
    [Parameter]
    public Func<bool> IsFinalMove { get; set; }
    [Parameter]
    public Func<bool> IsDraw { get; set; }
    [Parameter]
    public Func<(int x, int y)[]> GetWinnerCoords { get; set; }
    [Parameter]
    public string[,] GameState { get; set; }
    string Winner { get; set; }
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
        var anyWinPos = GetWinnerCoords()[0];

        Winner = GameState[anyWinPos.x,anyWinPos.y]+ " wins!";
        StateHasChanged();
        await ShowResult();
    }
    async Task ShowResult()
    {
        await Task.Delay(2000);
        await JsRuntime.InvokeAsync<object>(
                "addElementStyle", $"tictactoe-blur",
                "filter: blur(8px); transition: all 2s ease; "
                );

        await JsRuntime.InvokeAsync<object>(
                "addElementStyle", $"tictactoe-result",
                "z-index: 5"
                );
        await JsRuntime.InvokeAsync<object>(
                "addElementClass", $"tictactoe-result",
                "visible"
                );

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
        Winner = "Draw!";
        StateHasChanged();
        await ShowResult();

    }
    async Task FieldClicked(int x, int y)
    {
        await OnFieldClicked(x, y);
        await GameStateChanged();
    }
    public async Task GameStateChanged()
    {
        StateHasChanged();
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
    }
}
