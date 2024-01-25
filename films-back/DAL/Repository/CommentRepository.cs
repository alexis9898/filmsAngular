using DAL.Data;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly AppDataContext _context;
        public CommentRepository(AppDataContext context)
        {
            _context = context;
        }

        //get all
        public async Task<List<Comment>> GetComments()
        {
            var comments = await _context.Comments
                .Include(x=>x.User)
                .ToListAsync();
            return comments;
        }

        //get one by ID
        public async Task<Comment> GetComment(int commentId)
        {
            var comment = await _context.Comments.Where(c=>c.Id==commentId).FirstOrDefaultAsync();
            return comment;
        }

        //get all by costumerId and userid   //u have option with filter function
        //public async Task<List<Comment>> GetCommentsBy(int deliveryId, string userId)
        //{
        //    var comments = await _context.Comments.Where(c => c.DeliveryId == deliveryId && c.UserId==userId).ToListAsync();
        //    return comments;
        //}

        //filter
        public async Task<List<Comment>> GetCommentsByFilter(Comment comment)
        {
            var comments = await _context.Comments.Where(c =>
                     comment.FilmId != 0 ? c.FilmId == comment.FilmId: c.FilmId!= 0
                   // && comment.UserId!=null?c.UserId == comment.UserId:c.UserId!=null

                )
                .ToListAsync();

            return comments;
        }

        //post
        public async Task<Comment> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        // put/patch
        public async Task UpdateComment(Comment comment)
        {
            _context.Update(comment);
            await _context.SaveChangesAsync();
            return;
        }

        //delete
        public async Task DeleteComment(Comment comment)
        {
            _context.Remove(comment);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
