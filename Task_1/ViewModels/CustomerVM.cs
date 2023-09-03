using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace Task_1
{
    public class CustomerVM : INotifyPropertyChanged
    {
        public Customer ModelCustomer { get; set; }

        public int Id
        {
            get { return ModelCustomer.Id; }
            set
            {
                ModelCustomer.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return ModelCustomer.Name; }
            set
            {
                ModelCustomer.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Phone
        {
            get { return ModelCustomer.Phone; }
            set
            {
                ModelCustomer.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not CustomerVM) return false;
            if ((obj as CustomerVM).ModelCustomer == null) return false;
            return ModelCustomer.Id.Equals((obj as CustomerVM).ModelCustomer.Id);
        }

        public override int GetHashCode()
        {
            return ModelCustomer.Id.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

