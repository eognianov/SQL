namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;

    using Commands;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string[] inputTokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputTokens[0];

            string[] commandArgs = inputTokens.Skip(1).ToArray();

            switch (commandName)
            {
                case "DeleteUser":
                    DeleteUserCommand delete = new DeleteUserCommand();
                    result = delete.Execute(commandArgs);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute(commandArgs);
                    break;
                case "Login":
                    LoginCommand login = new LoginCommand();
                    result = login.Execute(commandArgs);
                    break;
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand();
                    result = registerUser.Execute(commandArgs);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute(commandArgs);
                    break;
                default:
                    throw new NotSupportedException($"Command {commandName} is not supported!");
            }

            return result;
        }
    }
}
