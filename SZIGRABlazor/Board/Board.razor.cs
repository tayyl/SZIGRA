using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace SZIGRABlazor.Board
{
    partial class Board
    {
        [Parameter]
        public int Width { get; set; }
        [Parameter]
        public int Height { get; set; }
        [Parameter]
        public Func<int, int, Task> OnFieldClicked { get; set; }
        [Parameter]
        public Func<bool> IsFinalMove { get; set; }
        [Parameter]
        public Func<bool> IsDraw { get; set; }
        [Parameter]
        public Func<(int x, int y)[]> GetWinnerCoords { get; set; }
        [Parameter]
        public string[,] GameState { get; set; }
        string Winner { get; set; }
        string BlurStyles { get; set; }
        string ShowResultClass { get; set; }
        string GetBorderClass(int i, int j)
        {
            var borderClassName = "tictactoe-border-";
            var toReturn = "";
            if (i != Height - 1) toReturn += $" {borderClassName}bottom";
            if (j != 0) toReturn += $" {borderClassName}left";

            return toReturn;
        }
        string CalculateRowHeight()
        {
            return $"{(100 / (double)Height).ToString(CultureInfo.InvariantCulture)}%";
        }
        string CalculateRowWidth()
        {
            return $"{(100 / Width)}%";
        }
        public async Task ResetBoard()
        {
            Winner = "";
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    await JsRuntime.InvokeAsync<object>(
                        "removeElementStyle", $"{i}-{j}",
                        "background-color"
                        );
                    await JsRuntime.InvokeAsync<object>(
                        "removeElementStyle", $"{i}-{j}",
                        "transition"
                        );

                }
            }
            ShowResultClass = "";
            BlurStyles = "";

        }
        async Task FieldClicked(int x, int y)
        {
            await OnFieldClicked(x, y);
            await UpdateBoardState();
        }
        public async Task UpdateBoardState()
        {
            await InvokeAsync(() => StateHasChanged());
            if (IsFinalMove())
            {
                if (IsDraw())
                {
                    await SelectDraw();
                }
                else
                {
                    await SelectWinnerPositions();
                }

            }
        }
        async Task SelectWinnerPositions()
        {
            var points = GetWinnerCoords();
            foreach (var point in points)
            {
                await JsRuntime.InvokeAsync<object>(
                    "addElementStyle", $"{point.x}-{point.y}",
                    "background-color: #6CD94E; transition: all 0.5s ease; "
                    );
            }
            var (x, y) = GetWinnerCoords()[0];

            Winner = GameState[x, y] + " wins!";

            await ShowResult();
        }
        async Task ShowResult()
        {
            await Task.Delay(2000);
            BlurStyles = "filter: blur(8px); transition: all 2s ease;";
            ShowResultClass = "visible";
            await InvokeAsync(() => StateHasChanged());
        }
        async Task SelectDraw()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    await JsRuntime.InvokeAsync<object>(
                        "addElementStyle", $"{i}-{j}",
                        "background-color: #5C6870; transition: all 0.5s ease; "
                        );

                }
            }
            Winner = "Draw!";
            await ShowResult();
        }
    }
}
