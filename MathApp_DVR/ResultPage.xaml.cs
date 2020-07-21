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
    public sealed partial class ResultPage : Page
    {
        public ResultPage()
        {
            this.InitializeComponent();

            this.Loaded += Dashboard_Loaded;
        }

        Users user;
        Results result;
        string U;
        string G;
        string D;
        string S;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = e.Parameter as Users;
            WelcomeMessage.Text = user.Username + "'s Results!";

        }

        private void Dashboard_Loaded
            (object sender, RoutedEventArgs e)
        {
                Gridview_Results.DataContext = App.RModel.Results
                .Where(i =>
                i.User == user.Username);

                Gridview_AllResults.DataContext = App.RModel.Results;
        }

        private void btnHomepage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Homepage), user);
        }
    }
}
