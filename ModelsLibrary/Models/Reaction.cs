using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    public class Reaction
    {
        public string Id { get; set; }
        public string Emoji { get; set; }
        public DateTime CreatedAt { get; set; }

        public string PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
