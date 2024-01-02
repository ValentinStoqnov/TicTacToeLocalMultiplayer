using System.Windows;
using TicTacToe.WPFPages;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MwFrame.Content = new MainMenuPage(this);
        }
    }
}
