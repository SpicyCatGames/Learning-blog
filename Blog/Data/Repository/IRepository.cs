using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        Task<List<Post>> GetAllPosts();
        /// <param name="pageNumber">index starts at 1</param>
        //Task<IndexViewModel> GetAllPosts(int pageNumber);
        Task<IndexViewModel> GetAllPosts(int pageNumber, string category);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);

        void AddSubComment(SubComment subComment);

        Task<bool> SaveChangesAsync();
    }
}