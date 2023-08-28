using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Task_1
{
    public class PositionVM : INotifyPropertyChanged
    {
        public Position ModelPosition { get; set; }

        public int Id
        {
            get { return ModelPosition.Id; }
            set
            {
                ModelPosition.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        
        public string Name
        {
            get { return ModelPosition.Name; }
            set
            {
                ModelPosition.Name = value;
                OnPropertyChanged(nameof(Name));
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
