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
        public DisciplinaList()
        {
            InitializeComponent();
            DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplina.ToList();
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            if (DisciplinaListView.SelectedItem != null)
            {
                try
                {
                        Disciplina student = DisciplinaListView.SelectedItem as Disciplina;
                        if (cmbx.Text == "volume")
                        {
                            student.volume = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "name")
                        {
                            student.name = txtBox.Text;
                        }
                        if (cmbx.Text == "ispolnitel")
                        {
                            student.ispolnitel = txtBox.Text;
                        }                        
                        ConnetionDB.db.SaveChanges();
                        DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplina.ToList();
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
            string kod = txtId.Text;
            string volume = txtVolume.Text;
            string name = txtName.Text;
            string ispolnitel = txtIspoln.Text;

            var tempDisp = new Disciplina()
            {
                kod = Convert.ToInt32(kod),
                volume = Convert.ToInt32(volume),
                name = name,
                ispolnitel = ispolnitel
            };
            try
            {
                ConnetionDB.db.Disciplina.Add(tempDisp);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлена дисциплина");
                DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplina.ToList();
                return;
            }
            catch
            {
                MessageBox.Show("Введены неправильные данные");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Disciplina exam = DisciplinaListView.SelectedItem as Disciplina;
            ConnetionDB.db.Disciplina.Remove(exam);
            ConnetionDB.db.SaveChanges();
            DisciplinaListView.ItemsSource = ConnetionDB.db.Disciplina.ToList();
        }
    }
}
