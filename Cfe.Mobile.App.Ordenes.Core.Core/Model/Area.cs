using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cfe.Mobile.App.Ordenes.Core.Core.Model {
    class Area : INotifyPropertyChanged {
        private int id;
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get => id; set => Set(ref id, value); }

        private string clave;
        [Column("Clave"), MaxLength(5)]
        public string Clave { get => clave; set => Set(ref clave, value); }

        private string nombre;
        [Column("Nombre"), MaxLength(50)]
        public string Nombre { get => nombre; set => Set(ref nombre, value); }
        
        private int idzona;
        [Column("IdZona")]
        public int IdZona { get => idzona; set => Set(ref idzona, value); }



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
