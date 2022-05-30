using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;

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
    private Canvas2DContext _context;

    protected BECanvasComponent _canvasReference;
    int zindex = -1;
    int t = 1;
    async Task Finish()
    {
        var points = GetWinnerCoords();
        foreach (var point in points)
        {
            await JsRuntime.InvokeAsync<BoundingClientRect>(
                "addElementStyle", $"{point.x}-{point.y}",
                "text-shadow: 0px 0px 13px rgba(92, 253, 81, 1); background-color: rgba(144, 185, 142, 0.29); color: darkgreen; transition: all 0.5s ease; "
                );
        }
        //zndex = 1;
        //StateHasChanged();
        //await _context.SetLineCapAsync(LineCap.Square);
        //await _context.SetLineWidthAsync(50);
        //await _context.SetStrokeStyleAsync("blue");
        //await JsRuntime.InvokeAsync<object>("initDrawing", DotNetObjectReference.Create(this));
    }
    //async Task<List<(double X, double Y)>> CreatePoints((int X, int Y)[] points)
    //{
    //    var fieldsCoords = new List<BoundingClientRect>();
    //    foreach(var point in points)
    //    {
    //        fieldsCoords.Add(await JsRuntime.InvokeAsync<BoundingClientRect>(
    //            "addElementStyle",$"{point.X}-{point.Y}", 
    //            "text-shadow: 0px 0px 13px rgba(92, 253, 81, 1); background-color: rgba(144, 185, 142, 0.29); color: darkgreen"
    //            )
    //        );
    //    }

    //    var waypoints = new List<(double X, double Y)>();
    //    //for (var i = 1; i < fieldsCoords.Count(); i++)
    //    //{
    //    //    var pt0 = fieldsCoords[i - 1];
    //    //    var pt1 = fieldsCoords[i];
    //    //    var dx = pt1.X - pt0.X;
    //    //    var dy = pt1.Y - pt0.Y;
    //    //    for (var j = 0; j < 100; j++)
    //    //    {
    //    //        var x = pt0.X + dx * j / 100;
    //    //        var y = pt0.Y + dy * j / 100;
    //    //        waypoints.Add(new(x, y));
    //    //    }

    //    //}
    //    return (waypoints);
    //}
    //[JSInvokable]
    //public async ValueTask DrawWinnerLine()
    //{
    //    if (t >= points.Count)
    //    {
    //        await JsRuntime.InvokeAsync<object>("finishDrawing");
    //    };
    //    await _context.BeginPathAsync();
    //    await _context.MoveToAsync(points[t - 1].X, points[t - 1].Y);
    //    await _context.LineToAsync(points[t].X, points[t].Y);
    //    await _context.StrokeAsync();
    //    t++;
    //}
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
     //   _context = await _canvasReference.CreateCanvas2DAsync();
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
        return $"{(100 / Height)}%";
    }
    string RowWidth()
    {
        return $"{(100 / Width)}%";
    }
    string CalculateFontSize()
    {
        //var fontSize = 4/(double)Width*Height;
        var fontSize = 10;
        return $"{fontSize}vh";
    }
    async Task FieldClicked(int x, int y)
    {
        if (IsFinalMove())
        {
            if (IsDraw())
            {
                //pokazanie remisu
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

public class BoundingClientRect
{
    public double X { get; set; }
    public double Y { get; set; }
}