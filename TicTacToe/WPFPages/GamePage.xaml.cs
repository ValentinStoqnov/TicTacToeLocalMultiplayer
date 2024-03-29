﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe.WPFPages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        ClientType clientType;
        ClientLogic clientLogic;
        ServerLogic serverLogic;
        GameLogic gameLogic;    

        private GamePage(ClientType clientType)
        {
            InitializeComponent();
            this.clientType = clientType;  
        }
        public GamePage(ClientType clientType, ClientLogic client) : this(clientType)
        {
            clientLogic = client;
            clientLogic.ReceivedGameActionEvent += ClientLogic_ReceivedGameActionEvent;
        }

        public GamePage(ClientType clientType, ServerLogic server) : this(clientType)
        {
            serverLogic = server;
            gameLogic = new GameLogic();
            serverLogic.ReceivedGameActionEvent += ServerLogic_ReceivedGameActionEvent;
        }

        private void ServerLogic_ReceivedGameActionEvent(object? sender, GameActionEventArgs e)
        {
            foreach (Button b in ButtonsWrapPanel.Children)
            {
                if (Int32.Parse(b.Tag.ToString()) == (int)e.receivedGameAction)
                {
                    if (gameLogic.CheckIfGameActionLegal(2,b))
                    {
                        b.Content = "o";
                        serverLogic.SendBackAcceptedGameAction(e);
                    }    
                }
            }
        }
        private void ClientLogic_ReceivedGameActionEvent(object? sender, GameActionEventArgs e)
        {
            foreach (Button b in ButtonsWrapPanel.Children)
            {
                if (Int32.Parse(b.Tag.ToString()) == (int)e.receivedGameAction)
                {
                    switch (e.receivedPlayerMark)
                    {
                        case PlayerMark.X:
                            b.Content ="x";
                            break;
                        case PlayerMark.O:
                            b.Content = "o";
                            break;
                    }
                }
            }
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            int buttonTag = Int32.Parse(btn.Tag.ToString());
            switch (clientType)
            {
                case ClientType.Client:
                    clientLogic.SendButtonClicked(buttonTag);
                    break;
                case ClientType.Server:
                    if (gameLogic.CheckIfGameActionLegal(1,btn))
                    {
                        serverLogic.SendButtonClicked(buttonTag);
                        btn.Content = "x";
                    }
                    break;
                default:
                    break;
            }
        }
        private List<Button> CreateGameStateButtonsList()
        {
            List<Button> buttons = new List<Button>();
            foreach (Button b in ButtonsWrapPanel.Children) buttons.Add(b);
            return buttons;
        }
    }
}
