using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cfe.Mobile.App.Ordenes.Core.Core.OS {
    public interface INavigationService {

        Task GoBack();
        Task Home();
        Task PopModal();
        Task PushModal(string pageKey, params object[] parameter);
        Task NavigateTo(string pageKey);
        Task NavigateTo(string pageKey, params object[] parameter);
    }
    public class NavigationService {
        public class PageKeys {
            public const string Inicio = "Inicio";
            public const string Datos = "Datos";
            public const string Config = "Configuración";
        }
    }

}
