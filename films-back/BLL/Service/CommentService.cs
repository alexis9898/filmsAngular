using AutoMapper;
using BLL.Interface;
using BLL.Model;
using DAL.Data;
using DAL.Interface;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        //private readonly IAccountService _accountService;

        public CommentService(IMapper mapper, ICommentRepository commentRepository) //IAccountService accountService)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
           // _accountService = accountService;
        }
        //get all
        public async Task<List<CommentModel>> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            var commentsModel = _mapper.Map<List<CommentModel>>(comments);
            //for (int i = 0; i < commentsModel.Count; i++)
            //    await Map(commentsModel[i]);
            return commentsModel;
        }

        //get one
        public async Task<CommentModel> GetComment(int commendId)
        {
            var comment = await _commentRepository.GetComment(commendId);
            var commentModel = _mapper.Map<CommentModel>(comment);
            //await Map(commentModel);
            return commentModel;
        }

        //filter
        public async Task<List<CommentModel>> GetCommentsByFilter(CommentModel commentModel)
        {
            Comment comment = _mapper.Map<Comment>(commentModel);
            List<Comment> comments = await _commentRepository.GetCommentsByFilter(comment);
            List<CommentModel> commentsModel = _mapper.Map<List<CommentModel>>(comments);
            for (int i = 0; i < commentsModel.Count; i++)
                await Map(commentsModel[i]);
            return commentsModel;
        }

        //add
        public async Task<CommentModel> AddComment(CommentModel commentModel)
        {
            var comment = await _commentRepository.AddComment(_mapper.Map<Comment>(commentModel));
            commentModel.Id = comment.Id;
            return commentModel;
        }

        //PUT
        public async Task<CommentModel> UpdateComment(int commentId, CommentModel commentModel)
        {
            var comment= await _commentRepository.GetComment(commentId);
            if(comment == null)
                return null;

            comment.Content = commentModel.Content;
            //comment.UserId = commentModel.UserId;
            comment.FilmId = commentModel.FilmId;

             await _commentRepository.AddComment(_mapper.Map<Comment>(commentModel));
            commentModel.Id = commentId;
            return commentModel;
        }

        //patch
        public async Task<CommentModel> UpdatePatchComment(int commentId, JsonPatchDocument commentModel)
        {
            var comment = await _commentRepository.GetComment(commentId);
            if (comment == null)
                return null;

            commentModel.ApplyTo(comment);
            await _commentRepository.UpdateComment(comment);
            return _mapper.Map<CommentModel>(comment);
        }

        //delete
        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await _commentRepository.GetComment(commentId);
            if (comment == null)
                return false;

            await _commentRepository.DeleteComment(comment);
            return true;

        }

        //map
        public async Task<CommentModel> Map(CommentModel commentModel)
        {
            //commentModel.User = await _accountService.FindUserById(commentModel.UserId);
            return commentModel;
        }

    }
}
