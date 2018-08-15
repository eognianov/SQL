namespace TeamBuilder.App.Core.Commands
{
    using System;

    using Models;
    using Data;

    using Utilities;
    using System.Linq;

    public class LoginCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            string username = args[0];
            string password = args[1];

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            User user = this.GetUserByCredentials(username, password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            AuthenticationManager.LogIn(user);

            return $"User {user.Username} successfully loged in!";
        }

        private User GetUserByCredentials(string username, string password)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsDeleted == false);

                return user;
            }
        }
    }
}
