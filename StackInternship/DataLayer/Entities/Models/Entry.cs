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
        public int AuthorId { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public ResourceDepartment Department { get; set; }
        public string Content { get; set; }
        public ICollection<UserEntry> UserEntries { get; set; }
    }
}
