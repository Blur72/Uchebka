using SchoolUP.db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для SotrudnikList.xaml
    /// </summary>
    public partial class SotrudnikList : Page
    {
        public SotrudnikList()
        {
            InitializeComponent();
            SotrudnikListView.ItemsSource = ConnetionDB.db.Sotrudnik.ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string kod = txtId.Text;
            string shifr = txtVolume.Text;
            string fam = txtName.Text;
            string dolj = txtIspoln.Text;
            string zarplata = txtNam.Text;
            string shef = txtIspln.Text;

            var tempDisp = new Sotrudnik()
            {
                tab_nomer = Convert.ToInt32(kod),
                shifr = shifr,
                Familiya = fam,
                Doljnost = dolj,
                Zarplata = Convert.ToInt32(zarplata),
                Shef = Convert.ToInt32(shef)
            };
            try
            {
                ConnetionDB.db.Sotrudnik.Add(tempDisp);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлен сотрудник");
                SotrudnikListView.ItemsSource = ConnetionDB.db.Sotrudnik.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (SotrudnikListView.SelectedItem != null)
            {
                try
                {
                    Sotrudnik student = SotrudnikListView.SelectedItem as Sotrudnik;
                    if (cmbx.Text == "shifr")
                    {
                        student.shifr = txtBox.Text;
                    }
                    if (cmbx.Text == "Familiya")
                    {
                        student.Familiya = txtBox.Text;
                    }
                    if (cmbx.Text == "Doljnost")
                    {
                        student.Doljnost = txtBox.Text;
                    }
                    if (cmbx.Text == "Zarplata")
                    {
                        student.Zarplata = Convert.ToInt32(txtBox.Text);
                    }
                    if (cmbx.Text == "Shef")
                    {
                        student.Shef = Convert.ToInt32(txtBox.Text);
                    }
                    ConnetionDB.db.SaveChanges();
                    SotrudnikListView.ItemsSource = ConnetionDB.db.Sotrudnik.ToList();
                    return;
                }
                catch
                {
                    MessageBox.Show("Данные неправильные");
                }
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Sotrudnik exam = SotrudnikListView.SelectedItem as Sotrudnik;
            ConnetionDB.db.Sotrudnik.Remove(exam);
            ConnetionDB.db.SaveChanges();
            SotrudnikListView.ItemsSource = ConnetionDB.db.Sotrudnik.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void txtfil_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sotrudnik sotrudnik = ConnetionDB.Sotrudnik;
            string searchText = txtfil.Text.ToLower();
            SotrudnikListView.ItemsSource = ConnetionDB.db.Sotrudnik.ToList().Where(s => s.Familiya.ToLower().Contains(searchText)).ToList();
        }
    }
}
