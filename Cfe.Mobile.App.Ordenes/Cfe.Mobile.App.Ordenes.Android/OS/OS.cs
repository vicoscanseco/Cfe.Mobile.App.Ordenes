using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.DeviceInfo;
using Cfe.Mobile.App.Ordenes.Core.Core.OS;

using Firebase.Messaging;
using Firebase.Iid;



namespace Cfe.Mobile.App.Ordenes.Droid.OS {
    public class OS : IOS {
        public string ObtieneIMEIDispositivo() {
            try {
                return $"{CrossDeviceInfo.Current.Id}";
            } catch (Exception) {

                return "IMEI fallido Android";
            }

        }

        public string ObtieneRutaCarpetaPersonal() {
            var documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return $"{documents}";
        }

        public string ObtieneIDNotificaciones() {
            var IDFB = "";

            IDFB = FirebaseInstanceId.Instance.Token;


            return IDFB;
        }


    }
}