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
using System.ComponentModel;
using System.Linq;
using DAL.Domain;
using DAL.Interfaces;
using DAL.Enums;
using BLL.Interfaces;
using System.Threading.Tasks;
using log4net;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IKernel kernel;

        private readonly Dictionary<string, AccountType> dictionary = new Dictionary<string, AccountType>
        {
            ["income"] = AccountType.Income,
            ["expences"] = AccountType.Expence,
            ["current"] = AccountType.Current
        };
        public AddAccount(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
            AddAccountCurrencyComboBox.ItemsSource = kernel.Get<IUnitOfWork>().Repository<Currency>().Get().Select(x => x.Code).ToList<String>();
        }

        private void AddAccountCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddAccountOKButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Add account button clicked");
            var service = kernel.Get<IBankAccountService>();
            string name = AddAccountNameTextBox.Text;
            string typeString = AddAccountTypeComboBox.Text;
            string currencyString = AddAccountCurrencyComboBox.Text;
            if (name.Length == 0 || typeString == "Type" || currencyString.Length == 0)
            {
                MessageBox.Show("Fields can not be empty");
                return;
            }
            AccountType type = dictionary[AddAccountTypeComboBox.Text.ToLower()];
            Currency currency = kernel.Get<IUnitOfWork>().Repository<Currency>().Get().FirstOrDefault(x => x.Code == AddAccountCurrencyComboBox.Text);
            BankAccount bankAccount = service.CreateAccount(type, name, currency);
            if (bankAccount == null)
            {
                MessageBox.Show("Something went wrong");
                kernel.Get<ILog>().Info("Add account failed");
            }
            PropertyChanged(this, new PropertyChangedEventArgs("Added"));
            Close();
        }
    }
}
