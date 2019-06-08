using Cfe.Mobile.App.Ordenes.Core.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cfe.Mobile.App.Ordenes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage {
        public Configuracion() {
            InitializeComponent();
            
        }
        public Configuracion(params object[] parametros) {
            InitializeComponent();
        }

        //protected async override void OnAppearing() {
        //    base.OnAppearing();

        //    //ViewModel.cargarDB();
        //    //ViewModel.GetDatosUsuario();
        //}


        private ConfigViewModel ViewModel {
            get {
                return BindingContext as ConfigViewModel;
            }
        }

    }
}