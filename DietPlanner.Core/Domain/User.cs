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
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public Plan Plan { get; protected set; }
        public IEnumerable<WeightPoint> WeightPoints { get; protected set; }

        protected User()
        {
        }

        protected User(Guid userId, string username, string email, string role, string password, string salt)
        {
            UserId = userId;
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            SetRole(role);
        }

        protected void SetPassword(string password)
        {

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

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception("Role cannot be empty.");
            }
            if (Role == role)
            {
                return;
            }
            Role = role;
            UpdatedAt = DateTime.UtcNow;
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

        public static User Create(Guid userId, string username, string email, string role, string password, string salt)
            => new User(userId, username, email, role, password, salt);
    }
}
