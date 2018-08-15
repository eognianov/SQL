namespace TeamBuilder.App.Core
{
    using System;

    using Models;
    using Utilities;

    public static class AuthenticationManager
    {
        private static User currentUser;

        public static void LogIn(User user)
        {
            currentUser = user;
        }

        public static void LogOut()
        {
            currentUser = null;
        }

        public static void Authorize()
        {
            if (currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static bool IsAuthenticated()
        {
            if (currentUser != null)
            {
                return true;
            }

            return false;
        }

        public static User GetCurrentUser()
        {
            if (currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            return currentUser;
        }
    }
}
