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
    public class EmployeeVM : INotifyPropertyChanged
    {
        public Employee ModelEmployee { get; set; }

        public int Id
        {
            get { return ModelEmployee.Id; }
            set
            {
                ModelEmployee.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return ModelEmployee.Name; }
            set
            {
                ModelEmployee.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Phone
        {
            get { return ModelEmployee.Phone; }
            set
            {
                ModelEmployee.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public int PositionId
        {
            get { return ModelEmployee.PositionId; }
            set
            {
                ModelEmployee.PositionId = value;
                OnPropertyChanged(nameof(PositionId));
            }
        }
        public PositionVM Position
        {
            get { return new PositionVM { ModelPosition = ModelEmployee.Position }; }
            set
            {
                ModelEmployee.Position = value.ModelPosition;
                OnPropertyChanged(nameof(Position));
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not EmployeeVM) return false;
            if ((obj as EmployeeVM).ModelEmployee == null) return false;
            return ModelEmployee.Id.Equals((obj as EmployeeVM).ModelEmployee.Id);
        }

        public override int GetHashCode()
        {
            return ModelEmployee.Id.GetHashCode();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

