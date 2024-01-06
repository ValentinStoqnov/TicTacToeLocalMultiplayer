using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.WPFPages
{
    /// <summary>
    /// Interaction logic for WaitingPage.xaml
    /// </summary>
    public partial class WaitingPage : Page
    {
        private MainWindow mainWindow;

        ServerLogic server;
        
        public WaitingPage(MainWindow mw, string hostName)
        {
            InitializeComponent();
            mainWindow = mw;
            server = new ServerLogic(hostName);
            StartServer(hostName);
        }

        private async void StartServer(string hostName)
        {
            bool isGameStarted = await server.StartServer(hostName);
            mainWindow.MwFrame.Content = new GamePage(ClientType.Server,server);
            if (isGameStarted) server.StartGame(hostName);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new JoinOrHostPage(mainWindow);
        }
    }
}
