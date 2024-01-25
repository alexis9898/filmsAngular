using BLL.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICommentService
    {
        Task<List<CommentModel>> GetComments();
        Task<List<CommentModel>> GetCommentsByFilter(CommentModel commentModel);
        Task<CommentModel> GetComment(int commentId);
        Task<CommentModel> AddComment(CommentModel commentModel);
        Task<CommentModel> UpdateComment(int commentId,CommentModel commentModel);
        Task<CommentModel> UpdatePatchComment(int commentId, JsonPatchDocument commentModel);
        Task<bool> DeleteComment(int commentId);
        //Task<List<Comment>> GetCommentsBy(int deliveryId, string userId);
    }
}
