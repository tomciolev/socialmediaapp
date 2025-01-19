using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Dto
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
