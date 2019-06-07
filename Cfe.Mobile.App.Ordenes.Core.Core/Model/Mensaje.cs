using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace Cfe.Mobile.App.Ordenes.Core.Core.Model {
    [Table("Mensajes")]
    public class Mensaje: INotifyPropertyChanged {

        private int id;
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get => id; set => Set(ref id, value); }

        private string rpu;
        [Column("Rpu"), MaxLength(50)]
        public string Rpu { get => rpu; set => Set(ref rpu, value); }

        private string medidor;
        [Column("Medidor"), MaxLength(6)]
        public string Medidor { get => medidor; set => Set(ref medidor, value); }

        private string posicion;
        [Column("Posicion"), MaxLength(50)]
        public string Posicion { get => posicion; set => Set(ref posicion, value); }

        private string noorden;
        [Column("NoOrden"), MaxLength(50)]
        public string NoOrden { get => noorden; set => Set(ref noorden, value); }

        private string descripcion;
        [Column("Descripcion"), MaxLength(5000)]
        public string Descripcion { get => descripcion; set => Set(ref descripcion, value); }

        private int iddispositivo;
        [Column("IdDispositivo")]
        public int IdDispositivo { get => iddispositivo; set => Set(ref iddispositivo, value); }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
