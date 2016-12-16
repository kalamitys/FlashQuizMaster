using FlashQuizMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashQuizMaster.Pages
{
    public class TopicDisplay : ContentPage
    {
        public TopicDisplay(ConnectCreateUserViewModel _curUserVM)
        {
            TopicViewModel curTopicVM = new TopicViewModel(_curUserVM);
            ListView lvAllTopics = new ListView();

            Label lblPageTitle = new Label
            {
                Text = "Vos matières",
                FontSize = 40,

            };

            Label lblUserConnected = new Label
            {
                Text = "Bon apprentissage, " + _curUserVM.LoginName + " !",
                FontSize = 15,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#607d8b")
            };


            List<TopicViewModel> AllTopicsVM = curTopicVM.GetTopicsForUserVM(_curUserVM);
            if (AllTopicsVM == null || (AllTopicsVM != null) && AllTopicsVM.Count() == 0)
            {
                //SDI: If there is no topic yet,we add a "fake" topic which is a link to open the CreateTopic page
                TopicViewModel fakeToAdd = new TopicViewModel(_curUserVM);
                fakeToAdd.TopicName = "Créer un sujet";
                AllTopicsVM = new List<TopicViewModel>();
                AllTopicsVM.Add(fakeToAdd);
            }
            else
            {
                //SDI: we just add the fake topic at the end of the list
                TopicViewModel fakeToAdd = new TopicViewModel(_curUserVM);
                fakeToAdd.TopicName = "Créer un sujet";
                AllTopicsVM.Add(fakeToAdd);
            }
            lvAllTopics.ItemsSource = AllTopicsVM;
            lvAllTopics.ItemTemplate = new DataTemplate(typeof(ImageCell));
            lvAllTopics.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "TopicImage");
            lvAllTopics.ItemTemplate.SetBinding(ImageCell.TextProperty, "TopicName");
            lvAllTopics.ItemTemplate.SetValue(ImageCell.TextColorProperty, Color.FromHex("#795548"));

            lvAllTopics.ItemTapped += async (sender, e) =>
            {
                TopicViewModel tvm = (TopicViewModel)e.Item;
                if (tvm.TopicName.Trim().ToLower().Equals("créer un sujet"))
                {
                    //SDI: if element tapped is to create a new topic, we go to the CreateTopic page
                    //Le modal ne marche pas bien car le retour à la page precedente ne rafraichis pas les données: A revoir //await Navigation.PushModalAsync(new CreateTopic(_curUserVM));
                    await Navigation.PushAsync(new CreateTopic(_curUserVM));
                }
                else //SDI:This is an existing Topic, we open the Chapter page
                {
                    await Navigation.PushAsync(new ChapterDisplay(tvm));
                    //await DisplayAlert("Tapped", tvm.TopicName.ToString() + " was selected.", "OK","Cancel");
                }
                ((ListView)sender).SelectedItem = null;
            };

            lvAllTopics.IsPullToRefreshEnabled = true;//To enable the refreshment of the listview when pulled

            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblPageTitle,lblUserConnected,lvAllTopics
                },
                BackgroundColor = Color.White
            };

            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}