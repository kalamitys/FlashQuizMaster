using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.ViewModels;

namespace FlashQuizMaster.Pages
{
    public class CreateQuestionAnswer : ContentPage
    {
        public CreateQuestionAnswer(QuestionAnswerViewModel questVM)
        {

            var lblChapter = new Label
            {
                Text = "Questions du chapitre " + questVM.QuestionChapterName
            };
            var lblQuestion = new Label
            {
                Text = "Question"
            };
            var entryQuestion = new Entry
            {
                Placeholder = "Entrez votre question",
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                WidthRequest = 400
            };
            entryQuestion.BindingContext = questVM;
            entryQuestion.SetBinding(Entry.TextProperty, new Binding("QuestionText"));

            var lblCorrectAnswer = new Label
            {
                Text = "Réponse correcte"
            };
            var entryCorrectAnswer = new Entry
            {                
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                WidthRequest = 400,
                HeightRequest= 100               
            };
            entryCorrectAnswer.BindingContext = questVM;
            entryCorrectAnswer.SetBinding(Entry.TextProperty, new Binding("CorrectAnswerText"));

            var lblFakeAnwser1 = new Label
            {
                Text = "Reponse Incorrecte N°1"
            };
            var entryFakeAnswer1 = new Entry
            {                
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                WidthRequest = 400,
                HeightRequest = 100
            };
            entryFakeAnswer1.BindingContext = questVM;
            entryFakeAnswer1.SetBinding(Entry.TextProperty, new Binding("FakeAnswerText1"));

            var lblFakeAnwser2 = new Label
            {
                Text = "Reponse Incorrecte N°2"
            };
            var entryFakeAnswer2 = new Entry
            {
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                WidthRequest = 400,
                HeightRequest = 100
            };
            entryFakeAnswer2.BindingContext = questVM;
            entryFakeAnswer2.SetBinding(Entry.TextProperty, new Binding("FakeAnswerText2"));

            var lblFakeAnwser3 = new Label
            {
                Text = "Reponse Incorrecte N°3"
            };
            var entryFakeAnswer3 = new Entry
            {
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                WidthRequest = 400,
                HeightRequest = 100
            };
            entryFakeAnswer3.BindingContext = questVM;
            entryFakeAnswer3.SetBinding(Entry.TextProperty, new Binding("FakeAnswerText3"));

            Button btnOk = new Button()
            {
                Text = "Valider",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Silver
            };

            btnOk.Clicked += async (sender, args) =>
            {
                if (string.IsNullOrEmpty(entryCorrectAnswer.Text.Trim()) || string.IsNullOrEmpty(entryFakeAnswer1.Text.Trim()))
                {
                    await DisplayAlert("Information", "Vous devez renseigner une réponse correcte et au moins une réponse incorrecte!", "OK");
                }
                else
                {
                    questVM.SaveQuestionVM();
                    await DisplayAlert("Information", "Votre question a été ajoutée/modifiée!", "OK");
                    //Le modal ne marche pas bien car le retour à la page precedente ne rafraichis pas les données: A revoir
                    await Navigation.PopAsync();
                }
            };

            Button btnCancel = new Button()
            {
                Text = "Annuler",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Silver
            };

            btnCancel.Clicked += async (sender, args) =>
            {
                await DisplayAlert("Info","question annulée","Annuler");//await Navigation.PushAsync(new ChapterDisplay(curTopic));//SDI:We go back to the full chapter list
            };
            var stackLayout = new StackLayout
            {
                Children =
                {
                     btnOk, btnCancel,
                    lblChapter,
                    lblQuestion,entryQuestion,
                    lblCorrectAnswer, entryCorrectAnswer,
                    lblFakeAnwser1, entryFakeAnswer1,
                    lblFakeAnwser2, entryFakeAnswer2,
                    lblFakeAnwser3, entryFakeAnswer3,
                   
                },
                BackgroundColor = Color.White
            };

            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}
