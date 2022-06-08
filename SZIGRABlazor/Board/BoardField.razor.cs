using Microsoft.AspNetCore.Components;

namespace SZIGRABlazor.Board
{
    partial class BoardField
    {
        string Content
        {
            get
            {
                if (X < GameState.GetLength(0) && Y < GameState.GetLength(1))
                {
                    return GameState[X, Y];
                }
                return string.Empty;
            }
        }
        [Parameter]
        public int X { get; set; }
        [Parameter]
        public int Y { get; set; }
        [Parameter]
        public Func<int, int, Task> OnButtonClicked { get; set; }
        [Parameter]
        public string[,] GameState { get; set; }
    }
}
