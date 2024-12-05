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
        int tab;
        public SpecialnostList(int TAB)
        {
            InitializeComponent();
            SpecialnostListView.ItemsSource = ConnetionDB.db.Specialities.ToList();
            this.tab = TAB;
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            string kod = txtId.Text;
            string naprav = txtName.Text;
            string shifr = txtIspoln.Text;

            // Проверяем, существует ли Код кафедры в таблице Department
            var departmentExists = ConnetionDB.db.Department.Any(d => d.Code == shifr);
            if (!departmentExists)
            {
                MessageBox.Show("Ошибка: Указанный Код кафедры не существует.");
                return;
            }

            var tempSpec = new Specialities()
            {
                Number = kod,
                Direction = naprav,
                Code_department = shifr,
            };

            try
            {
                ConnetionDB.db.Specialities.Add(tempSpec);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлена специальность");
                SpecialnostListView.ItemsSource = ConnetionDB.db.Specialities.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении специальности: {ex.Message}");
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialnostListView.SelectedItem != null)
            {
                try
                {
                    Specialities student = SpecialnostListView.SelectedItem as Specialities;
                    if (cmbx.Text == "Direction")
                    {
                        student.Direction = txtBox.Text;
                    }
                    if (cmbx.Text == "Code_department")
                    {
                        student.Code_department = txtBox.Text;
                    }
                    ConnetionDB.db.SaveChanges();
                    SpecialnostListView.ItemsSource = ConnetionDB.db.Specialities.ToList();
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
            // Получаем выбранный элемент из списка
            Specialities speciality = SpecialnostListView.SelectedItem as Specialities;

            // Проверяем, есть ли связанные записи в связанных таблицах
            var employees = ConnetionDB.db.Employee.Where(ivan => ivan.Code_department == speciality.Code_department).ToList();
            if (employees.Any())
            {
                // Если есть связанные записи, выводим сообщение и выходим из метода
                MessageBox.Show("Невозможно удалить специальность, так как она связана с записями сотрудников.");
                return;
            }

            // Если зависимостей нет, удаляем специальность
            ConnetionDB.db.Specialities.Remove(speciality);

            // Сохраняем изменения в базе данных
            ConnetionDB.db.SaveChanges();

            // Обновляем источник данных
            SpecialnostListView.ItemsSource = ConnetionDB.db.Specialities.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
