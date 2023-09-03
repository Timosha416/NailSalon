using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
            EditOrder.Click += EditOrder_Click;
            DeleteOrder.Click += DeleteOrder_Click;
            AddSchedule.Click += AddSchedule_Click;
            setPass.Click += SetPass_Click;
            statsName.SelectionChanged += Stats_SelectionChanged;
            statsMounthFrom.SelectionChanged += Stats_SelectionChanged;
            statsMounthTo.SelectionChanged += Stats_SelectionChanged;
            statsYear.SelectionChanged += Stats_SelectionChanged;
            chartYear.SelectionChanged += ChartYear_SelectionChanged;
            checkPass.Click += CheckPass_Click;
            tControl.SelectionChanged += TControl_SelectionChanged;

            DataContext = this;
            statsMounthFrom.ItemsSource = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            statsMounthTo.ItemsSource = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            chartYear.ItemsSource = new List<int> { 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030 };
            statsYear.ItemsSource = new List<int> { 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030 };
            statsName.SelectedIndex = 0;
            statsMounthFrom.SelectedIndex = DateTime.Now.Month - 1;
            statsMounthTo.SelectedIndex = DateTime.Now.Month - 1;
            statsYear.SelectedIndex = 3;
            chartYear.SelectedIndex = 3;
        }

        private void CheckPass_Click(object sender, RoutedEventArgs e)
        {
            if (checkPassText.Password.ToString() == db.Passwords.First().Pass)
            {
                MessageBox.Show("Вхід успішний!", "Повідомлення", MessageBoxButton.OK);
                tControl.SelectedIndex = 5;
                pass.Visibility = Visibility.Collapsed;
                admin.Visibility = Visibility.Visible;       
            }
            else
                MessageBox.Show("Некоректний пароль!", "Помилка", MessageBoxButton.OK);
        }

        private void TControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!e.AddedItems.Contains(admin))
            {
                //PasswordWindow passwordWindow = new PasswordWindow();
                //passwordWindow.db = db;
                //bool? result = passwordWindow.ShowDialog();
                //if (result == false)
                //{
                //    tControl.SelectedItem = e.RemovedItems[0];
                //    return;
                //}
                checkPassText.Password = "";
                pass.Visibility = Visibility.Visible;
                admin.Visibility = Visibility.Collapsed;
            }
        }

        // ЗМІНА ПАРОЛЮ
        private void SetPass_Click(object sender, RoutedEventArgs e)
        {
            if (curPass.Password.ToString() == db.Passwords.First().Pass)
            {
                db.Passwords.First().Pass = newPass.Password.ToString();
                db.SaveChanges();
                MessageBox.Show("Пароль успішно змінено!", "Повідомлення", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Некоректний пароль!", "Помилка", MessageBoxButton.OK);
        }
        // ГІСТОГАМА ДОХОДУ
        private void ChartYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Charts.AxisX.Clear();
            Charts.AxisY.Clear();
            var forCharts = db.Orders.Include(x => x.Service).Where(e => e.Date.Year == chartYear.SelectedIndex + 2020).GroupBy(x => x.Date.Month).OrderBy(m => m.Key).Select(g => new { Month = g.Key, TotalPrice = g.Sum(x => x.Service.Price)}).ToList();
            Charts.Series = new LiveCharts.SeriesCollection() { new ColumnSeries() { Values = new ChartValues<double>(forCharts.Select(x => x.TotalPrice).ToList()), Title = "Дохід" } };
            Charts.AxisX.Add(new Axis { Labels = forCharts.Select(e => e.Month.ToString()).ToList<string>(), Separator = new LiveCharts.Wpf.Separator { Step = 1 }, Title = "Місяці", FontSize = 12, Foreground = Brushes.Black, FontWeight = FontWeights.Bold } );
            Charts.AxisY.Add(new Axis { Title = "Дохід", FontSize = 12, Foreground = Brushes.Black, FontWeight = FontWeights.Bold });
            all.Content = forCharts.Select(x => x.TotalPrice).Sum() + " грн.";
        }
        // СТАТИСТИКА
        private void Stats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statsName.SelectedIndex == 0)
                gridStats.ItemsSource = db.Orders.Include(x => x.Employee).Where(e => e.Date.Year == statsYear.SelectedIndex + 2020 && e.Date.Month >= statsMounthFrom.SelectedIndex + 1 && e.Date.Month <= statsMounthTo.SelectedIndex + 1).GroupBy(x => x.Employee.Name).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();
            if (statsName.SelectedIndex == 1)
                gridStats.ItemsSource = db.Orders.Include(x => x.Service).Where(e => e.Date.Year == statsYear.SelectedIndex + 2020 && e.Date.Month >= statsMounthFrom.SelectedIndex + 1 && e.Date.Month <= statsMounthTo.SelectedIndex + 1).GroupBy(x => x.Service.Name).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();
            if (statsName.SelectedIndex == 2)
                gridStats.ItemsSource = db.Orders.Include(x => x.Customer).Where(e => e.Date.Year == statsYear.SelectedIndex + 2020 && e.Date.Month >= statsMounthFrom.SelectedIndex + 1 && e.Date.Month <= statsMounthTo.SelectedIndex + 1).GroupBy(x => x.Customer.Name).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

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
            MessageBox.Show("Додано!");
        }
        private void EditPosition_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
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
            MessageBox.Show("Змінено!");
        }
        private void DeletePosition_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPosition is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            var item = SelectedPosition.ModelPosition;
            db.Positions.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Positions));
            MessageBox.Show("Видалено!");
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
            MessageBox.Show("Додано!");
        }
        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedService is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
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
            MessageBox.Show("Змінено!");
        }
        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedService is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            var item = SelectedService.ModelService;
            db.Services.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Services));
            MessageBox.Show("Видалено!");
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
            MessageBox.Show("Додано!");
        }
        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
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
            MessageBox.Show("Змінено!");
        }
        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCustomer is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            var item = SelectedCustomer.ModelCustomer;
            db.Customers.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Customers));
            MessageBox.Show("Видалено!");
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
            MessageBox.Show("Додано!");
        }
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
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
            MessageBox.Show("Змінено!");
        }
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            var item = SelectedEmployee.ModelEmployee;
            db.Employees.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Employees));
            MessageBox.Show("Видалено!");
        }
        // КІНЕЦЬ СПІВРОБІТНИКІВ

        // ПОЧАТОК РОЗКЛАДУ     
        public ObservableCollection<ScheduleVM> Schedules
        {
            get
            {
                return new(db.Schedules.Where(e => e.Employee == SelectedEmployeeSchedule.ModelEmployee).Select(x => new ScheduleVM { ModelSchedule = x }));
            }
        }
        private EmployeeVM _selectedEmployeeSchedule = new EmployeeVM { };
        public EmployeeVM SelectedEmployeeSchedule
        {
            get => _selectedEmployeeSchedule;
            set
            {
                _selectedEmployeeSchedule = value;
                OnPropertyChanged(nameof(SelectedEmployeeSchedule));
                OnPropertyChanged(nameof(Schedules));
                OnPropertyChanged(nameof(Orders));
            }
        }
        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployeeSchedule.ModelEmployee is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            ScheduleWindow scheduleWindow = new ScheduleWindow();
            scheduleWindow.Employees = Employees;
            scheduleWindow.Schedules = Schedules;
            scheduleWindow.db = db;
            scheduleWindow.ModelEmployee = new EmployeeVM { ModelEmployee = SelectedEmployeeSchedule.ModelEmployee };
            bool? result = scheduleWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            OnPropertyChanged(nameof(Schedules));
            MessageBox.Show("Додано!");
        }
        // КІНЕЦЬ РОЗКЛАДУ

        // ПОЧАТОК ЗАПИСІВ
        public ObservableCollection<OrderVM> Orders
        {
            get
            {
                return new(db.Orders.Where(e => e.Date == SelectedDate && e.Employee == SelectedEmployeeSchedule.ModelEmployee).Select(x => new OrderVM { ModelOrder = x }));
            }
        }
        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(Orders));
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
            MessageBox.Show("Додано!");
        }
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Employees = Employees;
            orderWindow.Customers = Customers;
            orderWindow.Services = Services;            
            orderWindow.ModelOrder = SelectedOrder;
            var oldEmployeeId = SelectedOrder.EmployeeId;
            var oldCustomerId = SelectedOrder.CustomerId;
            var olderviceId = SelectedOrder.ServiceId;
            var oldDate = SelectedOrder.Date;
            var oldStart = SelectedOrder.Start;
            bool? result = orderWindow.ShowDialog();
            if (result == false)
            {
                SelectedOrder.EmployeeId = oldEmployeeId;
                SelectedOrder.CustomerId = oldCustomerId;
                SelectedOrder.ServiceId = olderviceId;
                SelectedOrder.Date = oldDate;
                SelectedOrder.Start = oldStart;
                return;
            }
            db.SaveChanges();
            OnPropertyChanged(nameof(Orders));
            MessageBox.Show("Змінено!");
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder is null)
            {
                MessageBox.Show("Оберіть значення!", "Повідомлення", MessageBoxButton.OK);
                return;
            }
            var item = SelectedOrder.ModelOrder;
            db.Orders.Remove(item);
            db.SaveChanges();
            OnPropertyChanged(nameof(Orders));
            MessageBox.Show("Видалено!");
        }
        // КІНЕЦЬ ЗАПИСІВ        
    }
}
