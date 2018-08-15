namespace TeamBuilder.App.Core.Commands
{
    using System;

    using Utilities;
    using Data;
    using Models;

    public class DeleteUserCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                currentUser.IsDeleted = true;

                context.Users.Update(currentUser);
                context.SaveChanges();

                AuthenticationManager.LogOut();
            }

            return $"User {currentUser.Username} was deleted successfully!";
        }
    }
}
