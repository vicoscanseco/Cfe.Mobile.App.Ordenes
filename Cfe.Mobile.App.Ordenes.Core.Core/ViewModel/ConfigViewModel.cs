using Cfe.Mobile.App.Ordenes.Core.Core.Data;
using Cfe.Mobile.App.Ordenes.Core.Core.Model;
using Cfe.Mobile.App.Ordenes.Core.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Cfe.Mobile.App.Ordenes.Core.Core.ViewModel {
    class ConfigViewModel : ViewModelBase {

        MensajesDataContext dtc = new MensajesDataContext();
        MensajesAPI api = new MensajesAPI();
        DatabaseAPI dbapi = new DatabaseAPI();

        private string idNotif;
        public string IDNotif { get => idNotif; set => Set(ref idNotif, value); }

        private string rpe;
        public string RPE { get => rpe; set {
                Set(ref rpe, value);
                RegistrarEquipoCommand.RaiseCanExecuteChanged();
            } }
        private string nombre;
        public string Nombre { get => nombre; set => Set(ref nombre, value); }

        public ConfigViewModel() {
            //Se cargan los datos en las tablas de Division
            //dtc.Delete<Division>()
            rpe = "";
            nombre = "";

            cargarDB();
            //try {
            GetDatosUsuario();
            //} catch(NullReferenceException e) {
            //    Debug.WriteLine(e.ToString());
            //    RPE = "";               
            //}

            
            var os = OS.DependencyService.Get<OS.IOS>();
            IDNotif = os.ObtieneIDNotificaciones();

        }

        private ObservableCollection<Division> divisiones;
        public ObservableCollection<Division> Divisiones { get => divisiones; set => Set(ref divisiones, value); }

        private ObservableCollection<Zona> zonas;
        public ObservableCollection<Zona> Zonas { get => zonas; set => Set(ref zonas, value); }

        private ObservableCollection<Area> areas;
        public ObservableCollection<Area> Areas { get => areas; set => Set(ref areas, value); }


        public async void GetDatosUsuario() {
            try {
                var users = await dtc.GetAllAsync<Usuario>();
                if (users.Count != 0) {
                    //Se leen los datos de la DB
                    var user = users.Select(x => x).FirstOrDefault();
                    RPE = user.Rpe;
                    IDNotif = user.IdEquipo;
                    Nombre = user.Nombre;

                }
            } catch { }
        }

        public async void cargarDB() {
            try {
                int sidivis = 0;
                try {
                    var div = await dtc.GetAllAsync<Division>();
                    sidivis = div.Count();
                } catch { }
                if (sidivis == 0) {
                    //Si es 0 se bajan los datos de la red
                    var divs = await dbapi.ObtenerDivisiones();
                    var zonas = await dbapi.ObtenerZonas();
                    var areas = await dbapi.ObtenerAreas();
                    foreach (var d in divs) {
                        await dtc.InsertAsync<Division>(d);
                        Divisiones.Add(d);
                    }
                    foreach (var z in zonas) {
                        await dtc.InsertAsync<Zona>(z);
                        Zonas.Add(z);
                    }
                    foreach (var a in areas) {
                        await dtc.InsertAsync<Area>(a);
                        Areas.Add(a);
                    }
                    await LlenaColecciones();
                } else {
                    await LlenaColecciones();
                }
            } catch(Exception e) {
                Debug.WriteLine(e.ToString());
            }
        }

        private async Task LlenaColecciones() {
            var div = await dtc.GetAllAsync<Division>();
            Divisiones = new ObservableCollection<Division>(div);
            var zs = await dtc.GetAllAsync<Zona>();
            Zonas = new ObservableCollection<Zona>(zs);
            var ar = await dtc.GetAllAsync<Area>();
            Areas = new ObservableCollection<Area>(ar);
        }

        RelayCommand<string> navegarACommand = null;
        public RelayCommand<string> NavegarACommand {
            get => navegarACommand ?? (navegarACommand = new RelayCommand<string>((pageKey) => {
                var nav = OS.DependencyService.Get<OS.INavigationService>();
                nav.GoBack();

            }, (s) => { return true; }));
        }

        RelayCommand registrarEquipoCommand = null;
        public RelayCommand RegistrarEquipoCommand {
            get => registrarEquipoCommand ?? (registrarEquipoCommand = new RelayCommand(async () => {
                //aca se hace la llamada al WS
                try {
                    var users = await dtc.GetAllAsync<Usuario>();
                    
                    var usr = users.Select(x => x).FirstOrDefault();
                    usr.Nombre = Nombre;
                    usr.Rpe = RPE;
                    usr.IdEquipo = IDNotif;
                    await dtc.UpdateAsync<Usuario>(usr);
                    await api.Registrar(RPE, IDNotif);
                    navegarACommand.Execute("Home");

                } catch (Exception e) {
                    //await page.DisplayAlert("Alert from View Model", "", "Ok");
                }

            }, () => { return true; }));
        }


    }
}
