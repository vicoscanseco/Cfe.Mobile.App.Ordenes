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
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace Cfe.Mobile.App.Ordenes.Core.Core.ViewModel {
    class MainViewModel : ViewModelBase {

       // OS.IOS OS { get { return DependencyService.Get<OS.IOS>(); } }

        private string rpe;

        public string RPE { get => rpe; set => Set(ref rpe, value); }

        private string nombre;
        public string Nombre { get => nombre; set => Set(ref nombre, value); }

        MensajesAPI api = new MensajesAPI();

        MensajesDataContext dtc = new MensajesDataContext();

        public MainViewModel() {
            var os = OS.DependencyService.Get<OS.IOS>();
            IDNotif = os.ObtieneIDNotificaciones();


            dtc.ActualizaTablas(IDNotif);
            Cargar();
            //rpe = "9L0BD";
            GetDatosUsuario();


        }

        private async void GetDatosUsuario() {
            try {
                var users = await dtc.GetAllAsync<Usuario>();
                if (users.Count != 0) {
                    //Se leen los datos de la DB
                    var user = users.Select(x => x).FirstOrDefault();
                    RPE = user.Rpe;
                    IDNotif = user.IdEquipo;
                    Nombre = user.Nombre;

                } else {
                    RPE = "NINGUNO";
                    Nombre = "DESCONOCIDO";
                }
            }catch(Exception e) {
                Debug.WriteLine(e.Message);
            }

        }


      


        private string idNotif;
        public string IDNotif { get => idNotif; set => Set(ref idNotif, value); }


        private int idSeleccionada;
        public int IdSeleccionada { get => idSeleccionada; set => Set(ref idSeleccionada, value); }

        private Mensaje mensajeResponse;

        public Mensaje Seleccionada {
            get => mensajeResponse;
            set {
                Set(ref mensajeResponse, value);
                CargarDetalle();     
                

            }
        }

        private async void CargarDetalle() {
            if (Seleccionada == null) return;

            var nav = OS.DependencyService.Get<OS.INavigationService>();
            IdSeleccionada = mensajeResponse.Id;
            await nav.NavigateTo("Datos", idSeleccionada);
            

            RaisePropertyChanged(() => Seleccionada);

        }

        public async void CargarId(int id) {
            Cargar();
            Seleccionada =
                Mensajes.Where(x => x.Id == id).FirstOrDefault();
        }
        private async void Cargar() {           
            var msjs = await dtc.GetAllAsync<Mensaje>();
            Mensajes = new ObservableCollection<Mensaje>(msjs);
        }
        private ObservableCollection<Mensaje> mensajes;
        public ObservableCollection<Mensaje> Mensajes { get => mensajes; set => Set(ref mensajes, value); }        


        RelayCommand<string> navegarACommand = null;
        public RelayCommand<string> NavegarACommand {
            get => navegarACommand ?? (navegarACommand = new RelayCommand<string>((pageKey) => {
                var nav = OS.DependencyService.Get<OS.INavigationService>();
                if (pageKey == OS.NavigationService.PageKeys.Inicio)
                    nav.Home();
                else
                    nav.NavigateTo(pageKey);
            }, (s) => { return true; }));
        }


        RelayCommand cargarDatosCommand = null;
        public RelayCommand CargarDatosCommand {
            get => cargarDatosCommand ?? (cargarDatosCommand = new RelayCommand(async () => {
                //aca se hace la llamada al WS
                try {
                    var msj = await api.Obtener(rpe);
                    if (msj != null) {
                        foreach (var m in Mensajes) {
                            await dtc.DeleteAsync<Mensaje>(m);
                        }
                        foreach (var item in msj) {
                            await dtc.InsertAsync<Mensaje>(item);
                            Mensajes.Add(item);
                        }
                        Cargar();
                    }
                } catch (Exception e) {
                    //await page.DisplayAlert("Alert from View Model", "", "Ok");
                    Debug.WriteLine(e.ToString());
                }

            }, () => { return true; }));
        }

        RelayCommand registrarEquipoCommand = null;
        public RelayCommand RegistrarEquipoCommand {
            get => registrarEquipoCommand ?? (registrarEquipoCommand = new RelayCommand(async () => {
                //aca se hace la llamada al WS
                try {
                    await api.Registrar(RPE, IDNotif);
                    
                } catch (Exception e) {
                    //await page.DisplayAlert("Alert from View Model", "", "Ok");
                }

            }, () => { return true; }));
        }


        RelayCommand verMapaCommand = null;
        public RelayCommand VerMapaCommand {
            get => verMapaCommand ?? (verMapaCommand = new RelayCommand(async () => {
                var msj = await AbrirMapa();
            }, () => { return true; }));
        }

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

        //public MainViewModel() {
        //    //Boton1 = "pluscircle.png";
        //    //Boton2 = "cogs.png";
        //    //Boton3 = "save.png"; //Uno es para salvar la notificacion
        //    //Boton4 = "save.png"; //Este es para salvar la config.
        //    //BackBoton = "arrowleft.png";
        //}

    }
}
