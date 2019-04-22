using DietPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Height { get; protected set; }
        public string Sex { get; protected set; }
        public DateTime Birthday { get; protected set; }
        public int Age => GetAge();

        public ISet<WeightPoint> WeightPoints { get; set; }

        public Plan Plan { get; set; }

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

        public void SetStats(int height, string sex, DateTime birthday)
        {
            SetHeight(height);
            SetSex(sex);
            SetBirthday(birthday);
        }

        private void SetBirthday(DateTime birthday)
        {
            //Expected: result1 >=0
            int result1 = DateTime.Compare(birthday, DateTime.UtcNow.AddYears(-150));
            //Expected: result2 <=0
            int result2 = DateTime.Compare(birthday, DateTime.UtcNow.AddYears(-12));
            if (result1 >=0 && result2 <=0)
            {
                Birthday = birthday;
            }
            return;
        }

        private void SetSex(string sex)
        {
            if (sex.ToLowerInvariant() == "male")
            {
                Sex = "Male";
                return;
            }
            if (sex.ToLowerInvariant() == "female")
            {
                Sex = "Female";
                return;
            }
            Sex = "Unknown";
        }

        private void SetHeight(int height)
        {
            if (height<50 || height > 300)
            {
                throw new Exception("User's height must be between 50 and 300 centimeters");
            }
            Height = height;
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
            if (username.Length > 100)
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

        public int GetAge()
        {
            return (DateTime.Now - Birthday).Days;
        }

        public static User Create(Guid userId, string username, string email, string role, string password, string salt)
            => new User(userId, username, email, role, password, salt);

        public void Update(string username, string email, string role, string password, string salt, string height, string sex, DateTime birthday)
        {
            if (username!=null) this.SetUsername(username);
            if (email!=null) this.SetEmail(email);
            if (role!=null) this.SetRole(role);
            if (password != null) this.SetPassword(password);
            if (height != null) this.SetHeight(Int32.Parse(height));
            if (sex != null) this.SetSex(sex);
            if (birthday != null) this.SetBirthday(birthday);
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
