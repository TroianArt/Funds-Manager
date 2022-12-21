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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Interfaces;
using DAL.Domain;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl, INotifyPropertyChanged
    {
        private IKernel kernel;

        private readonly BankAccount account;

        public event PropertyChangedEventHandler PropertyChanged;
        public AccountControl(IKernel kernel, BankAccount acc)
        {
            InitializeComponent();
            this.kernel = kernel;
            this.account = acc;
            AccountNameLabel.Content = account.Name;
            AccountCurrencyLabel.Content = account.CurrencyType.Code;
            AccountTypeLabel.Content = account.Type;
            AccountCostLabel.Content = kernel.Get<IBankAccountService>().GetAccountScore(account);
        }

        private void AccountOptionsShare_Click(object sender, RoutedEventArgs e)
        {
            ShareAccount ShareAccount = new ShareAccount(kernel, account);
            ShareAccount.Show();
        }
        private void AccountOptionsDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteAccount DeleteAccount = new DeleteAccount(kernel, account);
            DeleteAccount.PropertyChanged += DeleteCompleted;
            DeleteAccount.Show();
        }

        private void DeleteCompleted(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Deleted"));
        }
    }
}
