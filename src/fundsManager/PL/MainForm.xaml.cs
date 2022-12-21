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
using LiveCharts;
using LiveCharts.Wpf;
using BLL.Interfaces;
using System.Linq;
using BLL.Models;
using DAL.Interfaces;
using DAL.Domain;
using DAL.Enums;
using System.ComponentModel;
using log4net;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private IKernel kernel;
        public List<string> LabelsIncome { get; set; }
        public List<string> LabelsExpence { get; set; }
        public MainForm(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
            var statsService = kernel.Get<IStatisticsService>();
            statsService.CurrentUser = kernel.Get<IUserService>().CurrentUser;
            var bankAccountService = kernel.Get<IBankAccountService>();
            bankAccountService.CurrentUser = kernel.Get<IUserService>().CurrentUser;

            TransactionsTypeComboBox.DropDownClosed += new System.EventHandler(TransactionTypeChanged);

            UpdateAccountsView();

            //Stats
            StatsAccountsComboBox.ItemsSource = kernel.Get<IUnitOfWork>().Repository<Currency>().Get().Select(x => x.Code).ToList<String>();
            StatsAccountsComboBox.DropDownClosed += new System.EventHandler(LoadCharts);
            LabelsIncome = new List<string>();
            LabelsExpence = new List<string>();
            LoadDefaultGraphs();
            IncomeChart.Zoom = ZoomingOptions.X;
            ExpenceChart.Zoom = ZoomingOptions.X;
        }

        private void UpdateAccountsView()
        {
            AccSPanel.Children.Clear();
            AccSPanel2.Children.Clear();
            AccSPanel3.Children.Clear();
            List<BankAccount> accounts = kernel.Get<IBankAccountService>().GetAllUserAccounts().ToList<BankAccount>();
            if (accounts.Count == 0)
            {
                return;
            }
            foreach (BankAccount account in accounts)
            {
                if (account.Type == AccountType.Income)
                {
                    AccountControl control = new AccountControl(kernel, account);
                    control.PropertyChanged += RefreshView;
                    AccSPanel.Children.Add(control);
                }
                if (account.Type == AccountType.Current)
                {
                    AccountControl control = new AccountControl(kernel, account);
                    control.PropertyChanged += RefreshView;
                    AccSPanel2.Children.Add(control);
                }
                if (account.Type == AccountType.Expence)
                {
                    AccountControl control = new AccountControl(kernel, account);
                    control.PropertyChanged += RefreshView;
                    AccSPanel3.Children.Add(control);
                }
            }
        }

        private void SettingsChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Change password button clicked");
            Change_Password CnahgePassword = new Change_Password(kernel);
            CnahgePassword.Show();
        }

        private void SettingsChangeEmailButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Change email button clicked");
            ChangeEmail CnahgeEmail = new ChangeEmail(kernel);
            CnahgeEmail.Show();
        }

        private void SettingsChangePhoneNumberButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Change phone button clicked");
            ChangePhoneNumber ChangePhoneNumber = new ChangePhoneNumber(kernel);
            ChangePhoneNumber.Show();
        }

        private void SettingsLogOutButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Log out button clicked");
            MainWindow win1 = new MainWindow();
            win1.Show();
            SystemCommands.CloseWindow(this);
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Add account button clicked");
            AddAccount AddAccount = new AddAccount(kernel);
            AddAccount.PropertyChanged += RefreshView;
            AddAccount.Show();
        }

        private void RefreshView(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccountsView();
        }

        private void MainForm1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Height / this.Width != 0.5625)
            {
                this.Height = this.Width * 0.5625;
            }
        }

        private void TransactionConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            kernel.Get<ILog>().Info("Transaction confirm button clicked");
            var service = kernel.Get<IBankAccountService>();
            string FromName = TransactionsFromComboBox.Text;
            string ToName = TransactionsToComboBox.Text;
            if (FromName == "" || ToName == "" || !decimal.TryParse(TransactionsAmountTextBox.Text, out decimal amount))
            {
                MessageBox.Show("Fields can not be empty");
                return;
            }
            if (amount <= 0)
            {
                MessageBox.Show("Amount can not be negative or equal to 0");
                return;
            }
            DateTime dateTime = (DateTime)TransactionsDatePicker.SelectedDate;
            if (dateTime > DateTime.Today)
            {
                MessageBox.Show("Invalid date");
            }
            var all = service.GetAllUserAccounts();
            BankAccount from = all.FirstOrDefault(x => x.Name == TransactionsFromComboBox.Text);
            BankAccount to = all.FirstOrDefault(x => x.Name == TransactionsToComboBox.Text);
            if (from.Id == to.Id)
            {
                MessageBox.Show("Cannot make transaction on the same account");
                return;
            }
            try
            {
                service.MakeTransaction(from, to, amount, dateTime, "");
                MessageBox.Show("Done");
                UpdateAccountsView();
                kernel.Get<ILog>().Info("Transaction ended");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                kernel.Get<ILog>().Info("Transaction failed " + exc.Message);
            }
        }

        private void TransactionTypeChanged(object sender, System.EventArgs e)
        {
            var service = kernel.Get<IBankAccountService>();
            var all = service.GetAllUserAccounts();
            if (TransactionsTypeComboBox.Text == "Income")
            {
                TransactionsToComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Current).Select(x => x.Name).ToList<string>();
                TransactionsFromComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Income).Select(x => x.Name).ToList<string>();
            }
            else if (TransactionsTypeComboBox.Text == "Expences") 
            {
                TransactionsToComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Expence).Select(x => x.Name).ToList<string>();
                TransactionsFromComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Current).Select(x => x.Name).ToList<string>();
            }
            else if (TransactionsTypeComboBox.Text == "Current")
            {
                TransactionsToComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Current).Select(x => x.Name).ToList<string>();
                TransactionsFromComboBox.ItemsSource = all.Where(x => x.Type == AccountType.Current).Select(x => x.Name).ToList<string>();
            }
        }
        //----Stats----
        private void LoadDefaultGraphs()
        {
            ExpenceChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    Stroke=Brushes.PaleVioletRed,
                    Fill = Brushes.Transparent,
                    StrokeThickness=3

                }
            };
            IncomeChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    Stroke=Brushes.LightSeaGreen,
                    Fill = Brushes.Transparent,
                    StrokeThickness=3
                }
            };
        }
        private void LoadCharts(object sender, System.EventArgs e)
        {
            if (StatsAccountsComboBox.Text != "")
            {
                LoadIncomeChart();
                LoadExpenceChart();
                LoadPieChart();
            }
        }
        private void LoadIncomeChart()
        {
           
            IStatisticsService statsService = kernel.Get<IStatisticsService>();
            var statItemsIncome = statsService.GetIncomeStatisticsFullPeriod(StatsAccountsComboBox.Text).ToList<StatisticsItem>();
            ChartValues<decimal> chartIncome = new ChartValues<decimal>();
            statItemsIncome.ForEach(x => { chartIncome.Add(x.Value);
                                           LabelsIncome.Add(x.Date.ToString());
                                         }) ;
            IncomeChart.Series = new SeriesCollection{
                new LineSeries{
                                    Title = "Income",
                                    Values = chartIncome,
                                    Stroke = Brushes.LightSeaGreen,
                                    Fill = Brushes.Transparent,
                                    StrokeThickness=3
                    }};
            DataContext = this;
        }
        private void LoadExpenceChart()
        {
            IStatisticsService statsService = kernel.Get<IStatisticsService>();
            var statsItemsExpence = statsService.GetExpenceStatisticsFullPeriod(StatsAccountsComboBox.Text).ToList<StatisticsItem>();
            ChartValues<decimal> chartExpence = new ChartValues<decimal>();
            statsItemsExpence.ForEach(x => {  chartExpence.Add(x.Value);
                                              LabelsExpence.Add(x.Date.ToString());
                                           });
            ExpenceChart.Series = new SeriesCollection{
                new LineSeries{
                                Title = "Expence",
                                Values = chartExpence,
                                Stroke = Brushes.PaleVioletRed,
                                Fill = Brushes.Transparent,
                                StrokeThickness=3
                }};
            DataContext = this;
        }
        private void LoadPieChart()
        {
            IStatisticsService statsService = kernel.Get<IStatisticsService>();
            var stats = statsService.GetIncomeStatisticsFullPeriod(StatsAccountsComboBox.Text).ToList<StatisticsItem>();
            var stats2 = statsService.GetExpenceStatisticsFullPeriod(StatsAccountsComboBox.Text).ToList<StatisticsItem>();
            ChartValues<decimal> chartIncome = new ChartValues<decimal>();
            ChartValues<decimal> chartExpence = new ChartValues<decimal>();
            decimal sumIncome = 0;
            decimal sumExpence = 0;
            stats.ForEach(x => { chartIncome.Add(x.Value);
                                 sumIncome += x.Value;
                               });
            stats2.ForEach(x => { chartExpence.Add(x.Value);
                                  sumExpence += x.Value;
                                });
            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            pieChart.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Income",
                    Values = new ChartValues<decimal> {sumIncome},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill=Brushes.LightSeaGreen,
                    FontSize=17
                },
                new PieSeries
                {
                    Title = "Expence",
                    Values = new ChartValues<decimal> {sumExpence},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill=Brushes.PaleVioletRed,
                    FontSize=17
                }
            };
            DataContext = this;
        }
    }
}
