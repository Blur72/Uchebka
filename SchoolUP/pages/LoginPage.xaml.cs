using QRCoder;
using SchoolUP.db;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

namespace SchoolUP.pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        static MainWindow _mainWindow;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Btn_click(object sender, RoutedEventArgs e)
        {
            int tabn = Convert.ToInt32(txtLogin.Text);
            var tempUs = ConnetionDB.db.Employee.FirstOrDefault(l => l.Tab_Number == tabn);
            if (tempUs != null)
            {
                if(tempUs.Position == "преподаватель")
                {
                    MessageBox.Show("Здравствуйте преподаватель");
                    _mainWindow.MainFrame.NavigationService.Navigate(new ChangePrepodavatel(tempUs.Tab_Number));
                }
                if (tempUs.Position == "зав. кафедрой")
                {
                    MessageBox.Show("Здравствуйте зав. кафедрой");
                    _mainWindow.MainFrame.NavigationService.Navigate(new ChangeZavKafedri(tempUs.Tab_Number));
                }
                if (tempUs.Position == "инженер")
                {
                    MessageBox.Show("Здравствуйте инженер");
                    _mainWindow.MainFrame.NavigationService.Navigate(new ChangeZavKafedri(tempUs.Tab_Number));
                }

            }
            else
            {
                MessageBox.Show("Такого сотрудника нет");
            }
        }
    }
}
