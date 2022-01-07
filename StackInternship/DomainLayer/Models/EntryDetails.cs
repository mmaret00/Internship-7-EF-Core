using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class EntryDetails
    {
        public int Id { get; set; }
        public string AuthorUserName { get; set; }
        public UserRole AuthorsRole { get; set; }
        public EntryDepartmentChoice Department { get; set; }
        public DateTime Published { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int CommentCount { get; set; }
    }
}
