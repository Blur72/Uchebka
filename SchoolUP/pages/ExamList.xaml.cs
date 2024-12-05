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
        int tab;
        public ExamList(int TAB)
        {
            InitializeComponent();
            this.tab = TAB;
            var tempUser = ConnetionDB.db.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            if (tempUser.Position == "преподаватель")
            {
                ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                cmbx.Visibility = Visibility.Hidden;
                stackAdd.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Hidden;
            }
            else
            {
                ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                cmbx.Visibility = Visibility.Visible;
                stackAdd.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Visible;
            }
        }

        private void btnRedact_Click(object sender, RoutedEventArgs e)
        {
            var tempUser = ConnetionDB.db.Employee.FirstOrDefault(u => u.Tab_Number == tab);
            if (ExamListView.SelectedItem != null)
            {
                try
                {
                    if (tempUser.Position == "зав. кафедрой")
                    {
                        Exam student = ExamListView.SelectedItem as Exam;
                        if (cmbx.Text == "Date")
                        {
                            student.Date = Convert.ToDateTime(txtBox.Text);
                        }
                        if (cmbx.Text == "Code")
                        {
                            student.Code = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "Reg_number")
                        {
                            var emp = ConnetionDB.db.Exam.FirstOrDefault(a => a.Id_exam == Convert.ToInt32(txtBox.Text));
                            if (emp != null)
                            {
                                student.Reg_number = Convert.ToInt32(txtBox.Text);
                            }
                            else
                            {
                                MessageBox.Show("Такого рег номера не существует");
                            }
                            student.Tab_number = Convert.ToInt32(txtBox.Text);
                        }
                        if (cmbx.Text == "Tab_number")
                        {
                            var emp = ConnetionDB.db.Employee.FirstOrDefault(a => a.Tab_Number == Convert.ToInt32(txtBox.Text));
                            if (emp != null)
                            {
                                student.Tab_number = Convert.ToInt32(txtBox.Text);
                            }
                            else
                            {
                                MessageBox.Show("Такого таб номера не существует");
                            }
                        }
                        if (cmbx.Text == "Auditorium")
                        {
                            student.Auditorium = txtBox.Text;
                        }
                        if (cmbx.Text == "Grade")
                        {
                            if (Convert.ToInt32(txtBox.Text) > 1 && Convert.ToInt32(txtBox.Text) < 6)
                            {
                                student.Grade = Convert.ToInt32(txtBox.Text);
                            }
                            else
                            {
                                MessageBox.Show("Оценка может быть от 2 до 5");
                            }
                        }
                        ConnetionDB.db.SaveChanges();
                        ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                        return;
                    }  
                    else
                    {
                        if (Convert.ToInt32(txtBox.Text) > 1 && Convert.ToInt32(txtBox.Text) < 6)
                        {
                            Exam student = ExamListView.SelectedItem as Exam;
                            student.Grade = Convert.ToInt32(txtBox.Text);
                            ConnetionDB.db.SaveChanges();
                            ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Оценка может быть от 2 до 5");
                        }
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
            string date = txtDate.Text;
            string kod = txtKod.Text;
            string regnomer = txtRegNomer.Text;
            string tabnomer = txtTabNomer.Text;
            string audit = txtAuditor.Text;
            string ocenka = txtOcenk.Text;

            if (!int.TryParse(ocenka, out int grade) || grade < 2 || grade > 5)
            {
                MessageBox.Show("Оценка должна быть в диапазоне от 2 до 5.");
                return;
            }
            try
            {
                var tempExam = new Exam()
                {
                    Date = Convert.ToDateTime(date),
                    Code = Convert.ToInt32(kod),
                    Reg_number = Convert.ToInt32(regnomer),
                    Tab_number = Convert.ToInt32(tabnomer),
                    Auditorium = audit,
                    Grade = Convert.ToInt32(ocenka)
                };

                ConnetionDB.db.Exam.Add(tempExam);
                ConnetionDB.db.SaveChanges();
                MessageBox.Show("Добавлен экзамен");
                ExamListView.ItemsSource = ConnetionDB.db.Exam.ToList();
                return;
            } 
            catch
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
