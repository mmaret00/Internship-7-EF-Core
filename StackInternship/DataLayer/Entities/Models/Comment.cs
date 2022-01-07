using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Entities.Enums;

namespace DataLayer.Entities.Models
{
    public class Comment : Entry
    {
        public int ParentId { get; set; }

        public Comment(int authorId, int parentId, string content, ResourceDepartment department)
        {
            AuthorId = authorId;
            ParentId = parentId;
            Content = content;
            DateOfPublishing = DateTime.Now;
            Department = department;
        }

        public Comment() { }
    }
}
