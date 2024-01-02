using System;
using System.Collections.Generic;
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
    /// Interaction logic for ServerBrowserPage.xaml
    /// </summary>
    public partial class ServerBrowserPage : Page
    {
        private MainWindow mainWindow;

        public ServerBrowserPage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
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
