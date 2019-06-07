using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Foundation;
using SQLite;
using UIKit;

namespace Cfe.Mobile.App.Ordenes.iOS.OS {
    public class DB : IDB {
        public SQLiteConnection ObtenerConexionBaseDatos(string nombreDB) =>
            new SQLiteConnection(ObtieneRutaDeBaseDatos(nombreDB));

        public SQLiteAsyncConnection ObtenerConexionBaseDatosAsync(string nombreDB) => new SQLiteAsyncConnection(ObtieneRutaDeBaseDatos(nombreDB));


        public string ObtieneRutaDeBaseDatos(string nombreDB) =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{nombreDB}.db3");
    }
}