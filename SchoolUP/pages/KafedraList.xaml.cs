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
        int tab;
        public KafedraList(int TAB)
        {
            InitializeComponent();
            KafedraListView.ItemsSource = ConnetionDB.db.Department.ToList();
            this.tab = TAB;
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string shifr = txtId.Text;
            string name = txtName.Text;
            string fakultet = txtIspoln.Text;

            var tempKafedr= new Department()
            {
                Code = shifr,
                Name = name,
                FacultyAbbreviation = fakultet,
            };
            try
            {
                ConnetionDB.db.Department.Add(tempKafedr);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлена кафедра");
                KafedraListView.ItemsSource = ConnetionDB.db.Department.ToList();
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
                    Department student = KafedraListView.SelectedItem as Department;
                    if (cmbx.Text == "name")
                    {
                        student.Name = txtBox.Text;
                    }
                    ConnetionDB.db.SaveChanges();
                    KafedraListView.ItemsSource = ConnetionDB.db.Department.ToList();
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
            Department department = KafedraListView.SelectedItem as Department;

            var employees = ConnetionDB.db.Employee.Where(eпрапр => eпрапр.Code_department == department.Code).ToList();
            if (employees.Any())
            {
                MessageBox.Show("Невозможно удалить кафедру, так как она связана с записями сотрудников.");
                return;
            }
            ConnetionDB.db.Department.Remove(department);
            ConnetionDB.db.SaveChanges();
            KafedraListView.ItemsSource = ConnetionDB.db.Department.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void txtfil_TextChanged(object sender, TextChangedEventArgs e)
        {
            Department kafedra = ConnetionDB.department;
            string searchText = txtfil.Text.ToLower();
            KafedraListView.ItemsSource = ConnetionDB.db.Department.ToList().Where(s => s.Name.ToLower().Contains(searchText)).ToList();
        }

        private void cmbx1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Department kafedra = ConnetionDB.department;
            string searchText = txtfil.Text.ToLower();
            if (cmbx1.Text == "Name")
            {
                KafedraListView.ItemsSource = ConnetionDB.db.Department.ToList().OrderBy(k => k.Name);
            }
        }
    }
}
