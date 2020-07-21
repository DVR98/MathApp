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
    public sealed partial class QuizInformationPage : Page
    {
        public QuizInformationPage()
        {
            this.InitializeComponent();
        }

        Users User;
        QuizInfo QuizInformation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User = e.Parameter as Users;
        }

        string Difficulty, QType;
        int grade;

        private void Difficulty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)Difficulties.SelectedItem;
            Difficulty = typeItem.Content.ToString();
        }

        private void Grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)Grade.SelectedItem;
            grade = int.Parse(typeItem.Content.ToString());
        }

        private bool Valid()
        {
            bool Valid = true;

            if (Difficulty == null)
            {
                Valid = false;
            }
            else if (grade <= 0)
            {
                Valid = false;
            }
            else if (QType == null)
            {
                Valid = false;
            }
            else if ((NumberQuestions.Text != "") || (NumberQuestions.Text != null))
            {
                var num = int.TryParse(NumberQuestions.Text, out int i);

                if (num != true)
                {
                    Valid = false;
                }
                else
                {
                    Valid = true;
                }
            }
            else if ((NumberQuestions.Text == "") || (NumberQuestions.Text == null)) { Valid = false; }

            return Valid;
        }

        async private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool Validation = Valid();

            if (Validation == true)
            {
                QuizInformation = new QuizInfo {
                Difficulty = Difficulty,
                Grade = grade,
                Username = User.Username,
                QuizType = QType,
                NumberofQuestions = int.Parse(NumberQuestions.Text)
                };

                Frame.Navigate(typeof(QuizPage), QuizInformation);
            }
            else
            {
                MessageDialog msg = new MessageDialog("Check your options", "You forgot to fill in something needed to proceed or you entered an uneven number");
                await msg.ShowAsync();
            }    
        }

        private void btnHomepage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Homepage), User);
        }

        private void QuizType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)QuizType.SelectedItem;
            QType = typeItem.Content.ToString();
        }
    }
}
