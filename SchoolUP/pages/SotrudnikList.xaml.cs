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

        int Tab;
        public SotrudnikList(int TAB)
        {
            InitializeComponent();
            this.Tab = TAB;
            var tempUser = ConnetionDB.db.Employee.FirstOrDefault(u => u.Tab_Number == Tab);
            SotrudnikListView.ItemsSource = ConnetionDB.db.Employee.ToList();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string kod = txtId.Text;
            string shifr = txtVolume.Text;
            string fam = txtName.Text;
            string dolj = txtIspoln.Text;
            string zarplata = txtNam.Text;
            string shef = txtIspln.Text;

            var tempDisp = new Employee()
            {
                Tab_Number = Convert.ToInt32(kod),
                Code_department = shifr,
                Last_Name = fam,
                Position = dolj,
                Salary = Convert.ToInt32(zarplata),
                Chief = Convert.ToInt32(shef)
            };
            try
            {
                ConnetionDB.db.Employee.Add(tempDisp);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлен сотрудник");
                SotrudnikListView.ItemsSource = ConnetionDB.db.Employee.ToList();
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
                    Employee student = SotrudnikListView.SelectedItem as Employee;
                    var sakdm = ConnetionDB.db.Employee.FirstOrDefault(asdsad => asdsad.Tab_Number == student.Tab_Number);
                    if (cmbx.Text == "Code_department")
                    {
                        student.Code_department = txtBox.Text;
                    }
                    if (cmbx.Text == "Last_Name")
                    {
                        student.Last_Name = txtBox.Text;
                    }
                    if (cmbx.Text == "Position")
                    {
                        var emp = ConnetionDB.db.Employee.FirstOrDefault(a => a.Position == txtBox.Text);
                        if (emp != null)
                        {
                            student.Position = txtBox.Text; 
                        }
                        else
                        {
                            MessageBox.Show("Такой должности не существует");
                        }
                    }
                    if (cmbx.Text == "Salary")
                    {
                        if (Convert.ToInt32(txtBox.Text) > 0)
                        {
                            student.Salary = Convert.ToInt32(txtBox.Text);
                        }
                        else
                        {
                            MessageBox.Show("Оклад не может быть меньше 0");
                        }
                    }
                    if (cmbx.Text == "Chief")
                    {
                        var empp = ConnetionDB.db.Employee.FirstOrDefault(b=>b.Tab_Number == student.Tab_Number);
                        if (empp != null)
                        {
                            student.Chief = Convert.ToInt32(txtBox.Text);
                        }
                        else
                        {
                            MessageBox.Show("Такого шефа не существует");
                        }
                    }
                    ConnetionDB.db.SaveChanges();
                    SotrudnikListView.ItemsSource = ConnetionDB.db.Employee.ToList();
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
            Employee employee = SotrudnikListView.SelectedItem as Employee;

            if (employee != null && employee.Tab_Number == Tab)
            {
                MessageBox.Show("Невозможно удалить текущего пользователя.");
                return;
            }

            if (employee != null)
            {
                ConnetionDB.db.Employee.Remove(employee);
                ConnetionDB.db.SaveChanges();
                SotrudnikListView.ItemsSource = ConnetionDB.db.Employee.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void txtfil_TextChanged(object sender, TextChangedEventArgs e)
        {
            Employee sotrudnik = ConnetionDB.employee;
            string searchText = txtfil.Text.ToLower();
            SotrudnikListView.ItemsSource = ConnetionDB.db.Employee.ToList().Where(s => s.Last_Name.ToLower().Contains(searchText)).ToList();
        }

        private void SotrudnikListView_Loaded(object sender, RoutedEventArgs e)
        {
            SotrudnikListView.ItemsSource = ConnetionDB.db.Employee.ToList();
        }
    }
}
