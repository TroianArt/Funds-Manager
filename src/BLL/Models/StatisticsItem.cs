using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    /// <summary>
    /// Statistic Item class
    /// Contains field of dateTime and value
    /// </summary>
    public class StatisticsItem
    {

        /// <summary>
        /// Statistic item date
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Statistic item value
        /// </summary>
        public decimal Value { get; set; }
    }
}
         