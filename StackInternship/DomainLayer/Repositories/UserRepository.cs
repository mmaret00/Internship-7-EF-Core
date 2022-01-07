using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class UserRepository
    {
        private readonly StackInternshipDbContext context;
        public UserRepository()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        public User GetUserFromUserName(string username)
        {
            return context.Users
                .Where(u => u.UserName == username)
                .FirstOrDefault();
        }

        public bool CheckIfUserNameIsTaken(string username)
        {
            return context.Users
                   .Where(e => e.UserName == username)
                   .Any();
        }

        public bool RegisterUser(string name, string password)
        {
            var usernameAvailable = !CheckIfUserNameIsTaken(name);
            if (usernameAvailable)
            {
                context.Users.Add(new User(name, password));
                context.SaveChanges();
            }
            return usernameAvailable;
        }

        public User LogIntoUserAccount(string name, string password)
        {
            return context.Users
                .FirstOrDefault(u => u.UserName == name
                    && u.Password == password);
        }

        public List<UserDetails> GetUserDetailsList()
        {
            List<UserDetails> userDetailsList = new();

            context.Users
                .OrderBy(u => u.UserName)
                .ToList()
                .ForEach(u =>
                {
                    userDetailsList.Add(GetSingleUsersDetails(u.Id));
                });

            return userDetailsList;
        }

        public UserDetails GetSingleUsersDetails(int id)
        {
            var userDetails = context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDetails
                {
                    UserName = u.UserName,
                    ReputationPoints = u.ReputationPoints,
                    Role = u.Role,
                    IsTrustedUser = u.IsTrustedUser,
                    PermanentDeactivation = u.PermanentDeactivation,
                    DeactivatedUntil = u.DeactivatedUntil,
                })
                .FirstOrDefault();

            return userDetails;
        }

        public User ChangeUserName(User loggedInUser, string newUserName)
        {
            loggedInUser = context.Users
                .Find(loggedInUser.Id);
            loggedInUser.UserName = newUserName;
            context.SaveChanges();
            return loggedInUser;
        }

        public User ChangePassword(User loggedInUser, string newPassword)
        {
            loggedInUser = context.Users
                .Find(loggedInUser.Id);
            loggedInUser.Password = newPassword;
            context.SaveChanges();
            return loggedInUser;
        }

        public bool CheckIfPasswordIsCorrect(User loggedInUser, string enteredPassword)
        {
            return context.Users
                .Where(e => e.Id == loggedInUser.Id && e.Password == enteredPassword)
                .Any();
        }

        public void RemoveExpiredTemporaryDeactivation(int id)
        {
            var user = context.Users
                .Find(id);
            user.DeactivatedUntil = null;
            context.SaveChanges();
        }
    }
}
