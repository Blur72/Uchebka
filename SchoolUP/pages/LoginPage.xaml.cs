using QRCoder;
using SchoolUP.db;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        static MainWindow _mainWindow;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            QRCode.Source = GenerateQrCodeBitmapImage("https://dom.mck-ktits.ru/");
        }

        private void Btn_click(object sender, RoutedEventArgs e)
        {
            int login = Convert.ToInt32(txtLogin.Text);
            var user = ConnetionDB.db.Sotrudnik.FirstOrDefault(l => l.tab_nomer == login);
            if (user != null)
            {
                if(user.Doljnost == "преподаватель")
                {
                    ConnetionDB.Sotrudnik = user;
                    _mainWindow.MainFrame.NavigationService.Navigate(new ChangePrepodavatel());
                }
                if (user.Doljnost == "зав. кафедрой")
                {
                    ConnetionDB.Sotrudnik = user;
                    _mainWindow.MainFrame.NavigationService.Navigate(new ChangeZavKafedri());
                }
                if (user.Doljnost == "инженер")
                {
                    ConnetionDB.Sotrudnik = user;
                    _mainWindow.MainFrame.NavigationService.Navigate(new ChangeZavKafedri());
                }

            }
            else
            {
                MessageBox.Show("Такого сотрудника нет");
            }
        }
        private BitmapImage GenerateQrCodeBitmapImage(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q); using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrBitmap.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            BitmapImage bitmapImage = new BitmapImage(); bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; bitmapImage.StreamSource = ms;
                            bitmapImage.EndInit();
                            return bitmapImage;
                        }
                    }
                }
            }
        }
    }
}
