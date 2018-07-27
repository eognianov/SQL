using Microsoft.EntityFrameworkCore.Internal;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IAlbumService albumService;
        private readonly IAlbumRoleService albumRoleService;

        public ShareAlbumCommand(IUserService userService, IAlbumService albumService, IAlbumRoleService albumRoleService)
        {
            this.userService = userService;
            this.albumService = albumService;
            this.albumRoleService = albumRoleService;
        }
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            Role role;
            bool isValidRole = Enum.TryParse(permission, out role);


            if (!this.albumService.Exists(albumId))
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            if (!this.userService.Exists(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!isValidRole)
            {
                throw new AggregateException($"Permission must be either \"Owner\" or \"Viewer\"!");
            }

            UserDto userDto = userService.ByUsername<UserDto>(username);
            AlbumDto albumDto = albumService.ById<AlbumDto>(albumId);

            albumRoleService.PublishAlbumRole(albumId, userDto.Id, permission);

            return $"Username {username} added to album {albumDto.Name} ({permission})";
        }
    }
}
