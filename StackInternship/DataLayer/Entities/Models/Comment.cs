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
    }
}
