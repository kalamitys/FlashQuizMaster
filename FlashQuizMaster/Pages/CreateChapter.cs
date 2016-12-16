using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FlashQuizMaster.ViewModels;

namespace FlashQuizMaster.Pages
{
    public class CreateChapter : ContentPage
    {
        public CreateChapter(TopicViewModel curTopic)
        {
            ChapterViewModel chapterVM = new ChapterViewModel(curTopic);
            Label lblPageTitle = new Label
            {
                Text = "Nouveau chapitre",
                FontSize = 40,

            };
            Entry EntryChapter = new Entry()
            {
                Placeholder = "Entrez votre chapitre",
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
                await DisplayAlert("Information", "Le chapitre:" + chapterVM.ChapterName + " a été ajouté!", "OK");
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
