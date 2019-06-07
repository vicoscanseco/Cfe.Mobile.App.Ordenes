using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using System;
using Plugin.DeviceInfo;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Cfe.Mobile.App.Ordenes.UWP.OS {
    class OS : IOS {
        public string ObtieneIMEIDispositivo() {
            try {
                return $"{CrossDeviceInfo.Current.Id}";
            } catch (Exception) {

                return "IMEI fallido UWP";
            }
        }

        public string ObtieneRutaCarpetaPersonal() {
            var localFolder = ApplicationData.Current.LocalFolder;
            return $"{localFolder.Path}";
        }
        public string ObtieneIDNotificaciones() {
            var IDFB = "UWP 12345";

            //IDFB = FirebaseInstanceId.Instance.Token;


            return IDFB;
        }
    }
}
