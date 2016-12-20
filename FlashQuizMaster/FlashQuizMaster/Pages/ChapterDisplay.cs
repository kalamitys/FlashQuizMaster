using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FlashQuizMaster.ViewModels;
using System.Diagnostics;

namespace FlashQuizMaster.Pages
{
    public class CustomChapterCell : ImageCell
    {
        public CustomChapterCell()
        {

            var changeAction = new MenuItem { Text = "Modifier le nom" };
            changeAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            changeAction.Clicked += (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                var chap = (ChapterViewModel)mi.CommandParameter;
                ((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).Navigation.PushAsync(new ChapterEdition(chap.CurrentTopic, chap));
            };

            var editAction = new MenuItem { Text = "Editer" };
            editAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            editAction.Clicked += (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                var chap = (ChapterViewModel)mi.CommandParameter;
                //Debug.WriteLine("More clicked on row: " + chap.ChapterName.ToString());               
                ////((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).DisplayAlert("More Clicked", "On row: " + chap.ChapterName.ToString(), "OK");               
                ((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).Navigation.PushAsync(new ChapterContentEdition(chap));
            };

            var deleteAction = new MenuItem { Text = "Supprimer", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += (sender, e) =>
            {
                var mi = ((MenuItem)sender);
                var chap = (ChapterViewModel)mi.CommandParameter;
                int deleted = chap.DeleteChapterById(chap.ChapterId);
                //Debug.WriteLine("Resultat de la suppression: " + deleted.ToString());
                ((ContentPage)((StackLayout)((ListView)Parent).Parent).Parent).DisplayAlert("Info", "Le chapitre " + chap.ChapterName + " a été supprimé!","OK");
            };

            ContextActions.Add(changeAction);
            ContextActions.Add(editAction);
            ContextActions.Add(deleteAction);
        }
    }
    public class ChapterDisplay:ContentPage
    {
        public ChapterDisplay(TopicViewModel _curTopic)
        {
            ChapterViewModel curChapterVM = new ChapterViewModel(_curTopic);
            ListView lvAllChapters = new ListView();


            Label lblPageTitle = new Label
            {
                Text = "Vos Chapitres",
                FontSize = 40,

            };

            Label lblUserConnected = new Label
            {
                Text = "Bon apprentissage, " + _curTopic.CurrentUserVM.LoginName + " !",
                FontSize = 15,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#607d8b")
            };

            Label lblTopicSelected = new Label
            {
                Text =  _curTopic.TopicName,
                FontSize = 15,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#607d8b")
            };

            List<ChapterViewModel> AllChaptersVM = curChapterVM.GetChaptersForTopicVM(_curTopic);
            if (AllChaptersVM == null || (AllChaptersVM != null) && AllChaptersVM.Count() == 0)
            {
                //SDI: If there is no chapter yet,we add a "fake" chapter which is a link to open the CreateChapter page
                ChapterViewModel fakeToAdd = new ChapterViewModel(_curTopic);
                fakeToAdd.ChapterName = "Créer un chapitre";
                AllChaptersVM = new List<ChapterViewModel>();
                AllChaptersVM.Add(fakeToAdd);
            }
            else
            {
                //SDI: we just add the fake chapter at the end of the list
                ChapterViewModel fakeToAdd = new ChapterViewModel(_curTopic);
                fakeToAdd.ChapterName = "Créer un chapitre";
                AllChaptersVM.Add(fakeToAdd);
            }
            lvAllChapters.ItemsSource = AllChaptersVM;
            lvAllChapters.ItemTemplate = new DataTemplate(typeof(CustomChapterCell));//lvAllChapters.ItemTemplate = new DataTemplate(typeof(ImageCell));
            lvAllChapters.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "ChapterImage");
            lvAllChapters.ItemTemplate.SetBinding(ImageCell.TextProperty, "ChapterName");
            lvAllChapters.ItemTemplate.SetValue(ImageCell.TextColorProperty, Color.FromHex("#795548"));
            
            lvAllChapters.ItemTapped += async (sender, e) =>
            {
                ChapterViewModel cvm = (ChapterViewModel)e.Item;
                if (cvm.ChapterName.Trim().ToLower().Equals("créer un chapitre"))
                {
                    //SDI: if element tapped is to create a new chapter, we go to the CreateChapter page
                    //Le modal ne marche pas bien car le retour à la page precedente ne rafraichis pas les données: A revoir //await Navigation.PushModalAsync(new CreateTopic(_curUserVM));
                    await Navigation.PushAsync(new CreateChapter(_curTopic));
                }
                else //SDI:An existing chapter has been selected, we open the contextual menu
                {
                    //ChapterViewModel test = new ChapterViewModel(_curTopic); ;
                    //test.DeleteAllChapters();
                    await Navigation.PushAsync(new ChapterSelected(cvm));
                    //await DisplayAlert("Tapped", cvm.ChapterName.ToString() + " was selected.", "OK","Cancel");
                }

                ((ListView)sender).SelectedItem = null;
            };
           

            lvAllChapters.IsPullToRefreshEnabled = true;//To enable the refreshment of the listview when pulled

           var stackLayout = new StackLayout
            {
                Children =
                {
                    lblPageTitle,lblUserConnected,lblTopicSelected,lvAllChapters
                },
                BackgroundColor = Color.White
            };

            this.Content = stackLayout;            
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
            
        }
    }
}
