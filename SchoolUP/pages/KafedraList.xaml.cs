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
    /// Логика взаимодействия для KafedraList.xaml
    /// </summary>
    public partial class KafedraList : Page
    {
        public KafedraList()
        {
            InitializeComponent();
            KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string shifr = txtId.Text;
            string name = txtName.Text;
            string fakultet = txtIspoln.Text;

            var tempKafedr= new Kafedra()
            {
                shifr = shifr,
                name = name,
                fakultet = fakultet,
            };
            try
            {
                ConnetionDB.db.Kafedra.Add(tempKafedr);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлена кафедра");
                KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (KafedraListView.SelectedItem != null)
            {
                try
                {
                    Kafedra student = KafedraListView.SelectedItem as Kafedra;
                    if (cmbx.Text == "name")
                    {
                        student.name = txtBox.Text;
                    }
                    if (cmbx.Text == "fakultet")
                    {
                        student.fakultet = txtBox.Text;
                    }
                    ConnetionDB.db.SaveChanges();
                    KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList();
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
            Kafedra exam = KafedraListView.SelectedItem as Kafedra;
            ConnetionDB.db.Kafedra.Remove(exam);
            ConnetionDB.db.SaveChanges();
            KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void txtfil_TextChanged(object sender, TextChangedEventArgs e)
        {
            Kafedra kafedra = ConnetionDB.kafedra;
            string searchText = txtfil.Text.ToLower();
            KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList().Where(s => s.name.ToLower().Contains(searchText)).ToList();
        }

        private void cmbx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kafedra kafedra = ConnetionDB.kafedra;
            string searchText = txtfil.Text.ToLower();
            if (cmbx1.Text == "name")
            {
                KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList().OrderByDescending(k => k.name);
            }
            if (cmbx1.Text == "fakultet")
            {
                KafedraListView.ItemsSource = ConnetionDB.db.Kafedra.ToList().OrderBy(k => k.fakultet);
            }
        }
    }
}
