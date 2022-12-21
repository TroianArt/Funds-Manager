using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using DAL.Domain;
using DAL.Enums;

namespace BLL.Interfaces
{
    /// <summary>
    /// Statistics Service interface
    /// Contains method to GetExpenceStatistic, getIncomeStatistic
    /// GetExpenseStatisticFullPerion, getIncomeStatisticFullPeriod
    /// </summary>
    public interface IStatisticsService
    {
        /// <summary>
        /// Gets or sets statistics service current user
        /// </summary>
        User CurrentUser { get; set; }

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <param name="fromDate">expenses statistic from date</param>
        /// <param name="toDate">expenses statistic to date</param>
        /// <param name="currency">currency</param>
        /// <returns>statistic from fromDate to toDate</returns>
        public IEnumerable<StatisticsItem> GetExpenceStatistics(string currency,DateTime fromDate, DateTime toDate);

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <param name="fromDate">income statistic from date</param>
        /// <param name="toDate">income statistic to date</param>
        /// <param name="currency">currency</param>
        /// <returns>income statistic from fromDate to toDate</returns>
        public IEnumerable<StatisticsItem> GetIncomeStatistics(string currency,DateTime fromDate, DateTime toDate);

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <returns>expenses statistic for the whole period</returns>
        public IEnumerable<StatisticsItem> GetExpenceStatisticsFullPeriod(string currency);

        /// <summary>
        /// method of IStatisticService
        /// </summary>
        /// <returns>income statistic for the whole period</returns>
        public IEnumerable<StatisticsItem> GetIncomeStatisticsFullPeriod(string currency);
    }
}
