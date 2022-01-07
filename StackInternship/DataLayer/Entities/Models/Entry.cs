using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public User Author { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime PublishedAt { get; set; }
        public EntryDepartmentChoice Department { get; set; }
        public string Content { get; set; }
        public int ParentId { get; set; }
        public EntryType TypeOfEntry { get; set; }

        public Entry(int authorId, EntryDepartmentChoice department, string content, int parentId, EntryType typeOfEntry)
        {
            AuthorId = authorId;
            PublishedAt = DateTime.Now;
            Department = department;
            Content = content;
            ParentId = parentId;
            TypeOfEntry = typeOfEntry;
        }

        public Entry() { }
    }
}
