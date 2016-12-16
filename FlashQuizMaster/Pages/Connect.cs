using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FlashQuizMaster.ViewModels;

namespace FlashQuizMaster.Pages
{
    public class Connect : ContentPage
    {
        public Connect()
        {
            //this.Title = "Hierarchical Navigation";//SDI:
            ConnectCreateUserViewModel UserVM = new ConnectCreateUserViewModel();
            ListView lvAllUsers = new ListView();

            Label lblPageTitle = new Label
            {
                Text = "Utilisateurs",
                FontSize = 40,

            };

            #region Moved to Createuser page
            //Entry EntryLogin = new Entry()
            //{
            //    Placeholder = "Entrez votre login",
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    Keyboard = Keyboard.Text,
            //    WidthRequest =100
            //};

            //Button btnOk = new Button()
            //{
            //    Text = "Valider",
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    BackgroundColor = Color.Silver
            //};

            //btnOk.Clicked += async (sender, args) =>
            //{
            //    UserVM.SaveUserVM();
            //    List<UserViewModel> AllUsersVM = UserVM.GetUsersVM();
            //    lvAllUsers.ItemsSource = AllUsersVM;
            //    lvAllUsers.ItemTemplate = new DataTemplate(typeof(TextCell));
            //    lvAllUsers.ItemTemplate.SetBinding(TextCell.TextProperty, "LoginName");
            //    lvAllUsers.ItemTemplate.SetValue(TextCell.TextColorProperty, Color.FromHex("#795548"));
            //    await DisplayAlert("Information", "new user:" + UserVM.LoginName + "added!", "OK");
            //};
            #endregion

            List<ConnectCreateUserViewModel> AllUsersVM = UserVM.GetUsersVM();
            if (AllUsersVM == null || (AllUsersVM != null) && AllUsersVM.Count() == 0)
            {
                //SDI: If there is no user yet,we add a "fake" user which is a link to open the CreateUser page
                ConnectCreateUserViewModel fakeToAdd = new ConnectCreateUserViewModel();
                fakeToAdd.LoginName = "Créer un utilisateur";
                AllUsersVM = new List<ConnectCreateUserViewModel>();
                AllUsersVM.Add(fakeToAdd);
            }
            else
            {
                //SDI: we just add thefake user at the end of the list
                ConnectCreateUserViewModel fakeToAdd = new ConnectCreateUserViewModel();
                fakeToAdd.LoginName = "Créer un utilisateur";                
                AllUsersVM.Add(fakeToAdd);
            }
            lvAllUsers.ItemsSource = AllUsersVM;
            lvAllUsers.ItemTemplate = new DataTemplate(typeof(ImageCell));
            lvAllUsers.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "LoginImage");
            lvAllUsers.ItemTemplate.SetBinding(ImageCell.TextProperty, "LoginName");            
            lvAllUsers.ItemTemplate.SetValue(ImageCell.TextColorProperty, Color.FromHex("#795548"));
            

            lvAllUsers.ItemTapped += async (sender, e) =>
            {
                ConnectCreateUserViewModel uvm = (ConnectCreateUserViewModel)e.Item;
                if (uvm.LoginName.Trim().ToLower().Equals("créer un utilisateur"))
                {
                    //SDI: if element tapped is to create a new user, we go to the CreateUser page
                    await Navigation.PushAsync(new CreateUser());
                }
                else //SDI:This is a real user, we open the start page
                {
                    //TopicViewModel test = new TopicViewModel(uvm); ;
                    //test.DeleteAllTopics();
                    await Navigation.PushAsync(new TopicDisplay(uvm));
                }                              
                ((ListView)sender).SelectedItem = null;
            };

            lvAllUsers.IsPullToRefreshEnabled = true;//To enable the refreshment of the listview when pulled

            var stackLayout = new StackLayout
            {
                Children =
                {                   
                    lblPageTitle,lvAllUsers
                },
                BackgroundColor = Color.White
            };

            
            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}
