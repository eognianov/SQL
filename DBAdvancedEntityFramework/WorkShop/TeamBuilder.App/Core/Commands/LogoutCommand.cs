namespace TeamBuilder.App.Core.Commands
{
    using Utilities;

    using Models;

    public class LogoutCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.LogOut();

            return $"User {currentUser.Username} successfully loged out!";
        }
    }
}
