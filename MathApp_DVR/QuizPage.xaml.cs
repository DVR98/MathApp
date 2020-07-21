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
    public sealed partial class QuizPage : Page
    {
        public QuizPage()
        {
            this.InitializeComponent();
            Questionaire.Opacity = 0;
        }

        QuizInfo QInfo;
        double EndResult;
        Users User;
        int NumQuestions, Grade;
        string Username, Difficulty, QType;

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            CalculateResult();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            QInfo = e.Parameter as QuizInfo;

            CalculateQuestion();
            Questionaire.Opacity = 100;
        }

        int QuestionNumber, Score;
        double QuestionAnswer;

        private bool Validation()
        {
            bool Valid = false;
            bool NumValid;

            NumValid = float.TryParse(Answer.Text, out float B);

            if (NumValid == true)
            {
                Valid = true;
            }
            else { Valid = false; }

            return Valid;
        }

        async private void CalculateResult()
        {
            bool valid = Validation();

            if(valid == true)
            {
                if (QuestionAnswer == double.Parse(Answer.Text))
                {
                    Score += 1;
                    Answer.Text = "";
                }
                else { Answer.Text = ""; }

                CalculateQuestion();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please enter numbers(Use , for uneven numbers)", "Unvalid Answer");
                await msg.ShowAsync();
            }

            
        }

        private void CalculateQuestion()
        {
            Random rnd = new Random();

            if (QuestionNumber == 0)
            {
                //QuestionNumber += 1;
                NumQuestions = QInfo.NumberofQuestions;
                Grade = QInfo.Grade;
                Username = QInfo.Username;
                Difficulty = QInfo.Difficulty;
                QType = QInfo.QuizType;

                TUsername.Text = Username;
                TGrade.Text = "Grade: " + Grade.ToString();
                TScore.Text = "Score: " + Score.ToString() + " out of " + NumQuestions.ToString() + " so far." ;
                TQuestionsLeft.Text = "Questions left: " + ((NumQuestions) - QuestionNumber).ToString();
                TQuizType.Text = QType;
                QuestionNumber += 1;


                if (QuestionNumber <= NumQuestions)
                {
                    if (Grade == 1)

                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 10);
                            int numTwo = rnd.Next(1, 10);
                            QuestionAnswer = numOne + numTwo;
                            Question.Text = numOne.ToString() + " + " + numTwo.ToString();
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 20);
                            int numTwo = rnd.Next(1, 20);

                            int Randomize = rnd.Next(1, 3);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                        }
                    }
                    else if (Grade == 2)
                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 100);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 4);

                            QuestionAnswer = numOne + numTwo;
                            Question.Text = numOne.ToString() + " + " + numTwo.ToString();
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 100);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 4);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 12);
                                numTwo = rnd.Next(1, 12);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                    }
                    else if (Grade == 3)
                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 100);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 4);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 12);
                                numTwo = rnd.Next(1, 12);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 500);
                            int numTwo = rnd.Next(1, 500);

                            int Randomize = rnd.Next(1, 4);

                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 16);
                                numTwo = rnd.Next(1, 16);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                numOne = rnd.Next(1, 90);
                                numTwo = rnd.Next(1, 10);

                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                    }
                    else if (Grade == 4)
                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 200);
                            int numTwo = rnd.Next(1, 200);

                            int Randomize = rnd.Next(1, 4);

                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 15);
                                numTwo = rnd.Next(1, 15);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                numOne = rnd.Next(1, 90);
                                numTwo = rnd.Next(10, 10);

                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 1000);
                            int numTwo = rnd.Next(1, 1000);

                            int Randomize = rnd.Next(1, 4);

                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 20);
                                numTwo = rnd.Next(1, 20);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                numOne = rnd.Next(10, 90);
                                numTwo = rnd.Next(1, 10);

                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                TQuestionsLeft.Text = "Questions left: " + (NumQuestions - QuestionNumber).ToString();
                TScore.Text = "Score: " + Score.ToString() + " out of " + NumQuestions.ToString() + " so far.";
                QuestionNumber += 1;

                if (QuestionNumber <= NumQuestions)
                {
                    if (Grade == 1)

                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 13);
                            int numTwo = rnd.Next(1, 7);
                            QuestionAnswer = numOne + numTwo;
                            Question.Text = numOne.ToString() + " + " + numTwo.ToString();
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 50);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 3);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                        }
                    }
                    else if (Grade == 2)
                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 50);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 3);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }

                            QuestionAnswer = numOne + numTwo;
                            Question.Text = numOne.ToString() + " + " + numTwo.ToString();
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 100);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 4);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 12);
                                numTwo = rnd.Next(1, 12);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                    }
                    else if (Grade == 3)
                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 100);
                            int numTwo = rnd.Next(1, 12);

                            int Randomize = rnd.Next(1, 4);
                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 12);
                                numTwo = rnd.Next(1, 12);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 10000);
                            int numTwo = rnd.Next(1, 10000);

                            int Randomize = rnd.Next(1, 4);

                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 16);
                                numTwo = rnd.Next(1, 16);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                numOne = rnd.Next(1, 90);
                                numTwo = rnd.Next(1, 10);

                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                    }
                    else if (Grade == 4)
                    {
                        if (Difficulty == "Easy")
                        {
                            int numOne = rnd.Next(1, 200);
                            int numTwo = rnd.Next(1, 200);

                            int Randomize = rnd.Next(1, 4);

                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 100);
                                numTwo = rnd.Next(1, 100);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                numOne = rnd.Next(1, 90);
                                numTwo = rnd.Next(1, 10);

                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                        else if (Difficulty == "Hard")
                        {
                            int numOne = rnd.Next(1, 1000);
                            int numTwo = rnd.Next(1, 1000);

                            int Randomize = rnd.Next(1, 4);

                            if (Randomize == 1)
                            {
                                QuestionAnswer = numOne + numTwo;
                                Question.Text = numOne.ToString() + "+" + numTwo.ToString();
                            }
                            else if (Randomize == 2)
                            {
                                QuestionAnswer = numOne - numTwo;
                                Question.Text = numOne.ToString() + "-" + numTwo.ToString();
                            }
                            else if (Randomize == 3)
                            {
                                numOne = rnd.Next(1, 100);
                                numTwo = rnd.Next(1, 100);

                                QuestionAnswer = numOne * numTwo;
                                Question.Text = numOne.ToString() + " x " + numTwo.ToString();
                            }
                            else
                            {
                                numOne = rnd.Next(1, 90);
                                numTwo = rnd.Next(1, 10);

                                QuestionAnswer = numOne / numTwo;
                                Question.Text = numOne.ToString() + " ÷ " + numTwo.ToString();
                            }
                        }
                    }
                }
                else
                {
                    EndResults();
                }
            }
        }

        private Results RegisterResult()
        {
            var Result = new Results
            {
                User = Username,
                Grade = "Grade: " + Grade.ToString(),
                Difficulty = "Difficulty: " + Difficulty,
                Score = "Score: " + EndResult.ToString() + "%"
            };

            return Result;
        }

        async private void EndResults()
        {
            EndResult = ((double)Score / NumQuestions) * 100;
            EndResult -= EndResult % 1;
            Questionaire.Opacity = 0;

            User = App.Model.Users
                .Where(i =>
                i.Username == QInfo.Username)
                .FirstOrDefault();

            MessageDialog msg = new MessageDialog("Quiz Finished", "You got: " + (EndResult).ToString() + "% for your " + QType);
            await msg.ShowAsync();

            if (QType == "Exam")
            {
                var new_result = RegisterResult();
                App.RModel.Results.Add(new_result);
                await App.SaveResultModelAsync();
                Frame.Navigate(typeof(Homepage), User);
            }
            else
            {
                Frame.Navigate(typeof(Homepage), User);
            }
        }
    }
}
