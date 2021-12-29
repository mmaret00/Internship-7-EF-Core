using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Entities.Enums;

namespace DataLayer.Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public ICollection<Comment> Subcomments { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string Content { get; set; }
    }
}
