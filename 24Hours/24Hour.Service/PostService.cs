﻿using _24Hour.Data;
using _24Hour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24Hour.Service
{
    public class PostService
    {

        private readonly Guid _userId;
        public PostService(Guid userId)
        {
            _userId = userId;
        }




        public bool CreatePost(PostCreate model)
        {

            var entity = new Post()
            {
                OwnerId = _userId,
                Title = model.Title,
                Text = model.Text
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() >= 1;
            }

        }

        public IEnumerable<PostListItem> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Posts.Where(e => e.OwnerId == _userId).Select(e => new PostListItem { PostId = e.NoteId, Title = e.Title });
                return query.ToArray();
            }
        }


    }
}
