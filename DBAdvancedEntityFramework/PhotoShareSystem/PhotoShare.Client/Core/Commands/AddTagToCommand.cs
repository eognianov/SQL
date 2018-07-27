using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Utilities;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;
        private readonly IAlbumTagService albumTagService;

        public AddTagToCommand(IAlbumService albumService, ITagService tagService, IAlbumTagService albumTagService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
            this.albumTagService = albumTagService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] args)
        {
            string albumName = args[0];
            string tagName = args[1].ValidateOrTransform(); // validated

            if (!this.albumService.Exists(albumName) ||
                !this.tagService.Exists(tagName))
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            var album = this.albumService.ByName<Album>(albumName);
            var tag = this.tagService.ByName<AlbumTag>(tagName);



            if (album.AlbumTags.Contains(tag))
            { 
                throw new InvalidOperationException($"Tag {tagName} already added to album {albumName}!");
            }

            this.albumTagService.AddTagTo(album.Id, tag.TagId);

            return $"Tag {tagName} added to {albumName}";
        }
    }
}
