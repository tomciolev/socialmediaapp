using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Dto;
using ReactionsApi.Services;

namespace ReactionsApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _reactionService;

        public ReactionController(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> Create(string postId, [FromBody] CreateReactionDto createReactionDto)
        {
            try
            {
                var reaction = await _reactionService.Create(postId, createReactionDto);
                return CreatedAtAction(nameof(GetReactionById), new { reaction.Id }, reaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReaction(string id)
        {
            var result = await _reactionService.Remove(id);
            if (result)
            {
                return Ok("Reaction removed successfully");
            }
            return NotFound("Reaction not found");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactionById(string id)
        {
            try
            {
                var reaction = await _reactionService.GetReactionById(id);
                return Ok(reaction);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
