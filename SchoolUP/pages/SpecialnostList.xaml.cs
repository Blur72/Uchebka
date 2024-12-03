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
    /// Логика взаимодействия для SpecialnostList.xaml
    /// </summary>
    public partial class SpecialnostList : Page
    {
        public SpecialnostList()
        {
            InitializeComponent();
            SpecialnostListView.ItemsSource = ConnetionDB.db.Specialnost.ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string kod = txtId.Text;
            string naprav = txtName.Text;
            string shifr = txtIspoln.Text;

            var tempSpec = new Specialnost()
            {
                nomer = kod,
                napravlenie = naprav,
                shifr = shifr,
            };
            try
            {
                ConnetionDB.db.Specialnost.Add(tempSpec);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлена специальность");
                SpecialnostListView.ItemsSource = ConnetionDB.db.Specialnost.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialnostListView.SelectedItem != null)
            {
                try
                {
                    Specialnost student = SpecialnostListView.SelectedItem as Specialnost   ;
                    if (cmbx.Text == "napravlenie")
                    {
                        student.napravlenie = txtBox.Text;
                    }
                    if (cmbx.Text == "shifr")
                    {
                        student.shifr = txtBox.Text;
                    }
                    ConnetionDB.db.SaveChanges();
                    SpecialnostListView.ItemsSource = ConnetionDB.db.Specialnost.ToList();
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
            Specialnost exam = SpecialnostListView.SelectedItem as Specialnost;
            ConnetionDB.db.Specialnost.Remove(exam);
            ConnetionDB.db.SaveChanges();
            SpecialnostListView.ItemsSource = ConnetionDB.db.Specialnost.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
