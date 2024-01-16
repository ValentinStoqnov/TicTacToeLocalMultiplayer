using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameActionEventArgs : EventArgs
    {
        public GameActions receivedGameAction { get; set; }
        public PlayerMark receivedPlayerMark { get; set; }

        public GameActionEventArgs(GameActions gameActions, PlayerMark playerMark)
        {
            receivedGameAction = gameActions;
            receivedPlayerMark = playerMark;
        }
    }
}
