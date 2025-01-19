using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Dto;
using ModelsLibrary.Models;
using PostApi.Services;
using System.Diagnostics;

namespace PostApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        //[AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _postService.GetAll();
            return Ok(posts);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Post), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<Post>> GetById(string id)
        {
            try
            {
                var post = await _postService.GetById(id);
                return Ok(post);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(Post), 201)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create([FromBody] CreatePostDto postDto)
        {
            await _postService.Create(postDto);
            return CreatedAtAction(nameof(GetById), new { id = postDto.Title }, postDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(string id, [FromBody] CreatePostDto postDto)
        {
            try
            {
                await _postService.Update(id, postDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _postService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
