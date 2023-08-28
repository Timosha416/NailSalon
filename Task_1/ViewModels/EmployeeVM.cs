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
        public Position Position
        {
            get { return ModelEmployee.Position; }
            set
            {
                ModelEmployee.Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

