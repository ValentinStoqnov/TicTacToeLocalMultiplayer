using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.WPFPages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        private MainWindow mainWindow;
        
        public MainMenuPage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
        }

        private void MultiPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new JoinOrHostPage(mainWindow);
        }
    }
}
