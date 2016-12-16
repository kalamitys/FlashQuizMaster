using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizMaster
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            #region Initial sample code
            //var label = new Label { Text = "Database Created Using SQLite.NET\n" };

            //label.Text += " Using an Advanced Repository\n\n";

            //App.Repository.DeleteAllItems(); // clear out the table to start fresh

            //var item = new Item { Name = "First", Description = "This is the first item" };
            //App.Repository.SaveItem(item);

            //var firstItem = App.Repository.GetFirstItems();
            //label.Text += firstItem.First().Name + " item added.\n";

            //var id = 1;
            //item = App.Repository.GetItem(id);
            //label.Text += item.Name + " item at ID " + id.ToString() + "\n\n";

            //App.Repository.DeleteItem(id);
            //label.Text += "Deleted item at ID " + id.ToString() + "\n\n";

            //item = new Item { Name = "First", Description = "This is the first item" };
            //App.Repository.SaveItem(item);
            //item = new Item { Name = "Second", Description = "This is the second item" };
            //App.Repository.SaveItem(item);
            //item = new Item { Name = "Third", Description = "This is the furd item" };
            //App.Repository.SaveItem(item);

            //var items = App.Repository.GetItems();
            //foreach (var i in items)
            //{
            //    label.Text += i.Name + ": " + i.Description + "\n";
            //}

            //label.Text += "\n Oops, I meant: ";

            //item.Description = "This is the third item";
            //App.Repository.SaveItem(item);

            //id = 4;
            //item = App.Repository.GetItem(id);
            //label.Text += item.Name + ": " + item.Description + "\n";

            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            //Content = new StackLayout
            //{
            //    Children = {
            //        label
            //    }
            //};
            #endregion

            Label lblAppName = new Label
            {
                Text = "FlashQuizMaster",
                FontSize = 40,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            Label lblWelcome = new Label
            {
                Text = "Bienvenue sur l'application \n" +
                        "qui vous  (re)donnera \n" +
                        "le plaisir de reviser.",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

           
            Image homeImg = new Image
            {
                Source = "grad.jpg", 
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill
            };

            TapGestureRecognizer homeImgTapGest = new TapGestureRecognizer();
            homeImgTapGest.Tapped += async(s,e)=> 
            {
                homeImg.Opacity = .5;
                await Task.Delay(100);
                homeImg.Opacity = 1;
                await Navigation.PushAsync(new Pages.Connect());                
            };
            homeImg.GestureRecognizers.Add(homeImgTapGest);

            //SDI:Autre moyen de faire la meme chose
            //homeImg.GestureRecognizers.Add(new TapGestureRecognizer {
            //    Command = new Command(() => { Opacity = .5; })
            //});

            Content = new StackLayout
            {
                Children =
                {
                    lblAppName,
                    lblWelcome,
                    homeImg
                },
               
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
                Spacing = 50,
            };

            //SDI: is blocking the tapped event in iOS
            //ScrollView scrollViewHome = new ScrollView
            //{
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    BackgroundColor = Color.White,
            //    Content = Content
            //};

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            this.BackgroundColor = Color.Gray;
            //this.Content = scrollViewHome;//SDI:prevents the tapped event from working in iOS

        }       
    }
}