using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Model {
    /// <summary>
    /// Clase usada para manipulacion de datos en Viewmodel
    /// </summary>
    public class CustomPin {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Titulo { get; set; }
        public int Estatus { get; set; }
    }

    /// <summary>
    /// Enumerados de estatus para objetos CustomPin
    /// </summary>
    public enum EstatusPines {
        Alerta = 0,
        Activo = 1,
        Inactivo = 2
    }

}
