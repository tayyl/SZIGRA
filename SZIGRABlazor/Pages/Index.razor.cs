using Enums;
using SZIGRA;
using SZIGRA.Algorithm;
using SZIGRA.Enums;
using SZIGRA.Player;
namespace SZIGRABlazor.Pages;

partial class Index
{
    //dodać obliczanie głębokości
    #region View
    int BoardSize = 3;
    int MaxDepth = 8;
    GameMode pickedGameMode = GameMode.PlayerVsComputer;
    string StartButtonText = "START";
    bool isComputerMoving;
    async Task ComputerMove()
    {
        isComputerMoving = true;
        await Task.Yield();
        var newState = await Task.Run(
            async () =>
            {

                await InvokeAsync(() => StateHasChanged());
                return player1.MakeMove(ticTacToeGameState, 0, 0);
            }
        );
        MakeMove(newState);
        await InvokeAsync(() => StateHasChanged());
        await board.UpdateBoardState();
        isComputerMoving = false;
    }
    async Task OnStartButtonClicked()
    {
        StartButtonText = "RESTART";

        InitGame();

        await board.ResetBoard();
        await InvokeAsync(() => StateHasChanged());
    }
    void InitGame()
    {
        OnFieldClicked = PVC;
        ticTacToeGameState = new TicTacToeGameState(BoardSize, BoardSize);
        GameStateToDraw = new string[BoardSize, BoardSize];
        player1 = new TicTacToeComputerPlayer(
            new TicTacToeMiniMax(
                GameResultEnum.Player1Won,
                GameResultEnum.Player2Won,
                PlayerEnum.Player1,
                PlayerEnum.Player2,
                double.NegativeInfinity,
                double.PositiveInfinity,
                MaxDepth
            ),
            PlayerEnum.Player1
        );
        player2 = new TicTacToePlayer(PlayerEnum.Player2);
    }
    #endregion
    #region Game
    enum GameMode
    {
        PlayerVsPlayer = 0,
        PlayerVsComputer = 1,
        ComputerVsComputer = 2
    }

    Func<int, int, Task> OnFieldClicked;
    Board.Board? board;
    bool isPlayer1Move = false;
    TicTacToeGameState ticTacToeGameState;
    PlayerBase player1;
    PlayerBase player2;
    string[,] GameStateToDraw;
    GameResultEnum gameResult = GameResultEnum.GameOngoing;
    (int x, int y)[] winningCords;

    public Index()
    {
        ticTacToeGameState = new TicTacToeGameState(BoardSize, BoardSize);
        GameStateToDraw = new string[BoardSize, BoardSize];
        player1 = new TicTacToeComputerPlayer(
            new TicTacToeMiniMax(
                GameResultEnum.Player1Won,
                GameResultEnum.Player2Won,
                PlayerEnum.Player1,
                PlayerEnum.Player2,
                double.NegativeInfinity,
                double.PositiveInfinity,
                MaxDepth
            ),
            PlayerEnum.Player1
        );
        player2 = new TicTacToePlayer(PlayerEnum.Player2);
        OnFieldClicked = (x, y) => { return Task.CompletedTask; };


    }
    async Task PVC(int x, int y)
    {
        if (!isPlayer1Move)
        {
            var newState = player2.MakeMove(ticTacToeGameState, x, y);
            MakeMove(newState);
            await InvokeAsync(() => StateHasChanged());
        }

    }
    void MakeMove(TicTacToeGameState newState)
    {
        if (newState?.Equals(ticTacToeGameState) == false)
        {
            isPlayer1Move = !isPlayer1Move;
            ticTacToeGameState = newState;

            var result = ticTacToeGameState.GetGameResult();

            gameResult = result.gameResult;
            ChangeGameStateToDraw();

            if (gameResult != GameResultEnum.GameOngoing)
            {
                winningCords = result.sequenceCoords;
            }

        }
    }

    //Task PVP()
    //{

    //}
    //Task CVC()
    //{

    //}
    void ChangeGameStateToDraw()
    {
        var fontSize = (12 * 10) / (BoardSize * BoardSize);
        string player1Style = $"<span style=\"color: rgb(242,130,22); font-size: {fontSize}vh;\">O</span>";
        string player2Style = $"<span style=\"color: rgb(0,134,229);font-size: {fontSize}vh;\">X</span>";
        for (var i = 0; i < GameStateToDraw.GetLength(0); i++)
        {
            for (var j = 0; j < GameStateToDraw.GetLength(1); j++)
            {
                if (ticTacToeGameState[i, j] == PlayerEnum.None) continue;
                GameStateToDraw[i, j] = ticTacToeGameState[i, j] == PlayerEnum.Player1 ? player1Style : player2Style;
            }
        }
    }
    #endregion
    //todo: ustawienia - pvp, pvc, ?cvc?, restart, move computer
}
