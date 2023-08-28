using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task_1.Windows;

namespace Task_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        NailContext db = new NailContext();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            CalendarSchedule.SelectedDatesChanged += CalendarSchedule_SelectedDatesChanged;
            AddCustomer.Click += AddCustomer_Click;
            EditCustomer.Click += EditCustomer_Click;
            DeleteCustomer.Click += DeleteCustomer_Click;
            AddService.Click += AddService_Click;
            EditService.Click += EditService_Click;
            DeleteService.Click += DeleteService_Click;
            AddEmployee.Click += AddEmployee_Click;
            EditEmployee.Click += EditEmployee_Click;
            DeleteEmployee.Click += DeleteEmployee_Click;
            AddPosition.Click += AddPosition_Click;
            EditPosition.Click += EditPosition_Click;
            DeletePosition.Click += DeletePosition_Click;
            AddOrder.Click += AddOrder_Click;
            AddSchedule.Click += AddSchedule_Click;
            
        }   
        //ПОЧАТОК ПОСАД
        public ObservableCollection<PositionVM> Positions
        {
            get
            {
                return new(db.Positions.Select(x => new PositionVM { ModelPosition = x }));
            }
        }
        private PositionVM _selectedPosition;
        public PositionVM SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                _selectedPosition = value;
                OnPropertyChanged(nameof(SelectedPosition));
            }
        }
        private void AddPosition_Click(object sender, RoutedEventArgs e)
        {
            var position = new Position();
            PositionWindow positionWindow = new PositionWindow();
            positionWindow.ModelPosition = new PositionVM { ModelPosition = position };
            bool? result = positionWindow.ShowDialog();
            if (result == false)
            {

                return;
            }
            db.Positions.Add(position);
            db.SaveChanges();
            OnPropertyChanged(nameof(Positions));

            MessageBox.Show("ADDED SUCCESSFUL");
        }
        private void EditPosition_Click(object sender, RoutedEventArgs e)
        {
            PositionWindow positionWindow = new PositionWindow();
            positionWindow.ModelPosition = SelectedPosition;
            var oldName = SelectedPosition.Name;
            bool? result = positionWindow.ShowDialog();
            if (result == false)
            {
                SelectedPosition.Name = oldName;
                return;
            }
            db.SaveChanges();
            OnPropertyChanged(nameof(Positions));
            MessageBox.Show("EDIDED SUCCESSFUL");
        }
        private void DeletePosition_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedPosition.ModelPosition;
            db.Positions.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Positions));
            MessageBox.Show("DELETED SUCCESSFUL");
        }
        // КІНЕЦЬ ПОСАД

        // ПОЧАТОК ПОСЛУГ
        public ObservableCollection<ServiceVM> Services
        {
            get
            {
                return new(db.Services.Select(x => new ServiceVM { ModelService = x }));
            }
        }
        private ServiceVM _selectedService;
        public ServiceVM SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
            }
        }
        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            var service = new Service();
            ServiceWindow serviceWindow = new ServiceWindow();
            serviceWindow.ModelService = new ServiceVM { ModelService = service };
            bool? result = serviceWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            db.Services.Add(service);
            db.SaveChanges();
            OnPropertyChanged(nameof(Services));

            MessageBox.Show("ADDED SUCCESSFUL");
        }
        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow serviceWindow = new ServiceWindow();
            serviceWindow.ModelService = SelectedService;
            var oldName = SelectedService.Name;
            var oldPrice = SelectedService.Price;
            var oldLenght = SelectedService.Lenght;
            bool? result = serviceWindow.ShowDialog();
            if (result == false)
            {
                SelectedService.Name = oldName;
                SelectedService.Price = oldPrice;
                SelectedService.Lenght = oldLenght;
                return;
            }
            db.SaveChanges();
            OnPropertyChanged(nameof(Services));
            MessageBox.Show("EDIDED SUCCESSFUL");
        }
        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedService.ModelService;
            db.Services.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Services));
            MessageBox.Show("DELETED SUCCESSFUL");
        }
        // КІНЕЦЬ ПОСЛУГ

        // ПОЧАТОК КЛІЄНТІВ
        public ObservableCollection<CustomerVM> Customers
        {
            get
            {
                return new(db.Customers.Where(x => x.Name.Contains(SearchCustomer)).Select(x => new CustomerVM { ModelCustomer = x }));
            }
        }
        private CustomerVM _selectedCustomer;
        public CustomerVM SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer();
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.ModelCustomer = new CustomerVM { ModelCustomer = customer };
            bool? result = customerWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            db.Customers.Add(customer);
            db.SaveChanges();
            OnPropertyChanged(nameof(Customers));
            MessageBox.Show("ADDED SUCCESSFUL");
        }
        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.ModelCustomer = SelectedCustomer;
            var oldName = SelectedCustomer.Name;
            var oldPhone = SelectedCustomer.Phone;
            bool? result = customerWindow.ShowDialog();
            if (result == false)
            {
                SelectedCustomer.Name = oldName;
                SelectedCustomer.Phone = oldPhone;
                return;
            }
            db.SaveChanges();
            OnPropertyChanged(nameof(Customers));
            MessageBox.Show("EDIDED SUCCESSFUL");
        }
        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedCustomer.ModelCustomer;
            db.Customers.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Customers));
            MessageBox.Show("DELETED SUCCESSFUL");
        }
        private string _searchCustomer = "";
        public string SearchCustomer
        {
            get => _searchCustomer;
            set
            {
                _searchCustomer = value;
                OnPropertyChanged(nameof(SearchCustomer));
                OnPropertyChanged(nameof(Customers));
            }
        }
        // КІНЕЦЬ КЛІЄНТІВ

        // ПОЧАТОК СПІВРОБІТНИКІВ
        public ObservableCollection<EmployeeVM> Employees
        {
            get
            {
                return new(db.Employees.Include(x => x.Position).Select(x => new EmployeeVM { ModelEmployee = x }));
            }
        }
        private EmployeeVM _selectedEmployee;
        public EmployeeVM SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee();
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.Positions = Positions;
            employeeWindow.ModelEmployee = new EmployeeVM { ModelEmployee = employee };
            bool? result = employeeWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            db.Employees.Add(employee);
            db.SaveChanges();
            OnPropertyChanged(nameof(Employees));
            MessageBox.Show("ADDED SUCCESSFUL");
        }
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow();
            employeeWindow.Positions = Positions;
            employeeWindow.ModelEmployee = SelectedEmployee;
            var oldName = SelectedEmployee.Name;
            var oldPhone = SelectedEmployee.Phone;
            var oldPositionId = SelectedEmployee.PositionId;

            bool? result = employeeWindow.ShowDialog();
            if (result == false)
            {
                SelectedEmployee.Name = oldName;
                SelectedEmployee.Phone = oldPhone;
                SelectedEmployee.PositionId = oldPositionId;
                return;
            }
            db.SaveChanges();
            OnPropertyChanged(nameof(Employees));
            MessageBox.Show("EDIDED SUCCESSFUL");
        }
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var item = SelectedEmployee.ModelEmployee;
            db.Employees.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Employees));
            MessageBox.Show("DELETED SUCCESSFUL");
        }

        //private void EditEmployee_Click(object sender, RoutedEventArgs e)
        //{
        //    var item = (Employee)gridEmployee.SelectedItem;
        //    var obj = db.Employees.Find(item.Id);
        //    EmployeeWindow employeeWindow = new EmployeeWindow();
        //    employeeWindow.textName.Text = obj.Name;
        //    employeeWindow.textPhone.Text = obj.Phone;
        //    employeeWindow.cbPosition.SelectedIndex = employeeWindow.cbPosition.Items.Cast<Position>().ToList().FindIndex(p => p.Id == obj.PositionId);
        //    bool? result = employeeWindow.ShowDialog();
        //    if (result == false)
        //        return;
        //    obj.Name = employeeWindow.textName.Text;
        //    obj.Phone = employeeWindow.textPhone.Text;
        //    var pos = (Position)employeeWindow.cbPosition.SelectedItem;
        //    obj.Position = db.Positions.Find(pos.Id);
        //    db.SaveChanges();
        //    gridEmployee.Items.Refresh();
        //    MessageBox.Show("EDIDED SUCCESSFUL");
        //}
        // КІНЕЦЬ СПІВРОБІТНИКІВ

        // ПОЧАТОК РОЗКЛАДУ
        
        public ObservableCollection<ScheduleVM> Schedules
        {
            get
            {
                return new(db.Schedules.Select(x => new ScheduleVM { ModelSchedule = x }));
            }
        }
        private EmployeeVM _selectedSchedule;
        public EmployeeVM SelectedSchedule
        {
            get => _selectedSchedule;
            set
            {
                _selectedSchedule = value;
                OnPropertyChanged(nameof(SelectedSchedule));
            }
        }
        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            var schedule = new Schedule();
            ScheduleWindow scheduleWindow = new ScheduleWindow();
            scheduleWindow.Employees = Employees;
            scheduleWindow.ModelSchedule = new ScheduleVM { ModelSchedule = schedule };
            bool? result = scheduleWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            db.Schedules.Add(schedule);
            db.SaveChanges();
            OnPropertyChanged(nameof(Schedules));
            MessageBox.Show("ADDED SUCCESSFUL");
        }
        // КІНЕЦЬ РОЗКЛАДУ

        // ПОЧАТОК ЗАПИСІВ
        public ObservableCollection<OrderVM> Orders
        {
            get
            {
                return new(db.Orders.Select(x => new OrderVM { ModelOrder = x }));
            }
        }
        private OrderVM _selectedOrder;
        public OrderVM SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = new Order();
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Employees = Employees;
            orderWindow.Customers = Customers;
            orderWindow.Services = Services;
            orderWindow.ModelOrder = new OrderVM { ModelOrder = order };
            bool? result = orderWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            db.Orders.Add(order);
            db.SaveChanges();
            OnPropertyChanged(nameof(Orders));
            MessageBox.Show("ADDED SUCCESSFUL");
        }
        //private void EditOrder_Click(object sender, RoutedEventArgs e)
        //{
        //    OrderWindow orderWindow = new OrderWindow();
        //    orderWindow.Positions = Positions;
        //    orderWindow.ModelOrder = SelectedOrder;
        //    var oldName = SelectedOrder.Name;
        //    var oldPhone = SelectedOrder.Phone;
        //    var oldPositionId = SelectedOrder.PositionId;

        //    bool? result = orderWindow.ShowDialog();
        //    if (result == false)
        //    {
        //        SelectedOrder.Name = oldName;
        //        SelectedOrder.Phone = oldPhone;
        //        SelectedOrder.PositionId = oldPositionId;
        //        return;
        //    }
        //    db.SaveChanges();
        //    OnPropertyChanged(nameof(Orders));
        //    MessageBox.Show("EDIDED SUCCESSFUL");
        //}
        //private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        //{
        //    var item = SelectedOrder.ModelOrder;
        //    db.Orders.Remove(item);
        //    db.SaveChanges();
        //    OnPropertyChanged(nameof(Orders));
        //    MessageBox.Show("DELETED SUCCESSFUL");
        //}
        // КІНЕЦЬ ЗАПИСІВ
        private void CalendarSchedule_SelectedDatesChanged(object? sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = CalendarSchedule.SelectedDate;
            //Schedule. 
        }
    }
}
