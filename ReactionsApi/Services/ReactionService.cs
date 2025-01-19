using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Dto;
using ModelsLibrary.Migrations;
using ModelsLibrary.Models;
using ModelsLibrary.Services;

namespace ReactionsApi.Services
{
    public class ReactionService : IReactionService
    {
        private readonly SocialMediaDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public ReactionService(SocialMediaDbContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _context = context;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }

        public async Task<ReactionDto> Create(string postId,CreateReactionDto reactionDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var reaction = new Reaction
            {
                Id = Guid.NewGuid().ToString(),
                PostId = postId,
                UserId = user.Id,
                Emoji = reactionDto.Emoji,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reactions.Add(reaction);
            await _context.SaveChangesAsync();

            var createdReaction = await _context.Reactions
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reaction.Id);

            return _mapper.Map<ReactionDto>(createdReaction);
        }
        public async Task<ReactionDto> Update(string reactionId, string emoji)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

            var reaction = await _context.Reactions
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reactionId && r.UserId == user.Id);

            if (reaction == null)
                throw new Exception("Reaction not found or you're not the owner");

            reaction.Emoji = emoji;
            await _context.SaveChangesAsync();

            return _mapper.Map<ReactionDto>(reaction);
        }


        //public async Task<IEnumerable<ReactionDto>> GetReactionsForPostAsync(string postId)
        //{
        //    return await _context.Reactions
        //        .Where(r => r.PostId == postId)
        //        .Select(r => new ReactionDto
        //        {
        //            Id = r.Id,
        //            PostId = r.PostId,
        //            UserId = r.UserId,
        //            Emoji = r.Emoji,
        //            CreatedAt = r.CreatedAt
        //        }).ToListAsync();
        //}

        public async Task<bool> Remove(string reactionId)
        {
            var reaction = await _context.Reactions.FindAsync(reactionId);
            if (reaction == null)
            {
                return false;
            }

            _context.Reactions.Remove(reaction);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<ReactionDto> GetReactionById(string reactionId)
        {
            var reaction = await _context.Reactions
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reactionId);

            if (reaction == null)
                throw new Exception("Reaction not found");

            return _mapper.Map<ReactionDto>(reaction);
        }
    }

}
