using BLL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using log4net;

namespace PL
{
    public partial class Registration : Window
    {
        private IKernel kernel;
        public Registration(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Cancel button clicked");
            MainWindow win1 = new MainWindow();
            win1.Show();
            SystemCommands.CloseWindow(this);
        }

        private void RegSignUpButton_Click(object sender, RoutedEventArgs e)
        {

            kernel.Get<ILog>().Info("Registration button clicked");
            IUserService userService = kernel.Get<IUserService>();
            string firstName = FirstNameTextBox.Text;
            string secondName = SecondNameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;
            string confirmPassword = ConfirmPasswordTextBox.Password;
            Regex phoneRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
          
            if (firstName.Length==0|| secondName.Length==0|| phone.Length==0|| email.Length == 0 || password.Length == 0|| confirmPassword.Length==0)
            {
                ErrorLabel.Content = "Any field can not be empty.";
                return;
            }
            if (!userService.IsValidMail(email))
            {
                ErrorLabel.Content = "The email is not a valid email address.";
                return;
            }
            if (phoneRegex.IsMatch(password))
            {
                ErrorLabel.Content = "The password is not a valid password";
                return;
            }
            if (password!= confirmPassword)
            {
                ErrorLabel.Content = "Password doesn't match.";
                return;
            }
            try
            {
                var user = userService.SignUp(firstName, secondName, email, phone, password);
                MainForm mainForm = new MainForm(kernel);
                mainForm.Show();
                Close();
                kernel.Get<ILog>().Info("Registration success");
            }
            catch (ArgumentException exc)
            {
                kernel.Get<ILog>().Info("Registration failed");
                ErrorLabel.Content = exc.Message;
            }
  
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
