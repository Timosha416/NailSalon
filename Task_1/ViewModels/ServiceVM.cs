using Microsoft.EntityFrameworkCore.Infrastructure;
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
            get {
                if (ModelService.Lenght.Hours == 0 && ModelService.Lenght.Minutes == 0)
                    ModelService.Lenght = new TimeSpan(1,0,0);
                return ModelService.Lenght; 
            }
            set
            {
                ModelService.Lenght = value;
                OnPropertyChanged(nameof(Lenght));
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not ServiceVM) return false;
            if ((obj as ServiceVM).ModelService == null) return false;
            return ModelService.Id.Equals((obj as ServiceVM).ModelService.Id);
        }

        public override int GetHashCode()
        {
            return ModelService.Id.GetHashCode();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

