using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using SQLite;

using System.Diagnostics;

namespace Cfe.Mobile.App.Ordenes.Droid.OS {
    public class DB : IDB {
        public SQLiteConnection ObtenerConexionBaseDatos(string nombreDB) =>
            new SQLiteConnection(ObtieneRutaDeBaseDatos(nombreDB));

        public SQLiteAsyncConnection ObtenerConexionBaseDatosAsync(string nombreDB) {
            System.Diagnostics.Debug.WriteLine(ObtieneRutaDeBaseDatos(nombreDB));
            return new SQLiteAsyncConnection(ObtieneRutaDeBaseDatos(nombreDB));
        }


        public string ObtieneRutaDeBaseDatos(string nombreDB) =>
            Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), $"{nombreDB}.db3");
    }
}