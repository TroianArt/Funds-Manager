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
using DAL.Domain;
using BLL.Interfaces;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DeleteAccount.xaml
    /// </summary>
    public partial class DeleteAccount : Window, INotifyPropertyChanged
    {
        private IKernel kernel;

        private readonly BankAccount account;

        public event PropertyChangedEventHandler PropertyChanged;
        public DeleteAccount(IKernel kernel, BankAccount acc)
        {
            InitializeComponent();
            this.kernel = kernel;
            this.account = acc;
        }

        private void ChangeEmailCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void DeleteAccountOKButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<IBankAccountService>().DeleteAccount(account);
            PropertyChanged(this, new PropertyChangedEventArgs("Deleted"));
            Close();
        }
    }
}
