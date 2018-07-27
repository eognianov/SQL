using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class TagService:ITagService
    {
        private readonly PhotoShareContext context;

        public TagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => By<TModel>(i => i.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name) => By<TModel>(i => i.Name == name).SingleOrDefault();

        public bool Exists(int id) => ById<Tag>(id) != null;

        public bool Exists(string name) => ByName<Tag>(name) != null;

        public Tag AddTag(string name)
        {
            var tag = new Tag {Name = name};
            this.context.Tags.Add(tag);
            this.context.SaveChanges();
            return tag;
        }

        private IEnumerable<TModel> By<TModel>(Func<Tag, bool> predicate) =>
            this.context.Tags.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}
