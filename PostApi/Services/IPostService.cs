using ModelsLibrary.Dto;
using ModelsLibrary.Models;
using System.Diagnostics;

namespace PostApi.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponseDto>> GetAll();
        Task<Post> GetById(string id);
        Task Delete(string id);
        Task Update(string id, CreatePostDto postDto);
        Task<PostResponseDto> Create(CreatePostDto post);
    }
}
