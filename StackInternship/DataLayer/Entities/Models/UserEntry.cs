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
        public User User { get; set; }
        public int UserId { get; set; }
        public Entry Entry { get; set; }
        public int EntryId { get; set; }

        public bool Voted { get; set; }

        public UserEntry(int userId, int entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}
