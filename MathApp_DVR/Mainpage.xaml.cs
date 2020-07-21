using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MathApp_DVR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var User = App.Model.Users
                .Where(i =>
                i.Username == UserName.Text &&
                i.Password == PassWord.Password)
                .FirstOrDefault();

            if ((User != null) && (PassWord.Password != ""))
            {
                Frame.Navigate(typeof(Homepage), User);
            }
            else
            {
                MessageDialog message = new MessageDialog("Please enter the correct information", "Wrong Username or Password!");
                await message.ShowAsync();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registration));
        }
    }
}
