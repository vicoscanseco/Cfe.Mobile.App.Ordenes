using Cfe.Mobile.App.Ordenes.Core.Core.OS;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cfe.Mobile.App.Ordenes.UWP.OS {
    public class DB : IDB {
        public DB() {

        }
        public SQLiteConnection ObtenerConexionBaseDatos(string nombreDB) =>
            new SQLiteConnection(ObtieneRutaDeBaseDatos(nombreDB));

        public SQLiteAsyncConnection ObtenerConexionBaseDatosAsync(string nombreDB) => new SQLiteAsyncConnection(ObtieneRutaDeBaseDatos(nombreDB));


        public string ObtieneRutaDeBaseDatos(string nombreDB) =>
            Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, $"{nombreDB}.db3");
    }
}
