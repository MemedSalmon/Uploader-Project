using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uploader.Data;
using Uploader.Models;

namespace Uploader.Services
{
    public class PostService
    {
        public IQueryable<Post> Posts { get; set; }

        public AppDbContext Db { get; set; }

        public PostService(AppDbContext db)
        {
            Db = db;
            Posts = from Post in Db.Posts
                    select Post;
        }

        public Task<string> GeneratePostIdAsync(IQueryable<string> ids)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] tempStr = new char[20];
            bool match = false;
            Random rand = new Random();

            while (true)
            {
                for (int i = 0; i < tempStr.Length; i++)
                {
                    tempStr[i] = chars[rand.Next(chars.Length)];

                }

                foreach (string id in ids)
                {
                    if (id == new string(tempStr))
                    {
                        match = true;
                        break;
                    }
                }

                if (!match)
                    break;
            }

            return Task.Run(() => new string(tempStr));
        }

        public Task<Post> GeneratePostAsync(string id, string title, IFormFile file, AccountUser user)
        {
            Post post = new Post
            {
                Id = id,
                Title = title,
                Type = (file.ContentType.Split("/"))[0],
                Extension = Path.GetExtension(file.FileName).Split(".")[1],
                AccountUser = user
            };


            return Task.Run(() => post);

        }

        public Task<bool> RemovePostAsync(Post post)
        {
            Db.Posts.Remove(post);

            if (Db.SaveChanges() >= 0)
                return Task.Run(() => true);

            return Task.Run(() => false);

        }
    }
}
