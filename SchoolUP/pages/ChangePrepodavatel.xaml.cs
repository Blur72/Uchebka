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
    /// <summary>
    /// Логика взаимодействия для ChangePrepodavatel.xaml
    /// </summary>
    public partial class ChangePrepodavatel : Page
    {

        int tab;
        public ChangePrepodavatel(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = ConnetionDB.db.Employee.FirstOrDefault(u => u.Tab_Number == tab);
        }

        private void btnExam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamList(tab));
        }

        private void btnDisp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinaList(tab));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
