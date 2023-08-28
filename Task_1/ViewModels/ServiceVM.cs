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
    public class ServiceVM : INotifyPropertyChanged
    {
        public Service ModelService { get; set; }

        public int Id
        {
            get { return ModelService.Id; }
            set
            {
                ModelService.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return ModelService.Name; }
            set
            {
                ModelService.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public double Price
        {
            get { return ModelService.Price; }
            set
            {
                ModelService.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public TimeSpan Lenght
        {
            get { return ModelService.Lenght; }
            set
            {
                ModelService.Lenght = value;
                OnPropertyChanged(nameof(Lenght));
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

