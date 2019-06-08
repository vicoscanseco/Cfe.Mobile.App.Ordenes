using Cfe.Mobile.App.Ordenes.Core.Core.Model;
using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Cfe.Mobile.App.Ordenes.Core.Core.OS.NavigationService;


namespace Cfe.Mobile.App.Ordenes.OS
{
    class NavigationService : INavigationService
    {
        internal static Xamarin.Forms.INavigation Navigation { get; set; }

        public Task GoBack() => Navigation.PopAsync(true);

        public Task Home() => Navigation.PopToRootAsync(true);


        public Task NavigateTo(string pageKey)
        {

            switch (pageKey)
            {
                case PageKeys.Inicio:
                    return Navigation.PopToRootAsync();
                case PageKeys.Datos:
                    return Navigation.PushAsync(new Views.Datos(), true);
                case PageKeys.Config:
                    return Navigation.PushAsync(new Views.Configuracion(), true);


            }

            return null;
        }

        public Task NavigateTo(string pageKey, params object[] parameter)
        {
            switch (pageKey) {
                case PageKeys.Inicio:
                    return Navigation.PopToRootAsync();
                case PageKeys.Datos:
                    var datos = new Views.Datos(parameter);// (new Orden((Mensaje)parameter[0]));                                        
                    return Navigation.PushAsync(datos, true);
                case PageKeys.Config:
                    var config = new Views.Configuracion();// (new Orden((Mensaje)parameter[0]));                                        
                    return Navigation.PushAsync(config, true);
            }

            return null;
        }

        public async Task PopModal() => await Navigation.PopModalAsync();


        public Task PushModal(string pageKey, params object[] parameter)
        {
            return null;
        }
    }
}
