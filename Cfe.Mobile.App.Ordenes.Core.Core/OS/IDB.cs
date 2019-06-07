using System;
using System.Collections.Generic;
using System.Text;

namespace Cfe.Mobile.App.Ordenes.Core.Core.OS {
    public partial interface IDB {

        string ObtieneRutaDeBaseDatos(string nombreDB);

        SQLite.SQLiteConnection ObtenerConexionBaseDatos(string nombreDB);

        SQLite.SQLiteAsyncConnection ObtenerConexionBaseDatosAsync(string nombreDB);
    }
}
