using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace FlashQuizMaster.Pages
{
   
    public class ClickBehavior
    {
        #region ClickedProperty
        public static readonly BindableProperty ClickedProperty = BindableProperty.CreateAttached<ClickBehavior, Command>(
              bindable => ClickBehavior.GetClicked(bindable),
              null, /* default value */
              BindingMode.OneWay,
              null,
              (b, o, n) => ClickBehavior.ClickedChanged(b, o, n),
              null,
              null);

        public static Command GetClicked(BindableObject bo)
        {
            return (Command)bo.GetValue(ClickBehavior.ClickedProperty);
        }

        public static void SetClicked(BindableObject bo, Command value)
        {
            bo.SetValue(ClickBehavior.ClickedProperty, value);
        }

        public static void ClickedChanged(BindableObject bo, Command oldValue, Command newValue)
        {
            var view = bo as View;

            //assure that view contains our gesture recognizer
            if (!view.GestureRecognizers.Contains(gesture))
            {
                view.GestureRecognizers.Add(gesture);
            }
        }
        #endregion 
        #region ParameterProperty
        public static readonly BindableProperty ParameterProperty = BindableProperty.CreateAttached<ClickBehavior, object>(
              bindable => ClickBehavior.GetParameter(bindable),
              null,
              BindingMode.OneWay,
              null,
              null,
              null,
              null);

        public static object GetParameter(BindableObject bo)
        {
            return (object)bo.GetValue(ClickBehavior.ParameterProperty);
        }

        public static void SetParameter(BindableObject bo, object value)
        {
            bo.SetValue(ClickBehavior.ParameterProperty, value);
        }
        #endregion
        #region IsCheckedProperty
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.CreateAttached<ClickBehavior, bool>(
              bindable => ClickBehavior.GetIsChecked(bindable),
              false, /* default value */
              BindingMode.TwoWay,
              null,
              null,
              null,
              null);

        public static bool GetIsChecked(BindableObject bo)
        {
            return (bool)bo.GetValue(ClickBehavior.IsCheckedProperty);
        }

        public static void SetIsChecked(BindableObject bo, bool value)
        {
            bo.SetValue(ClickBehavior.IsCheckedProperty, value);
        }
        #endregion

        protected static void OnTapped(object sender, EventArgs e)
        {
            BindableObject bo = sender as BindableObject;
            Command command = ClickBehavior.GetClicked(bo);
            object parameter = ClickBehavior.GetParameter(bo);

            if (command != null && command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }

            /* For CheckBox behavior*/
            //ClickBehavior.SetIsChecked(bo, !ClickBehavior.GetIsChecked(bo));
        }

        static ClickBehavior()
        {
            //initialize TapGestureRecognizer
            gesture = new TapGestureRecognizer();
            gesture.NumberOfTapsRequired = 1;
            gesture.Tapped += OnTapped;
        }

        protected static TapGestureRecognizer gesture;

    }
}
