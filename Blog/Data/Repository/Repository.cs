using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _ctx.Posts.ToListAsync();
        }

        public async Task<List<Post>> GetAllPosts(string category)
        {
            return await _ctx.Posts
                .Where(post => post.Category.Equals(category))
                .ToListAsync();
            // This words but is case insensitive

            // TODO asenumerable or await linq.tolistasync or expression<func<>> for client side eval

            //Expression<Func<Post, bool>> InCategoryExpr = post => post.Category.Equals(category);
            //return await _ctx.Posts
            //    .Where(InCategoryExpr)
            //    .ToListAsync();
            // This works but is case insensitive
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}