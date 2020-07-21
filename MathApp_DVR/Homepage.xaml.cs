using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MathApp_DVR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Homepage : Page
    {
        public Homepage()
        {
            this.InitializeComponent();
        }

        Users user;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = e.Parameter as Users;
            WelcomeMessage.Text = "Welcome, " + user.Username + "!";
             
        }

        private void Quiz_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(QuizInformationPage), user);
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ResultPage), user);
        }

        private void btnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
