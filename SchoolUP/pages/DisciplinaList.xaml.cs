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
    public partial class DisciplinaList : Page
    {
        int tab;
        public DisciplinaList(int TAB)
        {
            InitializeComponent();
            DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplines.ToList();
            this.tab = TAB;
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinaListView.SelectedItem != null)
            {
                try
                {
                        Disciplines student = DisciplinaListView.SelectedItem as Disciplines;
                        if (cmbx.Text == "Volume")
                        {
                            student.Volume = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "Name")
                        {
                            student.Name = txtBox.Text;
                        }
                        if (cmbx.Text == "Code_department")
                        {
                            student.Code_department = txtBox.Text;
                        }                        
                        ConnetionDB.db.SaveChanges();
                        DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplines.ToList();
                        return;
                }
                catch
                {
                    MessageBox.Show("Данные неправильные");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_add(object sender, RoutedEventArgs e)
        {
            try
            {
                string volume = txtVolume.Text;
                string name = txtName.Text;
                string cod = txtIspoln.Text;

                var tempDisp = new Disciplines()
                {
                    Volume = Convert.ToInt32(volume),
                    Name = name,
                    Code_department = cod
                };

                ConnetionDB.db.Disciplines.Add(tempDisp);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлена дисциплина");
                DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplines.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Disciplines exam = DisciplinaListView.SelectedItem as Disciplines;
            ConnetionDB.db.Disciplines.Remove(exam);
            ConnetionDB.db.SaveChanges();
            DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplines.ToList();
        }
    }
}
