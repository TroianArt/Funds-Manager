using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Domain;
using DAL.Enums;

namespace BLL.Interfaces
{
    /// <summary>
    /// The Bank Account Service interface
    /// Contains methods to CreateAccount, DeleteAccount, ShareAccount,
    /// GetAllUserTransaction, GetUserAccounts, GetAccountScore
    /// </summary>
    public interface IBankAccountService
    {
        /// <summary>
        /// Gets or sets bank account service current user
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// method of IBankAccountService interface
        /// </summary>
        /// <param name="account">The Bank account</param>
        /// <returns>account score</returns>
        decimal GetAccountScore(BankAccount account);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <returns>all user transaction</returns>
        IEnumerable<Transaction> GetAllUserTransactions();

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="fromAccount">user transaction from this account</param>
        /// <returns>user transaction from account</returns>
        IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="toAccount">The bank account that get user transaction</param>
        /// <returns>user transaction to account</returns>
        IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="dateFrom">transaction from date</param>
        /// <param name="dateTo">transaction to date</param>
        /// <returns>user transaction from dateFrom to dateTo</returns>
        IEnumerable<Transaction> GetAllUserTransactions(DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="fromAccount">user transaction from this account</param>
        /// <param name="dateFrom">user transaction from date</param>
        /// <param name="dateTo">user transaction to date</param>
        /// <returns>user transaction account from dateFrom to dateTo </returns>
        IEnumerable<Transaction> GetAllUserTransactionsFrom(BankAccount fromAccount, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="toAccount">The bank account that get user transaction</param>
        /// <param name="dateFrom">user transaction from date</param>
        /// <param name="dateTo">user transaction to date</param>
        /// <returns>user transaction to account from dateFrom to dateTo</returns>
        IEnumerable<Transaction> GetAllUserTransactionsTo(BankAccount toAccount, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <returns>user accounts</returns>
        IEnumerable<BankAccount> GetAllUserAccounts();

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="from">The Bank account that send transaction</param>
        /// <param name="to">The bank account that get transaction</param>
        /// <param name="amount">amount of money</param>
        /// <param name="date">Date creation of transaction</param>
        /// <param name="description">description to transaction</param>
        /// <returns> transaction if done </returns>
        Task<Transaction> MakeTransaction(BankAccount from, BankAccount to, decimal amount, DateTime date, string description);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="account">The bank account to share</param>
        /// <param name="email">Email to share account</param>
        /// <returns>if account shared</returns>
        bool ShareAccount(BankAccount account, string email);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="type">Select account type</param>
        /// <param name="name">name of account</param>
        /// <param name="currency">select currency</param>
        /// <returns>created account</returns>
        BankAccount CreateAccount(AccountType type, string name, Currency currency);

        /// <summary>
        /// method of IBankAccountService
        /// </summary>
        /// <param name="account">The bank account to delete</param>
        /// <returns>if account deleted</returns>
        void DeleteAccount(BankAccount account);
    }
}