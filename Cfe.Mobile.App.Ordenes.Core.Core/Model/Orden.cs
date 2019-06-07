using System;
using System.Collections.Generic;
using System.Text;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Model {
    public class Orden {

        public int Id { get; set; }

        public string Rpu { get; set; }

        public string Medidor { get; set; }

        public string Posicion { get; set; }

        public string NoOrden { get; set; }

        public string Descripcion { get; set; }

        public int IdDispositivo { get; set; }

        public Orden(Mensaje mensaje) {
            Id = mensaje.Id;
            Rpu = mensaje.Rpu;
            Medidor = mensaje.Medidor;
            Posicion = mensaje.Posicion;
            NoOrden = mensaje.NoOrden;
            Descripcion = mensaje.Descripcion;
            IdDispositivo = mensaje.IdDispositivo;
        }

    }
}
