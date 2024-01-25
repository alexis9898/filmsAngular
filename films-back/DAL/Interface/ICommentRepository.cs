using DAL.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetComments();
        Task<List<Comment>> GetCommentsByFilter(Comment comment);
        Task<Comment> GetComment(int commentId);
        Task<Comment> AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);
        //Task<List<Comment>> GetCommentsBy(int deliveryId, string userId);
    }
}
