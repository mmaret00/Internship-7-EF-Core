using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Models
{
    public class UserEntry
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int EntryId { get; set; }
        public Entry Entry { get; set; }

        public bool Viewed { get; set; }
        public bool Upvoted { get; set; }
    }
}
