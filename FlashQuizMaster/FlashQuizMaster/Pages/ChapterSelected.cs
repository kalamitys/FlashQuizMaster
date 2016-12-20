using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FlashQuizMaster.ViewModels;


namespace FlashQuizMaster.Pages
{
    public class ChapterSelected:ContentPage
    {
        public ChapterSelected(ChapterViewModel _curChapter)
        {
            
            Label lblUserConnected = new Label
            {
                Text = "Bon apprentissage, " +  _curChapter.CurrentTopic.CurrentUserVM.LoginName +  " !",
                FontSize = 15,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#607d8b")
            };

            Label lblTopicSelected = new Label
            {
                Text = _curChapter.CurrentTopic.TopicName,
                FontSize = 15,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#607d8b")
            };

            Label lblChapterSelected = new Label
            {
                Text = _curChapter.ChapterName,
                FontSize = 15,
                FontAttributes = FontAttributes.Italic,
                TextColor = Color.FromHex("#607d8b")
            };

            #region Image Apprendre
            Image imgLearn = new Image
            {
                Source = "learn.png",
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill
            };

            TapGestureRecognizer imgLearnTapGest = new TapGestureRecognizer();
            imgLearnTapGest.Tapped += async (s, e) =>
            {
                String test = await DisplayActionSheet("Test action sheet on learn", "Annuler", "destruction", "Other choices");
                //await Navigation.PushAsync(new Pages.Connect());
            };
            imgLearn.GestureRecognizers.Add(imgLearnTapGest);

            #endregion

            #region Image Lancer le test

            Image imgRunTest = new Image
            {
                Source = "runtest.png",
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill
            };

            TapGestureRecognizer imgRunTestTapGest = new TapGestureRecognizer();
            imgRunTestTapGest.Tapped += async (s, e) =>
            {
                //String test = await DisplayActionSheet("Test action sheet on test", "Annuler", "destruction", "Other choices");
                await Navigation.PushAsync(new Pages.TestQuestionCheck(_curChapter));
            };
            imgRunTest.GestureRecognizers.Add(imgRunTestTapGest);

            #endregion

            #region Image Lire la fiche

            Image imgRevisionCard = new Image
            {
                Source = "revisioncard.png",
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill
            };

            TapGestureRecognizer imgRevisionCardTapGest = new TapGestureRecognizer();
            imgRevisionCardTapGest.Tapped += async (s, e) =>
            {
                String test = await DisplayActionSheet("Test action sheet on revision", "Annuler", "destruction", "Other choices");
                //await Navigation.PushAsync(new Pages.Connect());
            };
            imgRevisionCard.GestureRecognizers.Add(imgRevisionCardTapGest);
            #endregion


            var stackLayout = new StackLayout
            {
                Children =
                {
                    lblUserConnected,lblTopicSelected,lblChapterSelected,
                    imgLearn,imgRunTest,imgRevisionCard
                },
                BackgroundColor = Color.White
            };
            this.Content = stackLayout;
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
        }
    }
}
