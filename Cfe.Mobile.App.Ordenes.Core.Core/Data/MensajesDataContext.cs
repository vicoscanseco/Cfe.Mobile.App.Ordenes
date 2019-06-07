using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Cfe.Mobile.App.Ordenes.Core.Core.OS;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Data {
    class MensajesDataContext : SQLiteDataContext {

        public MensajesDataContext() => CreateTablesAsync();

        protected override SQLiteConnection db =>
            _db ?? (_db = OS.DependencyService.Get<IDB>().ObtenerConexionBaseDatos(NombreBD));

        protected override SQLiteAsyncConnection dbAsync =>
            _dbAsync ?? (_dbAsync = OS.DependencyService.Get<IDB>().ObtenerConexionBaseDatosAsync(NombreBD));

        protected override string NombreBD => "BDMensajesAppCFE";

        protected override void CreateTables() {
            db.CreateTable<Model.Mensaje>();
            db.CreateTable<Model.Division>();
            db.CreateTable<Model.Zona>();
            db.CreateTable<Model.Area>();
            db.CreateTable<Model.Usuario>();
        }


        protected override async void CreateTablesAsync() {
            await dbAsync.CreateTableAsync<Model.Mensaje>();
            await dbAsync.CreateTableAsync<Model.Division>();
            await dbAsync.CreateTableAsync<Model.Zona>();
            await dbAsync.CreateTableAsync<Model.Area>();
            await dbAsync.CreateTableAsync<Model.Usuario>();
        }
    }
}
