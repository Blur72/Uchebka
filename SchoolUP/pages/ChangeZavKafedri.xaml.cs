using SchoolUP.db;
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

namespace SchoolUP.pages
{
    public partial class ChangeZavKafedri : Page
    {
        public ChangeZavKafedri()
        {
            InitializeComponent();
            Sotrudnik sotrudnik = ConnetionDB.Sotrudnik;
            if (sotrudnik.Doljnost == "зав. кафедрой")
            {
                btnSpec.Visibility = Visibility.Hidden;
            }
        }

        private void btnExam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamList());
        }

        private void btnDisp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinaList());
        }

        private void btnSotr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SotrudnikList());
        }

        private void btnKafedr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KafedraList());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnSpec_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpecialnostList());
        }
    }
}
