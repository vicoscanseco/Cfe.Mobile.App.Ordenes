using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cfe.Mobile.App.Ordenes.Core.Core.Model;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Services {
    class MensajesAPI: WebApiClient {

        public MensajesAPI() : base("Ordenes/") {

        }

        public async Task<List<Mensaje>> Obtener(string rpe) =>
            await CallGetAsync<List<Mensaje>>($"GetDatos/{rpe}");


        public async Task Registrar(string rpe, string id) =>
            //await GuardarEquipoAsync("RegistraEquipo", new RpeEquipos { RPE = rpe, IDEquipo = id });
            await CallPostAsync<object, RpeEquipos>("RegistraEquipo", new { RPE = rpe, IDEquipo = id });




    }
}
