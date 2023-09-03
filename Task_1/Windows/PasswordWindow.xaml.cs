using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public int previousTabIndex;
        public NailContext db;
        //public bool flag;
        public PasswordWindow()
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
            if (enterPass.Password.ToString() != db.Passwords.First().Pass)
            {
                //flag = false;
                this.DialogResult = false;
            }
            else
                this.DialogResult = true;
        }
    }
}
