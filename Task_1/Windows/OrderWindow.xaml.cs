using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace Task_1
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ObservableCollection<EmployeeVM> Employees { get; set; }
        public ObservableCollection<CustomerVM> Customers { get; set; }
        public ObservableCollection<ServiceVM> Services { get; set; }
        public OrderVM ModelOrder { get; set; }
        public DateTime SelectedStart
        {
            get
            {
                return new DateTime(2000, 1, 1, ModelOrder.Start.Hours, ModelOrder.Start.Minutes, 0); ;
            }
            set
            {
                ModelOrder.Start = value.TimeOfDay;
                OnPropertyChanged(nameof(SelectedStart));
            }
        }

        public OrderWindow()
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

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (ModelOrder.ModelOrder.Employee is null || ModelOrder.ModelOrder.Customer is null || ModelOrder.ModelOrder.Service is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            this.DialogResult = true;
        }
    }
}
