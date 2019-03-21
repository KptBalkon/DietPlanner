using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DietPlanner.Core.Domain
{
    public class User
    {
        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Plan Plan { get; protected set; }
        public IEnumerable<WeightPoint> WeightPoints { get; protected set; }

        protected User()
        {
        }

        protected User(string username, string email, string password, string salt)
        {
            UserId = Guid.NewGuid();
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        protected void SetPassword(string password)
        {
           if(password.Length<8)
            {
                throw new Exception("Password must be at least 8 characters long");
            }
            Password = password;
        }

        protected void SetUsername(string username)
        {
            if (username.Length <= 3)
            {
                throw new Exception("Username must be longer than 3 characters.");
            }
            if (username.Length >100)
            {
                throw new Exception("Username cannot be longer than 100 characters.");
            }
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                throw new Exception("Username can only contain letters and numbers.");
            }
            Username = username;
        }

        protected void SetEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

            }
            catch (FormatException)
            {
                throw new Exception("Please provide properly formatted Email.");
            }
            Email = email.ToLowerInvariant();
        }

        public void CreatePlan(int plannedWeight, DateTime targetDate)
        {
            Plan = Plan.Create(UserId, plannedWeight, targetDate);
    }

        public static User Create(string username, string email, string password, string salt)
            => new User(username, email, password, salt);
    }
}
