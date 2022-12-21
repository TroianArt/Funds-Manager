using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;

namespace DAL.Domain
{
    /// <summary>
    /// Bank Account class
    /// Contains fields name, account type, currency type, currency id, list of users
    /// </summary>
    public class BankAccount : BaseEntity
    {
        /// <summary>
        /// Gets or sets bank account name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets bank account type 
        /// </summary>
        [Required]
        public AccountType Type { get; set; }

        /// <summary>
        /// Gets or sets bank account type id
        /// </summary>
        [Required]
        [ForeignKey("CurrencyType")]
        public int CurrencyTypeId { get; set; }

        /// <summary>
        /// Gets or sets bank account currency type 
        /// </summary>
        [Required]
        public virtual Currency CurrencyType { get; set; }

        /// <summary>
        /// Gets or sets bank account list of Users
        /// </summary>
        public virtual List<UserBankAccount> Users { get; set; }
    }
}
