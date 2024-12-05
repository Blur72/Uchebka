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
        int Tab;
        public ChangeZavKafedri(int TAB)
        {
            InitializeComponent();
            this.Tab = TAB;
            var tempUser = ConnetionDB.db.Employee.FirstOrDefault(u=>u.Tab_Number == Tab);
            if (tempUser.Position == "зав. кафедрой")
            {
                btnSpec.Visibility = Visibility.Hidden;
            }

        }

        private void btnExam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamList(Tab));
        }

        private void btnDisp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinaList(Tab));
        }

        private void btnSotr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SotrudnikList(Tab));
        }

        private void btnKafedr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KafedraList(Tab));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnSpec_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpecialnostList(Tab));
        }
    }
}
