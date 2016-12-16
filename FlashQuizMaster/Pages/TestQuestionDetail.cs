using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.ViewModels;
using FlashQuizMaster.BusinessLayer;

namespace FlashQuizMaster.Pages
{
    public class TestQuestionDetail : ContentPage
    {      
        private List<string> selectedAnswers = new List<string>();

        public TestQuestionDetail(QuestionAnswerViewModel questVM, TestQuestionManager tqMgr, int rank)
        {
            List<String> RandomAnswerList = Tools.PickRandom(questVM.CurrentAnswers.ToArray(), questVM.CurrentAnswers.Count());
            if (RandomAnswerList != null && RandomAnswerList.Count() > 0)
            {
                switch (RandomAnswerList.Count().ToString())
                {
                    case "1": //SDI: There must be at least 2 elements
                        break;
                    case "2":
                        questVM.CorrectAnswerTextRandomDisplay = RandomAnswerList[0];
                        questVM.FakeAnswerText1RandomDisplay = RandomAnswerList[1];
                        break;
                    case "3":
                        questVM.CorrectAnswerTextRandomDisplay = RandomAnswerList[0];
                        questVM.FakeAnswerText1RandomDisplay = RandomAnswerList[1];
                        questVM.FakeAnswerText2RandomDisplay = RandomAnswerList[2];
                        break;
                    case "4":
                        questVM.CorrectAnswerTextRandomDisplay = RandomAnswerList[0];
                        questVM.FakeAnswerText1RandomDisplay = RandomAnswerList[1];
                        questVM.FakeAnswerText2RandomDisplay = RandomAnswerList[2];
                        questVM.FakeAnswerText3RandomDisplay = RandomAnswerList[3];
                        break;
                }
            }

            var lblChapter = new Label
            {
                Text = "Questions du chapitre " + questVM.QuestionChapterName
            };

            #region lblQuestion

            Label lblQuestion = new Label
            {
                FontSize = 25
            };
            lblQuestion.BindingContext = questVM;
            lblQuestion.SetBinding(Label.TextProperty, new Binding("QuestionText"));

            #endregion

            #region lblCorrectAnswer
            var lblCorrectAnswer = new Label
            {
            };
            lblCorrectAnswer.BindingContext = questVM;
            lblCorrectAnswer.SetBinding(Label.TextProperty, new Binding("CorrectAnswerTextRandomDisplay"));

            //SDI: We add the tapped event to the label
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) =>
                {
                    if (lblCorrectAnswer.TextColor == Color.Purple) //SDI: Unselect the answer
                    {
                        lblCorrectAnswer.TextColor = Color.Black;
                        selectedAnswers.Remove (lblCorrectAnswer.Text);
                    }
                    else //SDI: select the answer
                    {
                        lblCorrectAnswer.TextColor = Color.Purple;
                        selectedAnswers.Add(lblCorrectAnswer.Text);
                    }

                };
            lblCorrectAnswer.GestureRecognizers.Add(tgr);



            #endregion

            #region Fake 1
            var lblFakeAnwser1 = new Label
            {
            };
            lblFakeAnwser1.BindingContext = questVM;
            lblFakeAnwser1.SetBinding(Label.TextProperty, new Binding("FakeAnswerText1RandomDisplay"));

            //SDI: We add the tapped event to the label
            var tgrFakeAnswer1 = new TapGestureRecognizer();
            tgrFakeAnswer1.Tapped += (s, e) =>
            {
                if (lblFakeAnwser1.TextColor == Color.Purple) //SDI: Unselect the answer
                {
                    lblFakeAnwser1.TextColor = Color.Black;
                    selectedAnswers.Remove(lblFakeAnwser1.Text);
                }
                else //SDI: select the answer
                {
                    lblFakeAnwser1.TextColor = Color.Purple;
                    selectedAnswers.Add(lblFakeAnwser1.Text);
                }

            };
            lblFakeAnwser1.GestureRecognizers.Add(tgrFakeAnswer1);
            #endregion

            #region Fake 2
            var lblFakeAnwser2 = new Label
            {
            };

            lblFakeAnwser2.BindingContext = questVM;
            lblFakeAnwser2.SetBinding(Label.TextProperty, new Binding("FakeAnswerText2RandomDisplay"));

            //SDI: We add the tapped event to the label
            var tgrFakeAnswer2 = new TapGestureRecognizer();
            tgrFakeAnswer2.Tapped += (s, e) =>
            {
                if (lblFakeAnwser2.TextColor == Color.Purple) //SDI: Unselect the answer
                {
                    lblFakeAnwser2.TextColor = Color.Black;
                    selectedAnswers.Remove(lblFakeAnwser2.Text);
                }
                else //SDI: select the answer
                {
                    lblFakeAnwser2.TextColor = Color.Purple;
                    selectedAnswers.Add(lblFakeAnwser2.Text);
                }

            };
            lblFakeAnwser2.GestureRecognizers.Add(tgrFakeAnswer2);
            #endregion

            #region Fake 3
            var lblFakeAnwser3 = new Label
            {
            };

            lblFakeAnwser3.BindingContext = questVM;
            lblFakeAnwser3.SetBinding(Label.TextProperty, new Binding("FakeAnswerText3RandomDisplay"));

            //SDI: We add the tapped event to the label
            var tgrFakeAnswer3 = new TapGestureRecognizer();
            tgrFakeAnswer3.Tapped += (s, e) =>
            {
                if (lblFakeAnwser3.TextColor == Color.Purple) //SDI: Unselect the answer
                {
                    lblFakeAnwser3.TextColor = Color.Black;
                    selectedAnswers.Remove(lblFakeAnwser3.Text);
                }
                else //SDI: select the answer
                {
                    lblFakeAnwser3.TextColor = Color.Purple;
                    selectedAnswers.Add(lblFakeAnwser3.Text);
                }

            };
            lblFakeAnwser3.GestureRecognizers.Add(tgrFakeAnswer3);
            #endregion

            Button btnCheck = new Button()
            {
                Text = "Verifier",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Silver
            };

            btnCheck.Clicked += async (sender, args) =>
            {
                //questVM.SaveQuestionVM();
                await DisplayAlert("Information", "votre question a été verifiée!", "OK");
                questVM.TestScore = questVM.TestScore + 1;
               
                //SDI:We count the correct answers
                tqMgr.GetTotalCorrectAnswer(this.QuestionsIsCorrect(questVM, selectedAnswers));


                int nbPage = ((TestQuestionCarousel)this.Parent).Children.Count();
                if(rank== nbPage)
                {
                    //SDI: if we are on the last question, then only we add the result page
                    ((TestQuestionCarousel)this.Parent).Children.Add(new TestQuestionResult(tqMgr,questVM));
                }                
                Tools.PageRight((TestQuestionCarousel)this.Parent);       
                        
            };
     
            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblChapter,
                    lblQuestion,
                    lblCorrectAnswer,
                    lblFakeAnwser1,
                    lblFakeAnwser2,
                    lblFakeAnwser3,
                    btnCheck
                },
                BackgroundColor = Color.White
            };

            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }

        /// <summary>
        /// Check if the selected answers are giving the correct answer
        /// </summary>
        /// <param name="qvm"></param>
        /// <returns></returns>
        public bool QuestionsIsCorrect(QuestionAnswerViewModel qvm, List<string> answerList)
        {
            //SDI: In this version of the tool, there can only be one correct answer (To be changed in V1.1)
            bool res = false;
            if (answerList.Count()==1 && qvm.CorrectAnswerText == answerList[0])
            {
                res = true;
            }
            return res;
        }

    }
}
