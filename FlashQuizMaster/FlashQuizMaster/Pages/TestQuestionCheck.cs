using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using FlashQuizMaster.ViewModels;
using FlashQuizMaster.BusinessLayer;


namespace FlashQuizMaster.Pages
{
    public class TestQuestionCheck : ContentPage
    {
        private int NbrQuestion = 0;
        private TestQuestionManager TestQuestMgr = new TestQuestionManager();
        private TestQuestionCarousel PageTestQuestion = new TestQuestionCarousel();
        

        public TestQuestionCheck(ChapterViewModel _curChapter)
        {
            var entryNbrQuestion = new Entry {
                Placeholder = "Nombre de questions souhaitées",
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Numeric,
                WidthRequest = 400
            };

            Button btnOk = new Button()
            {
                Text = "Valider",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Silver
            };

            btnOk.Clicked += async (sender, args) =>
            {
                Regex regex = new Regex("^(0|[1-9][0-9]*)$");
                int nbre = 0;
                if(entryNbrQuestion.Text != null && entryNbrQuestion.Text.Trim() != string.Empty)
                {
                    nbre = int.Parse(entryNbrQuestion.Text);
                }
                Match match = regex.Match(nbre.ToString());
                if (match.Success && nbre>0)
                {
                    // await Navigation.PushAsync(new TopicDisplay(curUserVM));//SDI:We go back to the full topics list
                    NbrQuestion = int.Parse(entryNbrQuestion.Text);
                    TestQuestMgr.TotalTestQuestion = NbrQuestion;
                    List<QuestionAnswerViewModel> randomQuestionList = TestQuestMgr.GetRandomQuestions(NbrQuestion, _curChapter);
                    if (randomQuestionList.Count() > 0)
                    {
                        int nb = 0;
                        foreach (var quest in randomQuestionList)
                        {
                            nb = nb + 1;
                            quest.FillAnswerListFromViewModel(quest);
                            var subPage = new TestQuestionDetail(quest, TestQuestMgr,nb);
                            subPage.BindingContext = quest;
                            PageTestQuestion.Children.Add(subPage);                            
                        }                        
                        
                        await Navigation.PushAsync(PageTestQuestion);
                    }
                    else
                    {
                        await DisplayAlert("Information", "Ce chapitre n'a pas de questions. Veuillez créer un questionnaire avant de faire le test", "OK");
                    }
                    Navigation.RemovePage(this);
                }
                else
                {
                    await DisplayAlert("Attention", "veuillez entrer un nombre superieur à zéro.", "Ok");
                }

            };

            
            var stackLayout = new StackLayout
            {
                Children =
                {
                    entryNbrQuestion,btnOk
                },
                BackgroundColor = Color.White
            };
            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}
