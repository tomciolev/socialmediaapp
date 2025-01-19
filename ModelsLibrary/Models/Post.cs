using System;
using System.Collections.Generic;

namespace ModelsLibrary.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
    }
}
