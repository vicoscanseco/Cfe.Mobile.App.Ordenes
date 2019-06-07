using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using Cfe.Mobile.App.Ordenes.Core.Core.ViewModel;
using Xamarin.Essentials;
using Cfe.Mobile.App.Ordenes.Core.Core.Model;

namespace Cfe.Mobile.App.Ordenes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Datos : ContentPage {
        private readonly int id;

        public Datos(params object[] parametros) {
            InitializeComponent();
            id = (int)parametros[0];
            this.ViewModel.CargarId(id);
        }
        
       //protected async override void OnAppearing() {
       //     base.OnAppearing();
            

       //     //this.ViewModel.VerMapaCommand.Execute(null);
       //     //Orden msj = (Orden)BindingContext;
       //     //string[] p = msj.Posicion.Split(',');
       //     //double latitud = double.Parse(p[0]);
       //     //double longitud = double.Parse(p[1]);
       //     ////Position position = new Position(latitud, longitud);
       //     ////customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.0)));
       //     ////customMap.Pins.Add(new Pin { Label = msj.Rpu, Position = position  });            
         

       //     //var location = new Location(latitud, longitud);
       //     //var etiqueta = $"Medidor: {msj.Medidor} - RPU: {msj.Rpu}";
       //     //var options = new MapLaunchOptions { Name = etiqueta };//, NavigationMode = NavigationMode.Walking };

       //     //await Map.OpenAsync(location, options);

       // }

        private DatosViewModel ViewModel {
            get {
                return BindingContext as DatosViewModel;
            }
        }
    }
}