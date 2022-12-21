namespace DAL.Domain
{
    /// <summary>
    /// User Bank Account class
    /// Contains fields user Id, user, bank account Id, bank account
    /// </summary>
    public class UserBankAccount
    {
        /// <summary>
        /// Gets or sets user bank account user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets user bank account user
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets user bank account bank account id
        /// </summary>
        public int BankAccountId { get; set; }

        /// <summary>
        /// Gets or sets user bank account bank account
        /// </summary>
        public virtual BankAccount BankAccount { get; set; }
    }
} 
