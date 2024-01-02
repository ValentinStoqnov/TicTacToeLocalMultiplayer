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
    /// Interaction logic for WaitingPage.xaml
    /// </summary>
    public partial class WaitingPage : Page
    {
        private MainWindow mainWindow;
        
        public WaitingPage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
            ServerLogic serverLogic = new ServerLogic();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MwFrame.Content = new JoinOrHostPage(mainWindow);
        }
    }
}
