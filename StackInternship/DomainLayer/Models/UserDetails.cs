using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class UserDetails
    {
        public string UserName { get; set; }
        public int ReputationPoints { get; set; }
        public UserRole Role { get; set; }
        public bool IsTrustedUser { get; set; }
        public bool PermanentDeactivation { get; set; }
        public DateTime? DeactivatedUntil { get; set; }
    }
}
