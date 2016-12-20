using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.ViewModels;

namespace FlashQuizMaster.Pages
{
    public class CustomQuestionCell : ImageCell
    {
        public CustomQuestionCell()
        {

            //var createAction = new MenuItem { Text = "Créer" };
            //createAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            //createAction.Clicked += (sender, e) =>
            //{
            //    var mi = ((MenuItem)sender);
            //    var quest = (QuestionAnswerViewModel)mi.CommandParameter;
            //    ((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).Navigation.PushAsync(new CreateQuestionAnswer(quest));
            //};
            var editAction = new MenuItem { Text = "Editer" };
            editAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            editAction.Clicked += (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                var quest = (QuestionAnswerViewModel)mi.CommandParameter;
                ((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).Navigation.PushAsync(new CreateQuestionAnswer(quest));
            };

            var deleteAction = new MenuItem { Text = "Supprimer", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                var quest = (QuestionAnswerViewModel)mi.CommandParameter;
                int deleted = quest.DeleteQuestionById(quest.QuestionId);                
                ((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).DisplayAlert("Info", "La question a été supprimée!", "OK");
            };

            //ContextActions.Add(createAction);
            ContextActions.Add(editAction);
            ContextActions.Add(deleteAction);
        }
    }
    public class ChapterContentEdition : ContentPage
    {
        public ChapterContentEdition(ChapterViewModel _selChapterVM)
        {
            ListView lvAllChapterQuestions = new ListView();
            QuestionAnswerViewModel QuestionsVM = new QuestionAnswerViewModel(_selChapterVM);

            var lblChapter = new Label
            {
                Text = "Chapitre " + _selChapterVM.ChapterName
            };            

            List<QuestionAnswerViewModel> AllQuestionsVM = QuestionsVM.GetQuestionsForChapterVM(_selChapterVM);

            if (AllQuestionsVM == null || (AllQuestionsVM != null) && AllQuestionsVM.Count() == 0)
            {
                //QuestionAnswerViewModel test = new QuestionAnswerViewModel(_selChapterVM); ;
                //test.DeleteAllQuestions();

                //SDI: If there is no questions yet,we add a "fake" question which is a link to open the CreateQuestion page
                QuestionAnswerViewModel fakeToAdd = new QuestionAnswerViewModel(_selChapterVM);
                fakeToAdd.QuestionText = "Nouvelle question";
                AllQuestionsVM = new List<QuestionAnswerViewModel>();
                AllQuestionsVM.Add(fakeToAdd);
            }
            else
            {
                //SDI: we just add the fake chapter at the end of the list
                QuestionAnswerViewModel fakeToAdd = new QuestionAnswerViewModel(_selChapterVM);
                fakeToAdd.QuestionText = "Nouvelle question";
                AllQuestionsVM.Add(fakeToAdd);
            }

            lvAllChapterQuestions.ItemsSource = AllQuestionsVM;
            lvAllChapterQuestions.ItemTemplate =  new DataTemplate(typeof(CustomQuestionCell));
            lvAllChapterQuestions.ItemTemplate.SetBinding(ImageCell.TextProperty, "QuestionText");
            lvAllChapterQuestions.ItemTemplate.SetValue(ImageCell.TextColorProperty, Color.FromHex("#795548"));

            lvAllChapterQuestions.ItemTapped += async (sender, e) =>
            {
                QuestionAnswerViewModel qvm = (QuestionAnswerViewModel)e.Item;
                //SDI: Creation and Update are using the same create question page since the fields are bound to the object
                await Navigation.PushAsync(new CreateQuestionAnswer(qvm));

                #region Old
                //if (qvm.QuestionText.Trim().ToLower().Equals("nouvelle question"))
                //{
                //    //SDI: if element tapped is to create a new question, we go to the CreateQuestion page                   
                //    await Navigation.PushAsync(new CreateQuestionAnswer(qvm)); //await DisplayAlert("Info", "nouvelle question", "Ok");
                //}
                //else //SDI:An existing question has been selected, we edit the question
                //{
                //    await Navigation.PushAsync(new QuestionAnswerContentEdition(qvm));
                //}
                #endregion

                ((ListView)sender).SelectedItem = null;
            };


            lvAllChapterQuestions.IsPullToRefreshEnabled = true;//To enable the refreshment of the listview when pulled


            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblChapter,lvAllChapterQuestions
                },
                BackgroundColor= Color.White
            };

            this.BindingContext = AllQuestionsVM;
            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}
