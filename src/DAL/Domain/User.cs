using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Domain
{
    /// <summary>
    /// User class
    /// Contains fields of user mail, user password, user name, user surname
    /// user phone
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets user mail
        /// </summary>
        [Required]
        public string Mail { get; set; }
        
        /// <summary>
        /// Gets or sets user password
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        /// <summary>
        /// Gets or sets user list of bank accounts
        /// </summary>
        public virtual List<UserBankAccount> BankAccounts { get; set; }
        
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets user surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Gets or sets user phone
        /// </summary>
        [Required]
        public string Phone { get; set; }
    }
}
