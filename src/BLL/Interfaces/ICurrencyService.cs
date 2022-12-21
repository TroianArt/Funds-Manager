using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    /// <summary>
    /// The Currency Service interface
    /// Contains method GetRate, GetRatByDate and UpdateCurrency
    /// </summary>
    public interface ICurrencyService
    { 
        /// <summary>
        /// method of ICurrencyService
        /// Update Currency
        /// </summary>
        void UpdateCurrency();

        /// <summary>
        /// method of ICurrencyService
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <returns>currency rate</returns>
        decimal GetRate(string code);

        /// <summary>
        /// method of ICurrencyService
        /// </summary>
        /// <param name="code">Code of currency</param>
        /// <param name="date">Wanted date</param>
        /// <returns>Currency rate on the date</returns>
        decimal GetRateByDate(string code, DateTime date);
    }
}