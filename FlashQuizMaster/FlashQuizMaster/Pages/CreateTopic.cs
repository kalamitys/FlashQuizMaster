using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using FlashQuizMaster.ViewModels;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace FlashQuizMaster.Pages
{
    public class CreateTopic : ContentPage
    {
        public CreateTopic(ConnectCreateUserViewModel curUserVM)
        {
            TopicViewModel topicVM = new TopicViewModel(curUserVM);

            Label lblPageTitle = new Label
            {
                Text = "Nouveau sujet",
                FontSize = 40,

            };
            Entry EntryTopic = new Entry()
            {
                Placeholder = "Entrez votre sujet",
                HorizontalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
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

                if (await ValidateAsync(topicVM.TopicName))
                {
                    topicVM.SaveTopicVM();
                    await DisplayAlert("Information", "Le sujet:" + topicVM.TopicName + " a été ajouté!", "OK");
                    //Le modal ne marche pas bien car le retour à la page precedente ne rafraichis pas les données: A revoir
                    //await Navigation.PopModalAsync(); 
                    await Navigation.PushAsync(new TopicDisplay(curUserVM));//SDI:We go back to the full topics list
                }
            };

            

            async Task<bool> ValidateAsync(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    await DisplayAlert("Information", "Le sujet: invalid", "OK");
                    return false;
                }
                else
                {
                    return true;
                }

            }


            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblPageTitle,EntryTopic,btnOk
                },
                BackgroundColor = Color.White
            };

            EntryTopic.BindingContext = topicVM;
            EntryTopic.SetBinding(Entry.TextProperty, new Binding("TopicName"));

            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}
