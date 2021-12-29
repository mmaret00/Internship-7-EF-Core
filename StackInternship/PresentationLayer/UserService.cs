using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer;
using DataLayer.Entities;
using DataLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Factories;
using DomainLayer;

namespace PresentationLayer
{
    public class UserService
    {
        public UserService()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }
        private readonly StackInternshipDbContext context;

        public bool RegisterUser(string name, string password)
        {
            if (Enumerable.Any(context.Users, user => name == user.UserName && password == user.Password))
                return false;

            context.Users.Add(new User(name, password));
            context.SaveChanges();
            return true;
        }
        public User LoginIntoUserAccount(string name, string password)
        {
            var loggedInUser = context.Users.Where(c => c.Password == password).FirstOrDefault(c => c.UserName == name);
            //crasha ako se falije sifra ili ime
            return loggedInUser;
        }

        public void PrintUsers()
        {
            Console.Clear();
            context.Users
                .ToList()
                .ForEach(c => {
                    Console.Write($"Korisničko ime: {c.UserName}\n" +
                        $"Reputacijski bodovi: {c.ReputationPoints}\n" +
                        $"Uloga: {c.Role}\n" +
                        $"Trusted user: ");
                    if (c.IsTrustedUser)
                    {
                        Console.Write("da\n\n");
                    }
                    else Console.Write("ne\n\n");
                });
        }

        public void ChangeUsersDataMenu(User loggedInUser)
        {
            while (true)
            {
                PrintsUsersData(loggedInUser);
                var choice = (ChangeUsersDataChoice)ChangeUsersDataOutput();
                UserService user = new();
                switch (choice)
                {
                    case ChangeUsersDataChoice.ChangeName:
                        /*var newName = */user.ChangeUsersName(loggedInUser.UserName);
                        //if (newName is not null) loggedInUser.UserName = newName;
                        break;
                    case ChangeUsersDataChoice.ChangePassword:
                        user.ChangeUsersPassword(loggedInUser);
                        break;
                    case ChangeUsersDataChoice.Exit:
                        PopupService.ReturnToDashboard();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 2)\n");
                        break;
                }
            }
        }

        public void PrintsUsersData(User loggedInUser)
        {
            Console.Clear();
            Console.Write($"Korisničko ime : {context.Users.FirstOrDefault(c => c.UserName == loggedInUser.UserName).UserName}\n" +
                $"Reputacijski bodovi: {context.Users.FirstOrDefault(c => c.UserName == loggedInUser.UserName).ReputationPoints}\n" +
                $"Uloga: {context.Users.FirstOrDefault(c => c.UserName == loggedInUser.UserName).Role}\n" +
                $"Trusted user: ");
            if (context.Users.FirstOrDefault(c => c.UserName == loggedInUser.UserName).IsTrustedUser)
            {
                Console.Write("da\n\n");
            }
            else Console.Write("ne\n\n");
        }

        public static char ChangeUsersDataOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Promjena korisničkog imena\n" +
                "2 - Promjena lozinke\n" +
                "0 - Povratak na dashboard");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public string ChangeUsersName(string name)
        {
            Console.Clear();
            var newUserName = OutputService.EnterInfo(InputString.ChangeUserName);
            if (newUserName is null) return null;
            var oldName = "";

            foreach (var user in context.Users)
            {
                if(user.UserName == name)
                {
                    oldName = user.UserName;
                    user.UserName = newUserName;
                }
            }

            Console.WriteLine($"Jeste li sigurni da želite promijeniti ime iz {oldName} u {newUserName}?");
            if (ChecksAndVerifications.ConfirmationCheck())
            {
                context.SaveChanges();
                Console.WriteLine("Uspješno ste promijenili ime.");
            }
            PopupService.ClickAnyKeyToContinue();
            return newUserName;
        }

        public void ChangeUsersPassword(User ChangeUsersDataMenu)
        {
            Console.Clear();
            var newPassword = OutputService.EnterInfo(InputString.ChangePassword);
            if (newPassword is null) return;
            var oldPassword = "";

            foreach (var user in context.Users)
            {
                if (user.UserName == ChangeUsersDataMenu.UserName)
                {
                    oldPassword = user.Password;
                    user.Password = newPassword;
                }
            }

            Console.WriteLine($"Jeste li sigurni da želite promijeniti lozinku iz {oldPassword} u {newPassword}?");
            if (ChecksAndVerifications.ConfirmationCheck())
            {
                context.SaveChanges();
                Console.WriteLine("Uspješno ste promijenili lozinku.");
            }
            PopupService.ClickAnyKeyToContinue();
        }
    }
}
