using BLL.Interfaces;
using DAL.Domain;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System;
using System.Runtime.InteropServices;

namespace BLL.Services
{
    /// <summary>
    /// User Service class
    /// Implement IUserService
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Contains regex to validate phone number
        /// </summary>
        private readonly Regex phoneRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
        
        /// <summary>
        /// Gets user service current user
        /// </summary>
        public User CurrentUser { get; private set; }

        /// <summary>
        /// Contains an object of IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="email">select email</param>
        /// <returns>if email is valid</returns>
        public bool IsValidMail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">unit of work</param>
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.CurrentUser = null;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="newMail">new email</param>
        /// <returns>if email changed</returns>
        public bool ChangeMail(string newMail)
        {
            bool changed = false;
            var emails = this.unitOfWork.Repository<User>().Get().Select(x => x.Mail);
            if (!emails.Contains(newMail) && this.IsValidMail(newMail))
            {
                this.CurrentUser.Mail = newMail;
                this.unitOfWork.Repository<User>().Update(this.CurrentUser);
                this.unitOfWork.Save();
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="oldPassword">password to change</param>
        /// <param name="newPassword">new password</param>
        /// <returns>if password changed</returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            bool changed = false;
            string currentPassword = this.CurrentUser.Password;
            bool validPassword = BCrypt.Net.BCrypt.Verify(oldPassword, currentPassword);
            if (validPassword)
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                this.CurrentUser.Password = hashPassword;
                this.unitOfWork.Repository<User>().Update(this.CurrentUser);
                this.unitOfWork.Save();
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="number">new phone number</param>
        /// <returns>if phone number changed</returns>
        public bool ChangePhoneNumber(string number)
        {
            bool changed = false;
            var numbers = this.unitOfWork.Repository<User>().Get().Select(x => x.Phone);
            if (this.phoneRegex.IsMatch(number) && !numbers.Contains(number))
            {
                this.CurrentUser.Phone = number;
                this.unitOfWork.Repository<User>().Update(this.CurrentUser);
                this.unitOfWork.Save();
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <returns>logged-in user</returns>
        public User Login(string email, string password)
        {
            User user = this.unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                this.CurrentUser = user;
            }
            else
            {
                throw new ArgumentException("The email or password is incorrect.");
            }

            return this.CurrentUser;
        }

        /// <summary>
        /// Implementation of IUserService
        /// </summary>
        /// <param name="firstName">user name</param>
        /// <param name="lastName">user last name</param>
        /// <param name="email">user email</param>
        /// <param name="phoneNumber">user phone number</param>
        /// <param name="password">user password</param>
        /// <returns> registered user</returns>
        public User SignUp(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            var existUser = this.unitOfWork.Repository<User>().Get().FirstOrDefault(x => x.Mail == email || x.Phone == phoneNumber);
            if (existUser == null && this.phoneRegex.IsMatch(phoneNumber) && this.IsValidMail(email))
            {
                User user = new User
                {
                    Name = firstName,
                    Surname = lastName,
                    Mail = email,
                    Phone = phoneNumber,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    BankAccounts = new List<UserBankAccount>()
                };
                this.unitOfWork.Repository<User>().Update(user);
                this.unitOfWork.Save();
                this.CurrentUser = user;
            }
            else
            {
                throw new ArgumentException("Phone or mail incorrect");
            }

            return this.CurrentUser;
        }
    }
}