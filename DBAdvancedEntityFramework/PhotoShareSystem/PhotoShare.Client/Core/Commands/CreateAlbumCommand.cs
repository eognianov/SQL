using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Utilities;
using PhotoShare.Models.Enums;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;


    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            string bgColor = data[2];
            string[] tags = data.Skip(3).ToArray();

            var userExist = this.userService.Exists(username);
            if (!userExist)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var albumExist = this.albumService.Exists(albumTitle);

            if (albumExist)
            {
                throw new ArgumentException($"Album {albumTitle} exist!");
            }

            var isValidBgColor = Enum.TryParse(bgColor, out Color resultColor);

            if (!isValidBgColor)
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = tags[i].ValidateOrTransform();

                var currentTag = this.tagService.Exists(tags[i]);

                if (!currentTag)
                {
                    throw new ArgumentException("Invalid tags!");
                }
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;
            this.albumService.Create(userId, albumTitle, bgColor, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
