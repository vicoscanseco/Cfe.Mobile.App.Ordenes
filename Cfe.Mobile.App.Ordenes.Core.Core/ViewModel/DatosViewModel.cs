using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Cfe.Mobile.App.Ordenes.Core.Core.Data;
using Cfe.Mobile.App.Ordenes.Core.Core.Model;
using Cfe.Mobile.App.Ordenes.Core.Core.Services;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;

namespace Cfe.Mobile.App.Ordenes.Core.Core.ViewModel {
    class DatosViewModel : ViewModelBase {
        public DatosViewModel() {

        }

        MensajesDataContext dtc = new MensajesDataContext();

        private Mensaje mensajeResponse;
        public Mensaje Seleccionada {
            get => mensajeResponse;
            set {
                Set(ref mensajeResponse, value);
            }
        }

        RelayCommand verMapaCommand = null;
        public RelayCommand VerMapaCommand {
            get => verMapaCommand ?? (verMapaCommand = new RelayCommand(async () => {
                var msj = await AbrirMapa();
            }, () => { return true; }));
        }

        RelayCommand rutaCommand = null;
        public RelayCommand RutaCommand {
            get => rutaCommand ?? (rutaCommand = new RelayCommand(async () => {
                var msj = await AbrirRuta();
            }, () => { return true; }));
        }

        public async void CargarId(int id) {
           // Cargar();
            var msjs = await dtc.GetAllAsync<Mensaje>();
            Seleccionada =
                 msjs.Where(x => x.Id == id).FirstOrDefault();
        }
        private async void Cargar() {
            var msjs = await dtc.GetAllAsync<Mensaje>();
            Mensajes = new ObservableCollection<Mensaje>(msjs);
        }
        private ObservableCollection<Mensaje> mensajes;
        public ObservableCollection<Mensaje> Mensajes { get => mensajes; set => Set(ref mensajes, value); }        
        private async Task<string> AbrirMapa() {
            
            string[] p = Seleccionada.Posicion.Split(',');
            double latitud = double.Parse(p[0]);
            double longitud = double.Parse(p[1]);


            var location = new Location(latitud, longitud);
            var etiqueta = $"Medidor: {Seleccionada.Medidor} - RPU: {Seleccionada.Rpu}";
            var options = new MapLaunchOptions { Name = etiqueta };//, NavigationMode = NavigationMode.Walking };

            await Map.OpenAsync(location, options);
            return "";
        }

        private async Task<string> AbrirRuta() {

            string[] p = Seleccionada.Posicion.Split(',');
            double latitud = double.Parse(p[0]);
            double longitud = double.Parse(p[1]);


            var location = new Location(latitud, longitud);
            var etiqueta = $"Medidor: {Seleccionada.Medidor} - RPU: {Seleccionada.Rpu}";
            var options = new MapLaunchOptions { Name = etiqueta, NavigationMode = NavigationMode.Walking };

            await Map.OpenAsync(location, options);
            return "";
        }

        RelayCommand<string> navegarACommand = null;
        public RelayCommand<string> NavegarACommand {
            get => navegarACommand ?? (navegarACommand = new RelayCommand<string>((pageKey) => {
                var nav = OS.DependencyService.Get<OS.INavigationService>();
                nav.GoBack();
                
            }, (s) => { return true; }));
        }

    }
}
