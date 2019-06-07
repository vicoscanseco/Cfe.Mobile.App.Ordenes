using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.DeviceInfo;
using Foundation;
using UIKit;

namespace Cfe.Mobile.App.Ordenes.iOS.OS {
    class OS : IOS {
        public string ObtieneIMEIDispositivo() {
            try {
                return $"{CrossDeviceInfo.Current.Id}";
            } catch (Exception) {

                return "IMEI fallido iOS";
            }
        }

        public string ObtieneRutaCarpetaPersonal() {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return $"{documents}";
        }

        public string ObtieneIDNotificaciones() {
            var IDFB = "IOS";

            


            return IDFB;
        }
    }
}