
using Cfe.Mobile.App.Ordenes.Core.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Cfe.Mobile.App.Ordenes.UC {
    public class CustomMap : Map {
        public static readonly BindableProperty CustomPinsProperty = BindableProperty.Create("CustomPins", typeof(List<CustomPin>), typeof(CustomMap), defaultValue: new List<CustomPin>(), defaultBindingMode: BindingMode.TwoWay);
        public List<CustomPin> CustomPins {
            get { return (List<CustomPin>)GetValue(CustomPinsProperty); }
            set { SetValue(CustomPinsProperty, value); }
        }
    }
}
