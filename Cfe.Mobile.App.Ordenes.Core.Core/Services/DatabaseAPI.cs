using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cfe.Mobile.App.Ordenes.Core.Core.Model;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Services {
    class DatabaseAPI : WebApiClient {
        public DatabaseAPI() : base("DataBase/") {

        }
        public async Task<List<Division>> ObtenerDivisiones() =>
            await CallGetAsync<List<Division>>($"GetDivisiones/");

        public async Task<List<Zona>> ObtenerZonas() =>
            await CallGetAsync<List<Zona>>($"GetZonas/");

        public async Task<List<Area>> ObtenerAreas() =>
            await CallGetAsync<List<Area>>($"GetAreas/");
    }
}
