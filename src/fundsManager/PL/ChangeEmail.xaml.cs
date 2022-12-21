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

namespace PL
{
    /// <summary>
    /// Interaction logic for ChangeEmail.xaml
    /// </summary>
    public partial class ChangeEmail : Window
    {
        private IKernel kernel;
        public ChangeEmail(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }

        private void ChangeEmailCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangeEmailUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var service = kernel.Get<IUserService>();
            var mail = NewEmailTextBox.Text;
            if (!service.ChangeMail(mail))
            {
                MessageBox.Show("Invalid email");
                return;
            }
            Close();
        }
    }
}
