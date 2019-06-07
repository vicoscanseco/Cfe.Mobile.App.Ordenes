using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Cfe.Mobile.App.Ordenes.Core.Core.Model {
    class Usuario : INotifyPropertyChanged {

        private int id;
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get => id; set => Set(ref id, value); }

        private string nombre;
        [Column("Nombre"), MaxLength(50)]
        public string Nombre { get => nombre; set => Set(ref nombre, value); }

        private string rpe;
        [Column("Rpe"), MaxLength(50)]
        public string Rpe { get => rpe; set => Set(ref rpe, value); }

        private int iddiv;
        [Column("IdDivision")]
        public int IdDivision { get => iddiv; set => Set(ref iddiv, value); }
        private int idzona;
        [Column("IdZona")]
        public int IdZona { get => idzona; set => Set(ref idzona, value); }

        private int idarea;
        [Column("IdArea")]
        public int IdArea { get => idarea; set => Set(ref idarea, value); }        
       
        private string idequipo;
        [Column("IdEquipo"), MaxLength(500)]
        public string IdEquipo { get => idequipo; set => Set(ref idequipo, value); }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
