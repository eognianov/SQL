using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;
        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var friendUsername = data[1];
            var userExists = this.userService.Exists(username);
            var friendExistrs = this.userService.Exists(friendUsername);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not exist!");
            }
            if (!friendExistrs)
            {
                throw new ArgumentException($"User {friendUsername} not exist!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(username);
            var friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            bool isSendRequestFromUser = user.Friends.Any(x => x.Username == friendUsername);
            bool isSendRequestFromFriend = friend.Friends.Any(x => x.Username == username);

            if (isSendRequestFromUser && isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"{friendUsername} and {username} are already friends!");
            }
            else if (isSendRequestFromUser && !isSendRequestFromFriend)
            {
                throw new InvalidOperationException($"Request is already sent!");
            }

            this.userService.AddFriend(user.Id, friend.Id);

            return $"Friend {friendUsername} added to {username}";
        }
    }
}
