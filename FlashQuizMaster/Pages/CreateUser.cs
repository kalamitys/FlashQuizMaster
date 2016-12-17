using System;
using FlashQuizMaster.ViewModels;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace FlashQuizMaster.Pages
{
    public class CreateUser: ContentPage
    {
        public CreateUser()
        {
            ConnectCreateUserViewModel UserVM = new ConnectCreateUserViewModel();

            Label lblPageTitle = new Label
            {
                Text = "Nouvel utilisateur",
                FontSize = 40,

            };

            Entry EntryLogin = new Entry()
            {
                Placeholder = "Entrez votre login",
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
                var valid =await ValidateAsync(UserVM.LoginName);
                if(valid)
                {
                    UserVM.SaveUserVM();
                    await DisplayAlert("Information", "L'utilisateur:" + UserVM.LoginName + " a été ajouté!", "OK");
                    await Navigation.PushAsync(new Connect());//SDI:We go back to the full user list
                }
             
            };

            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblPageTitle,EntryLogin,btnOk
                },
                BackgroundColor = Color.White
            };


            EntryLogin.BindingContext = UserVM;
            EntryLogin.SetBinding(Entry.TextProperty, new Binding("LoginName"));

            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;

        }

        public async Task<bool>  ValidateAsync(string loginName)
        {
            if (string.IsNullOrWhiteSpace(loginName))
            {
                await DisplayAlert("Information", "L'utilisateur: invalid", "OK");
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
