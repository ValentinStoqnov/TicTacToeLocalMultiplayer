using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.WPFPages
{
    /// <summary>
    /// Interaction logic for ServerBrowserPage.xaml
    /// </summary>
    public partial class ServerBrowserPage : Page
    {
        private MainWindow mainWindow;
        public ObservableCollection<string> ServerList { get; set; }

        public ServerBrowserPage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            ServerList = ClientLogic.GetAllServerNames().Result;
            ServerBrowserLv.ItemsSource = ServerList;

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new JoinOrHostPage(mainWindow);
        }

        private void JoinBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
