using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TicTacToe
{
    public class GameLogic
    {
        int playerTurn;
        // 1 = server , 2 = client
        public GameLogic()
        {
            playerTurn = 1;
        }
        private void ChangePlayerTurn()
        {
            if (playerTurn == 1) playerTurn = 2;
            else playerTurn = 1;
        }
        public bool CheckIfGameActionLegal(int player,Button button)
        {
            if (button.Content == null)
            {
                if (player == playerTurn)
                {
                    ChangePlayerTurn();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
