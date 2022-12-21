using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL.Interfaces;
using BLL.Services;

namespace PL
{
    /// <summary>
    /// Interaction logic for Change_Password.xaml
    /// </summary>
    public partial class Change_Password : Window
    {
        private IKernel kernel;
        public Change_Password(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void ChangePasswordCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangePasswordUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var service = kernel.Get<IUserService>();
            string current = ChangePasswordCurrentPasswordTextBox.Password;
            string newPassword = ChangePasswordNewPasswordTextBox.Password;
            string confirmPassword = ChangePasswordConfirmPasswordTextBox.Password;
            if (current.Length == 0 || newPassword.Length == 0 || confirmPassword.Length == 0)
            {
                MessageBox.Show("Fields can not be empty");
                return;
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Password doesn't match.");
                return;
            }
            if (!service.ChangePassword(current, newPassword))
            {
                MessageBox.Show("Current password is different");
                return;
            }
            Close();
        }
    }
}
