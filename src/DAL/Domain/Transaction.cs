using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Domain
{
    /// <summary>
    /// Transaction class
    /// contains fields Bank account, amount, transaction date, description, user id, user
    /// </summary>
    public class Transaction : BaseEntity
    {
        /// <summary>
        /// Gets or sets bank account from Transaction
        /// </summary>
        public virtual BankAccount BankAccountFrom { get; set; }

        /// <summary>
        /// Gets or sets bank account to Transaction
        /// </summary>
        public virtual BankAccount BankAccountTo { get; set; }

        /// <summary>
        /// Gets or sets amount to Transaction
        /// </summary>
        [Required]
        public decimal AmountTo { get; set; }

        /// <summary>
        /// Gets or sets amount From Transaction
        /// </summary>
        [Required]
        public decimal AmountFrom { get; set; }

        /// <summary>
        /// Gets or sets transaction Date
        /// </summary>
        [Required]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets transaction Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets transaction UserId
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets transaction User
        /// </summary>
        public virtual User User { get; set; }
    }
}
