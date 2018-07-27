using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class AcceptFriendCommand : ICommand
    {
        private const int DataLength = 3;

        private readonly IUserService userService;
        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {

            var firstUsername = data[0];
            var secondUsername = data[1];

            if (!this.userService.Exists(firstUsername))
            {
                throw new ArgumentException($"User {firstUsername} not found!");
            }
            else if (!this.userService.Exists(secondUsername))
            {
                throw new ArgumentException($"User {secondUsername} not found!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(firstUsername);
            var friend = this.userService.ByUsername<UserFriendsDto>(secondUsername);

            if (user.Friends.Any(x => x.Username == secondUsername) && friend.Friends.Any(x => x.Username == firstUsername))
            {
                throw new InvalidOperationException($"{firstUsername} is already a friend to {secondUsername}");
            }


            this.userService.AcceptFriend(user.Id, friend.Id);

            user.Friends.Add(new FriendDto { Username = friend.Username });
            friend.Friends.Add(new FriendDto { Username = user.Username });

            return $"Friend {friend.Username} added to {user.Username}";
        }
    }
}
