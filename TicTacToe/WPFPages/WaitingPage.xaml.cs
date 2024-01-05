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
        
        public WaitingPage(MainWindow mw, string hostName)
        {
            InitializeComponent();
            mainWindow = mw;
            ServerLogic serverLogic = new ServerLogic(hostName);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new JoinOrHostPage(mainWindow);
        }
    }
}
