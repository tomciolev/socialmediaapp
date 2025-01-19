using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Dto;
using ModelsLibrary.Models;
using ModelsLibrary.Services;
using System.Diagnostics;

namespace PostApi.Services
{
    public class PostService : IPostService
    {
        private readonly SocialMediaDbContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public PostService(SocialMediaDbContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _context = context;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        public async Task<PostResponseDto> Create(CreatePostDto postDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = postDto.Title,
                Content = postDto.Content,
                ImageUrl = postDto.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            var createdPost = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Reactions)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.Id == post.Id);

            return _mapper.Map<PostResponseDto>(createdPost);
        }
        public async Task Update(string id, CreatePostDto postDto)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if (currentUser == null || post.UserId != currentUser.Id)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this post");
            }

            post.Title = postDto.Title ?? post.Title;
            post.Content = postDto.Content ?? post.Content;
            post.ImageUrl = postDto.ImageUrl ?? post.ImageUrl;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                throw new Exception("Post not found");
            }

            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
            if (currentUser == null || post.UserId != currentUser.Id)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this post");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostResponseDto>> GetAll()
        {
            return await _context.Posts
           .Include(p => p.User)
           .Include(p => p.Reactions)
               .ThenInclude(r => r.User)
           .OrderByDescending(p => p.CreatedAt)
           .ProjectTo<PostResponseDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<Post> GetById(string id)
        {
            var post = await _context.Posts
                //.Include(p => p.Reactions)
                //.ThenInclude(r => r.User) // Pobieramy reakcje wraz z użytkownikami
                .FirstOrDefaultAsync(post => post.Id == id);

            return post == null ? throw new Exception($"Activity with ID {id} was not found.") : post;
        }
    }
}
