using System.Windows.Controls;


namespace TicTacToe.WPFPages
{
    /// <summary>
    /// Interaction logic for JoinOrHostPage.xaml
    /// </summary>
    public partial class JoinOrHostPage : Page
    {
        private MainWindow mainWindow;
        
        public JoinOrHostPage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
        }

        private void JoinGameBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new ServerBrowserPage(mainWindow);
        }

        private void HostGameBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new MainMenuPage(mainWindow);
        }
    }
}
