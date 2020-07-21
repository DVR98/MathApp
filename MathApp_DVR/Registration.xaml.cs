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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MathApp_DVR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Registration : Page
    {
        public Registration()
        {
            this.InitializeComponent();
            Valid = false;
        }

        private Users RegisterUser()
        {
            var user = App.Model.Users
                 .Where(i =>
                 i.Username == NewUsername.Text)
                 .FirstOrDefault();

            if ((user == null) && (NewPassword.Text != ""))
            {
                user = new Users
                {
                    Username = NewUsername.Text,
                    Password = NewPassword.Text
                };

                Valid = true;
            }
            else
            {
                Valid = false;
            }

            return user;
        }

        public bool Valid;

        async private void IsUserValid()
        {
            var new_user = RegisterUser();

            if (Valid == true)
            {
                App.Model.Users.Add(new_user);
                await App.SaveUserModelAsync();
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                MessageDialog message = new MessageDialog("Please enter a new username and password", "Username already exists  or you gave invalid information!");
                await message.ShowAsync();
            } 
        }

        private void btnRegister1_Click(object sender, RoutedEventArgs e)
        {
            IsUserValid();
        }

        private void NavigateToLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
