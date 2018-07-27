using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Utilities;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagCommand : ICommand
    {
        private readonly ITagService tagService;

        public AddTagCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public string Execute(string[] args)
        {
            string tagName = args[0];

            var exist = this.tagService.Exists(tagName);

            if (exist)
            {
                throw new ArgumentException($"Tag {tagName} exist!");
            }

            tagName = tagName.ValidateOrTransform();

            this.tagService.AddTag(tagName);

            return $"Tag {tagName} was added successfully!";
        }
    }
}
