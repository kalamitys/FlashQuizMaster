using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashQuizMaster.ViewModels;
using Xamarin.Forms;

namespace FlashQuizMaster.Pages
{
    class NegateConverter : IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

       
        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }

    class ClickBehaviorTestViewModel //:ViewModel
    {
        public Command Clicked { get; set; }

        protected bool isChecked = false;
        public bool IsChecked
        {
            get { return isChecked; }
            set { value = !isChecked; }//set { this.ChangeAndNotify(ref isChecked, value); }
        }


        public ClickBehaviorTestViewModel()
        {
            this.Clicked = new Command(p => OnClicked(p));
        }

        public void OnClicked(object p)
        {
        }
    }
    public partial class Test : ContentPage
    {
        public Test()
        {
            new ClickBehavior();
            InitializeComponent();
        }
        
    }
}
