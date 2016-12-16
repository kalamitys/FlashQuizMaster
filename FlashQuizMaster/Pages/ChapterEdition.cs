using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.ViewModels;

namespace FlashQuizMaster.Pages
{
    public class ChapterEdition : ContentPage
    {
        public ChapterEdition(TopicViewModel curTopic, ChapterViewModel curChapter)
        {
            Chapter oldChapter = new Chapter();
            oldChapter.ID = curChapter.ChapterId;
            oldChapter.Name = curChapter.ChapterName;
            oldChapter.TopicId = curChapter.ChapterTopicId;
            
            ChapterViewModel chapterVM = new ChapterViewModel(curTopic,oldChapter);

            Label lblPageTitle = new Label
            {
                Text = "Modifier le chapitre",
                FontSize = 40,

            };
            Entry EntryChapter = new Entry()
            {               
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                WidthRequest = 400
            };
            EntryChapter.BindingContext = chapterVM;
            EntryChapter.SetBinding(Entry.TextProperty, new Binding("ChapterName"));

            Button btnOk = new Button()
            {
                Text = "Valider",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Silver
            };

            btnOk.Clicked += async (sender, args) =>
            {
                chapterVM.SaveChapterVM();
                await DisplayAlert("Information", "Le chapitre:" + chapterVM.ChapterName + " a été modifié!", "OK");
                //Le modal ne marche pas bien car le retour à la page precedente ne rafraichis pas les données: A revoir
                //await Navigation.PopModalAsync(); 
                await Navigation.PushAsync(new ChapterDisplay(curTopic));//SDI:We go back to the full chapter list
            };

            Button btnCancel = new Button()
            {
                Text = "Annuler",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Silver
            };

            btnCancel.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new ChapterDisplay(curTopic));//SDI:We go back to the full chapter list
            };

            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblPageTitle,EntryChapter, btnCancel,btnOk
                },
                BackgroundColor = Color.White
            };

            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    
    }
}
