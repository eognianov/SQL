namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;

    using Models.Enums;
    using Utilities;
    using Data;
    using Models;

    public class RegisterUserCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(7, args);

            string username = args[0];

            if (username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            string password = args[1];

            if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            string repeatPassword = args[2];

            if (password != repeatPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            string firstName = args[3];
            string lastName = args[4];

            int age;

            bool isNumber = int.TryParse(args[5], out age);

            if (!isNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;

            bool isGenderValid = Enum.TryParse(args[6], out gender);

            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            this.RegisterUser(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully!";
        }

        private void RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = new User
                {
                    Username = username,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender,
                    Password = password
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
