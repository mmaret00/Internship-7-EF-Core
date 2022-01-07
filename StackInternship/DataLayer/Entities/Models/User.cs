using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Entities.Enums;

namespace DataLayer.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ReputationPoints { get; set; }
        public UserRole Role { get; set; }
        public bool IsTrustedUser { get; set; }
        public DateTime? DeactivatedUntil { get; set; }
        public bool PermanentDeactivation { get; set; }

        public User(string name, string password) {

            UserName = name;
            Password = password;
            ReputationPoints = 1;
        }

        public User() { }
    }
}
