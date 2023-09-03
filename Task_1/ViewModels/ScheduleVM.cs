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
    public class ScheduleVM : INotifyPropertyChanged
    {
        public Schedule ModelSchedule { get; set; }

        public int Id
        {
            get { return ModelSchedule.Id; }
            set
            {
                ModelSchedule.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public int EmployeeId
        {
            get { return ModelSchedule.EmployeeId; }
            set
            {
                ModelSchedule.EmployeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }
        public string Date
        {
            get { return ModelSchedule.Date; }
            set
            {
                ModelSchedule.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public TimeSpan Start
        {
            get { return ModelSchedule.Start; }
            set
            {
                ModelSchedule.Start = value;
                OnPropertyChanged(nameof(Start));
            }
        }
        public TimeSpan End
        {
            get { return ModelSchedule.End; }
            set
            {
                ModelSchedule.End = value;
                OnPropertyChanged(nameof(End));
            }
        }
        public EmployeeVM Employee
        {
            get { return new EmployeeVM { ModelEmployee = ModelSchedule.Employee }; }
            set
            {
                ModelSchedule.Employee = value.ModelEmployee;
                OnPropertyChanged(nameof(Employee));
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not ScheduleVM) return false;
            if ((obj as ScheduleVM).ModelSchedule == null) return false;
            return ModelSchedule.Id.Equals((obj as ScheduleVM).ModelSchedule.Id);
        }
        
        public override int GetHashCode()
        {
            return ModelSchedule.Id.GetHashCode();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

