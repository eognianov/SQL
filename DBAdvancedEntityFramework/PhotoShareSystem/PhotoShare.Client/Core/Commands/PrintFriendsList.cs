using System;
using System.Collections.Generic;
using System.Linq;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class PrintFriendsListCommand:ICommand
    {
        private readonly IUserService userService;

        public PrintFriendsListCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.Exists(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var user = this.userService.ByUsername<User>(username);





            List<string> friends = user.FriendsAdded.Select(x=>x.Friend.Username).ToList(); // usernames only

            if (friends.Count == 0)
                return "No friends for this user.:(";

            return $"Friends:\n-{String.Join("\n-", friends)}";
        }
    }
}
