using Microsoft.AspNetCore.Identity;

namespace ModelsLibrary.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
    }
}
