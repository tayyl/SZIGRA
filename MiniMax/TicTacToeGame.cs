using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using SZIGRA.Algorithm;
using SZIGRA.Enums;
using SZIGRA.Player;

namespace SZIGRA
{
    public class TicTacToeGame
    {
        public PlayerEnum[,] GameState { get; private set; }
        public PlayerEnum MovingPlayer { get; private set; }
        IPlayer player1;
        IPlayer player2;
        //na pewno stan gry powinien byc tworzony poza gra?
        //teraz musi, bo gracze maja miec dependency do planszy, wiec zeby wszystko trzymalo ta sama referencje
        //zaleznie od preferencji mozna podac dwoch graczy komputerowych lub ludzkich
        public TicTacToeGame(IPlayer player1, IPlayer player2, PlayerEnum[,] gameState)
        {
            this.GameState = gameState;
            this.player1 = player1;
            this.player2 = player2;
            MovingPlayer = PlayerEnum.Player1;
        }

        public bool MakeMove(int x, int y)
        {
            var result = MovingPlayer == PlayerEnum.Player1 ? player1.MakeMove(x, y) : player2.MakeMove(x, y);

            MovingPlayer = MovingPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;

            //ruch nieudany jeżeli koordynaty błędne, lub pole zajęte (a w przypadku komputera, brak możliwości ruchu)
            return result;
        }
        public GameResultEnum GetGameResult()
        {
            //?? - mam w TicTacToeMiniMax obliczanie wyniku, co zrobic zeby go nie kopiować?
            //gra powinna byc w stanie okreslic czy trwa, czy ktos wygral czy jest remis
            return default;
        }
        //zeby moc za pomoca widoku zaznaczyc ??
        public List<int> GetWinningSequenceIndexes()
        {
            var result = new List<int>();



            return result;
        }

    }
}
