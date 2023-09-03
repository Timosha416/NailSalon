using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task_1.Windows
{
    /// <summary>
    /// Логика взаимодействия для ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window, INotifyPropertyChanged
    {        
        public NailContext db;
        public ObservableCollection<EmployeeVM> Employees { get; set; }
        public ObservableCollection<ScheduleVM> Schedules { get; set; }
        public ScheduleVM ModelSchedule { get; set; }
        public EmployeeVM ModelEmployee { get; set; }
        public int empId;
        //public EmployeeVM SelectedEmployee
        //{
        //    set { empId = value.Id; OnPropertyChanged(nameof(SelectedEmployee)); }
        //}
        public ScheduleWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = this;
            Ok.Click += Ok_Click;
            Cancel.Click += Cancel_Click;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        public void Edit(string date, int empId, ScheduleVM ModelSchedule)
        {
            ModelSchedule = new ScheduleVM { ModelSchedule = new Schedule() };
            ModelSchedule.ModelSchedule = Schedules.First(x => x.Date == date && x.EmployeeId == empId).ModelSchedule;
            ModelSchedule.Start = Start.Value.Value.TimeOfDay;
            ModelSchedule.End = End.Value.Value.TimeOfDay;
            db.SaveChanges();
        }
        public void Add(string date, int empId, ScheduleVM ModelSchedule)
        {
            ModelSchedule = new ScheduleVM { ModelSchedule = new Schedule() };
            ModelSchedule.Date = date;
            ModelSchedule.EmployeeId = empId;
            ModelSchedule.Start = Start.Value.Value.TimeOfDay;
            ModelSchedule.End = End.Value.Value.TimeOfDay;
            db.Schedules.Add(ModelSchedule.ModelSchedule);
            db.SaveChanges();
        }
        public void Delete(string date, int empId, ScheduleVM ModelSchedule)
        {
            ModelSchedule = new ScheduleVM { ModelSchedule = new Schedule() };
            ModelSchedule.ModelSchedule = Schedules.First(x => x.Date == date && x.EmployeeId == empId).ModelSchedule;
            db.Schedules.Remove(ModelSchedule.ModelSchedule);
            db.SaveChanges();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {            
            empId = ModelEmployee.Id;
            ModelSchedule = new ScheduleVM { ModelSchedule = new Schedule() };            
            if (Monday.IsChecked == true) { try { Edit("Понеділок", empId, ModelSchedule); } catch { Add("Понеділок", empId, ModelSchedule); } }
            else try { Delete("Понеділок", empId, ModelSchedule); } catch { };
            if (Tuesday.IsChecked == true) { try { Edit("Вівторок", empId, ModelSchedule); } catch { Add("Вівторок", empId, ModelSchedule); } }
            else try { Delete("Вівторок", empId, ModelSchedule); } catch { };
            if (Wednesday.IsChecked == true) { try { Edit("Середа", empId, ModelSchedule); } catch { Add("Середа", empId, ModelSchedule); } }
            else try { Delete("Середа", empId, ModelSchedule); } catch { };
            if (Thursday.IsChecked == true) { try { Edit("Четвер", empId, ModelSchedule); } catch { Add("Четвер", empId, ModelSchedule); } }
            else try { Delete("Четвер", empId, ModelSchedule); } catch { };
            if (Friday.IsChecked == true) { try { Edit("П'ятниця", empId, ModelSchedule); } catch { Add("П'ятниця", empId, ModelSchedule); } }
            else try { Delete("П'ятниця", empId, ModelSchedule); } catch { };
            if (Saturday.IsChecked == true) { try { Edit("Субота", empId, ModelSchedule); } catch { Add("Субота", empId, ModelSchedule); } }
            else try { Delete("Субота", empId, ModelSchedule); } catch { };
            this.DialogResult = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
