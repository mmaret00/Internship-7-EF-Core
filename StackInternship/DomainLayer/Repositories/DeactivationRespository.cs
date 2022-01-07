using DataLayer.Entities;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class DeactivationRespository
    {
        private readonly StackInternshipDbContext context;
        public DeactivationRespository()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        public DateTime? DeactivateInternTemporarily(User userToDeactivate, int numberOfDays)
        {
            userToDeactivate = context.Users
                .Find(userToDeactivate.Id);
            userToDeactivate.PermanentDeactivation = false;
            userToDeactivate.DeactivatedUntil = DateTime.Now.AddDays(numberOfDays);
            context.SaveChanges();
            return userToDeactivate.DeactivatedUntil;
        }

        public void DeactivateInternPermanently(User userToDeactivate)
        {
            userToDeactivate = context.Users
                .Find(userToDeactivate.Id);
            userToDeactivate.DeactivatedUntil = null;
            userToDeactivate.PermanentDeactivation = true;
            context.SaveChanges();
        }

        public void ReactivateIntern(User userToReactivate)
        {
            userToReactivate = context.Users
                .Find(userToReactivate.Id);
            userToReactivate.DeactivatedUntil = null;
            userToReactivate.PermanentDeactivation = false;
            context.SaveChanges();
        }
    }
}
