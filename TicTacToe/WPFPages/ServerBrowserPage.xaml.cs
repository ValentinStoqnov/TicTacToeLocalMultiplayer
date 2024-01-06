using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Models;

namespace TicTacToe.WPFPages
{
    /// <summary>
    /// Interaction logic for ServerBrowserPage.xaml
    /// </summary>
    public partial class ServerBrowserPage : Page
    {
        private MainWindow mainWindow;
        private ClientLogic client;
        public ObservableCollection<ServerModel>? ServerList { get; set; }

        public ServerBrowserPage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            client = new ClientLogic("PlaceHolderPLayerName");
            GetServerList();
        }
        private async void GetServerList()
        {
            ServerList = await client.RequestServerNames();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new JoinOrHostPage(mainWindow);
        }

        private async void JoinBtn_Click(object sender, RoutedEventArgs e)
        {
            ServerModel? serverChosen = ServerBrowserLv.SelectedItem as ServerModel;
            bool canStartGame = await client.JoinServer(serverChosen);
            if (canStartGame)
            {
                client.StartGame();
                mainWindow.MwFrame.Content = new GamePage(ClientType.Client, client);

            }
        }
    }
}
