using Enums;
using SZIGRA;
using SZIGRA.Algorithm;
using SZIGRA.Enums;
using SZIGRA.Player;
namespace SZIGRABlazor.Pages;

enum GameMode
{
    PlayerVsPlayer = 0,
    PlayerVsComputer = 1,
    ComputerVsComputer = 2
}
partial class Index
{
    int BoardSize = 3;
    int MaxDepth = 8;
    GameMode pickedGameMode = GameMode.PlayerVsComputer;
    string StartButtonText = "START";
    bool isComputerMoving;

    Func<int, int, Task> OnFieldClicked;
    Board.Board? board;
    TicTacToeGameState ticTacToeGameState;
    PlayerBase player1;
    PlayerBase player2;
    string[,] GameStateToDraw;
    GameResultEnum gameResult = GameResultEnum.GameOngoing;
    (int x, int y)[] winningCords;
    bool isPlayer1Move = false;
    bool isComputerTurn = false;

    public Index()
    {
        InitGame();
    }
    void InitGame()
    {
        gameResult = GameResultEnum.GameOngoing;
        ticTacToeGameState = new TicTacToeGameState(BoardSize, BoardSize);
        GameStateToDraw = new string[BoardSize, BoardSize];
        OnFieldClicked = (x, y) => Task.CompletedTask;
    }
    void UpdateGameStateToDraw()
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
    async Task OnStartButtonClicked()
    {
        StartButtonText = "RESTART";

        InitGame();
        ChangeGameMode();
        await board.ResetBoard();
        await InvokeAsync(() => StateHasChanged());
    }

    async Task ComputerMove()
    {
        isComputerMoving = true;
        await Task.Yield();
        var newState = await Task.Run(() => isPlayer1Move ? player1.MakeMove(ticTacToeGameState, 0, 0) : player2.MakeMove(ticTacToeGameState, 0, 0));
        MakeMove(newState);
        await board.UpdateBoardState();
        isComputerMoving = false;
        if (pickedGameMode == GameMode.PlayerVsComputer)
        {
            isComputerTurn = false;
        }
    }
    async Task HumanPlayerMove(int x, int y)
    {
        if ((pickedGameMode == GameMode.PlayerVsComputer && isComputerTurn == false) || pickedGameMode == GameMode.PlayerVsPlayer)
        {
            var newState = isPlayer1Move ? player1.MakeMove(ticTacToeGameState, x, y) : player2.MakeMove(ticTacToeGameState, x, y);
            if (pickedGameMode == GameMode.PlayerVsComputer) isComputerTurn = true;
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
            UpdateGameStateToDraw();

            if (gameResult != GameResultEnum.GameOngoing)
            {
                winningCords = result.sequenceCoords;
            }

        }
    }
    void ChangeGameMode()
    {
        switch (pickedGameMode)
        {
            case GameMode.PlayerVsPlayer://ok
                isPlayer1Move = false;
                isComputerTurn = false;
                player1 = new TicTacToePlayer(PlayerEnum.Player1);
                player2 = new TicTacToePlayer(PlayerEnum.Player2);
                OnFieldClicked = HumanPlayerMove;

                break;
            case GameMode.PlayerVsComputer:
                isPlayer1Move = true;
                isComputerTurn = true;
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
                OnFieldClicked = HumanPlayerMove;
                break;
            case GameMode.ComputerVsComputer:
                isComputerTurn = true;
                OnFieldClicked = (x, y) => Task.CompletedTask;
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
                player2 = new TicTacToeComputerPlayer(
                           new TicTacToeMiniMax(
                               GameResultEnum.Player2Won,
                               GameResultEnum.Player1Won,
                               PlayerEnum.Player2,
                               PlayerEnum.Player1,
                               double.NegativeInfinity,
                               double.PositiveInfinity,
                               MaxDepth
                           ),
                           PlayerEnum.Player2
                       );
                isPlayer1Move = false;
                break;
        }
    }
   
}
