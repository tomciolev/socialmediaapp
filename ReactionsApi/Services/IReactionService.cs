using ModelsLibrary.Dto;

namespace ReactionsApi.Services
{
    public interface IReactionService
    {
        Task<ReactionDto> Create(string postId, CreateReactionDto reactionDto);
        //Task<IEnumerable<ReactionDto>> GetReactionsForPostAsync(string postId);
        Task<bool> Remove(string reactionId);
        Task<ReactionDto> GetReactionById(string reactionId);
    }

}
