using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Domain
{
    /// <summary>
    /// Currency class
    /// Contains field currency code
    /// </summary>
    public class Currency : BaseEntity
    {
        /// <summary>
        /// Gets or sets currency code 
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
