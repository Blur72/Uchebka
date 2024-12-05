using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
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
    /// Логика взаимодействия для PageQR.xaml
    /// </summary>
    public partial class PageQR : Page
    {
        public PageQR()
        {
            InitializeComponent();
            QRCode.Source = GenerateQrCodeBitmapImage("https://yandex.ru/images/search?from=tabbar&img_url=https%3A%2F%2Fcdn1.ozone.ru%2Fs3%2Fmultimedia-1-7%2F7005371875.jpg&lr=43&pos=0&rpt=simage&text=%D0%BA%D0%B8%D1%80%D0%B8%D0%B5%D1%88%D0%BA%D0%B8");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
