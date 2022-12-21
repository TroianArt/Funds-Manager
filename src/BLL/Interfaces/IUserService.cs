using System;
using DAL.Domain;
using DAL.Enums;

namespace BLL.Interfaces
{
    /// <summary>
    /// User service interface
    /// Contains method to Login, SignUp, ChangePassword,
    /// ChangeMail, ChangePhoneNumber, isValidMail
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets user service current user
        /// </summary>
        User CurrentUser { get; }

        /// <summary>
        /// method of IUserService
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <returns>logged-in user</returns>
        User Login(string email, string password);

        /// <summary>
        /// method of IUserService
        /// </summary>
        /// <param name="firstName">user name</param>
        /// <param name="lastName">user last name</param>
        /// <param name="email">user email</param>
        /// <param name="phoneNumber">user phone number</param>
        /// <param name="password">user password</param>
        /// <returns>registered user</returns>
        User SignUp(string firstName, string lastName, string email, string phoneNumber, string password);

        /// <summary>
        /// method of IUserService
        /// </summary>
        /// <param name="oldPassword">password to change</param>
        /// <param name="newPassword">new password</param>
        /// <returns>if password changed</returns>
        bool ChangePassword(string oldPassword, string newPassword);

        /// <summary>
        /// method of IUserService
        /// </summary>
        /// <param name="newMail">new email</param>
        /// <returns>if email changed</returns>
        bool ChangeMail(string newMail);

        /// <summary>
        /// method of IUserService
        /// </summary>
        /// <param name="number">new phone number</param>
        /// <returns>if phone number changed</returns>
        bool ChangePhoneNumber(string number);

        /// <summary>
        /// method of IUserService
        /// </summary>
        /// <param name="email">select email</param>
        /// <returns>if email valid</returns>
        bool IsValidMail(string email);
    }
}