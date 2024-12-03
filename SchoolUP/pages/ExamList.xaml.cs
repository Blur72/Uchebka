using SchoolUP.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using Wpf.Ui.Controls;
using static QRCoder.PayloadGenerator;
using MessageBox = System.Windows.MessageBox;

namespace SchoolUP.pages
{
    public partial class ExamList : Page
    {
        public ExamList()
        {
            InitializeComponent();
            ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
            Sotrudnik sotrudnik = ConnetionDB.Sotrudnik;
            if (sotrudnik.Doljnost == "преподаватель")
            {
                cmbx.Visibility = Visibility.Hidden;
                stackAdd.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Hidden;
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            Sotrudnik sotrudnik = ConnetionDB.Sotrudnik;
            if (ExamListView.SelectedItem != null)
            {
                try
                {
                    if (sotrudnik.Doljnost == "зав. кафедрой")
                    {
                        Exam student = ExamListView.SelectedItem as Exam;
                        if (cmbx.Text == "date")
                        {
                            student.date = txtBox.Text;
                        }
                        if (cmbx.Text == "kod")
                        {
                            student.kod = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "reg_nomer")
                        {
                            student.reg_nomer = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "tab_nomer")
                        {
                            student.tab_nomer = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "auditorya")
                        {
                            student.auditorya = txtBox.Text;
                        }
                        if (cmbx.Text == "ocenka")
                        {
                            student.ocenka = Convert.ToInt32(txtBox.Text);
                        }
                        ConnetionDB.db.SaveChanges();
                        ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                        return;
                    }  
                    else
                    {
                        Exam student = ExamListView.SelectedItem as Exam;
                        student.ocenka = Convert.ToInt32(txtBox.Text);
                        ConnetionDB.db.SaveChanges();
                        ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                    }
                }
                catch { 
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
            string id = txtId.Text;
            string date = txtDate.Text;
            string kod = txtKod.Text;
            string regnomer = txtRegNomer.Text;
            string tabnomer = txtTabNomer.Text;
            string audit = txtAuditor.Text;
            string ocenka = txtOcenk.Text;

            var tempExam = new Exam()
            {
                id = Convert.ToInt32(id),
                date = date,
                kod = Convert.ToInt32(kod),
                reg_nomer = Convert.ToInt32(regnomer),
                tab_nomer = Convert.ToInt32(tabnomer),
                auditorya = audit,
                ocenka = Convert.ToInt32(ocenka)
            };
            try
            {
                ConnetionDB.db.Exam.Add(tempExam);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлен экзамен");
                ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                return;
            } catch
            { 
                MessageBox.Show("Введены неправильные данные");
            }

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Exam exam = ExamListView.SelectedItem as Exam;
            ConnetionDB.db.Exam.Remove(exam);
            ConnetionDB.db.SaveChanges();
            ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
        }
    }
}
