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
        public StackInternshipDbContext context;
        public UserService()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        public bool RegisterUser(string name, string password)
        {
            if(context.Users
                .Where(e => e.UserName == name)
                .Any())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Već postoji osoba s ovim korisničkim imenom!");
                Console.ResetColor();
                PopupService.ReturnToLoginMenu();
                return false;
            }

            context.Users.Add(new User(name, password));
            context.SaveChanges();
            return true;
        }
        public User LoginIntoUserAccount(string name, string password)
        {
            var loggedInUser = context.Users
                .Where(u => u.Password == password)
                .FirstOrDefault(u => u.UserName == name);
            return loggedInUser;
        }

        public User ChangeUsersDataMenu(User loggedInUser)
        {
            UserService user = new();
            User changedUser = null;
            while (true)
            {
                PrintsUsersData(loggedInUser);
                var choice = (ChangeUsersDataChoice)ChangeUsersDataOutput(loggedInUser);
                switch (choice)
                {
                    case ChangeUsersDataChoice.ChangeName:
                        changedUser = user.ChangeUsersName(loggedInUser);
                        if (changedUser is not null) loggedInUser = changedUser;
                        break;
                    case ChangeUsersDataChoice.ChangePassword:
                        changedUser = user.ChangeUsersPassword(loggedInUser);
                        if (changedUser is not null) loggedInUser = changedUser;
                        break;
                    case ChangeUsersDataChoice.Submenu:
                        Console.Clear();
                        if (loggedInUser.Role is UserRole.Intern)
                            changedUser = user.InternSubmenu(loggedInUser);
                        if (loggedInUser.Role is UserRole.Organizer)
                            changedUser = user.OrganizerSubmenu(loggedInUser);
                        if (changedUser is not null) loggedInUser = changedUser;
                        break;
                    case ChangeUsersDataChoice.Exit:
                        PopupService.ReturnToDashboard();
                        context.SaveChanges();
                        return loggedInUser;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 2)\n");
                        Console.ResetColor();
                        break;
                }
            }
        }

        public void PrintsUsersData(User loggedInUser)
        {
            Console.Clear();
            context.Users
                .Where(u => u == loggedInUser)
                .ToList()
                .ForEach(u =>
                {
                    Console.Write($"Korisničko ime: {u.UserName}\n" +
                        $"Reputacijski bodovi: {u.ReputationPoints}\n" +
                        $"Uloga: {u.Role}\n" +
                        $"Trusted user: ");
                    if (loggedInUser.IsTrustedUser)
                    {
                        Console.Write("da\n\n");
                    }
                    else Console.Write("ne\n\n");
                });
        }

        public static char ChangeUsersDataOutput(User loggedInUser)
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Promjena korisničkog imena\n" +
                "2 - Promjena lozinke\n" +
                $"3 - {loggedInUser.Role} podizborik\n" +
                "0 - Povratak na dashboard");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public User ChangeUsersName(User loggedInUser)
        {
            Console.Clear();
            var newUserName = OutputService.EnterInfo(InputString.ChangeUserName);
            if (newUserName is null) return null;
            if (newUserName == loggedInUser.UserName)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unijeli ste korisničko ime koje već imate!");
                Console.ResetColor();
                PopupService.ReturnToProfile();
                return null;
            }

            loggedInUser = context.Users
                .Where(u => u == loggedInUser)
                .FirstOrDefault();
            var oldName = loggedInUser.UserName;
            loggedInUser.UserName = newUserName;

            Console.WriteLine($"Jeste li sigurni da želite promijeniti ime iz {oldName} u {newUserName}?");
            if (!ChecksAndVerifications.ConfirmationCheck() || !PasswordValidation(loggedInUser))
            {
                PopupService.ReturnToProfile();
                return null;
            }
            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uspješno ste promijenili ime.");
            Console.ResetColor();

            PopupService.ReturnToProfile();
            return loggedInUser;
        }

        public bool PasswordValidation(User loggedInUser)
        {
            Console.WriteLine("Molimo unesite lozinku za nastavak:\n" +
            "Za odustajanje unesite prazan unos:");
            var enteredRepeatedPassword = Console.ReadLine().Trim();
            if (enteredRepeatedPassword.Length is 0)
            {
                return false;
            }
            var correct = context.Users
                .Where(e => e.Id == loggedInUser.Id && e.Password == enteredRepeatedPassword)
                .Any();
            if (!correct)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unijeli ste pogrešnu lozinku");
                Console.ResetColor();

            }
            return correct;
        }

        public User ChangeUsersPassword(User loggedInUser)
        {
            Console.Clear();
            if (!PasswordValidation(loggedInUser))
            {
                PopupService.ReturnToProfile();
                return null;
            }
            var newPassword = OutputService.EnterInfo(InputString.ChangePassword);
            if (newPassword is null) return null;
            if (newPassword == loggedInUser.Password)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unijeli ste lozinku koju već koristite!");
                Console.ResetColor();
                PopupService.ReturnToProfile();
                return null;
            }

            loggedInUser = context.Users
                .Where(u => u == loggedInUser)
                .FirstOrDefault();
            var oldPassword = loggedInUser.Password;
            loggedInUser.Password = newPassword;

            Console.WriteLine($"Jeste li sigurni da želite promijeniti lozinku iz {oldPassword} u {newPassword}?");
            if (ChecksAndVerifications.ConfirmationCheck())
            {
                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Uspješno ste promijenili lozinku.");
                Console.ResetColor();
            }
            PopupService.ReturnToProfile();
            return loggedInUser;
        }

        public User BecomeTrusted(User loggedInUser)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            if (loggedInUser.ReputationPoints < 1000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Imate manje od potrebnih 1000 bodova!");
                Console.ResetColor();
                PopupService.ReturnToProfile();
                return loggedInUser;
            }
            if (loggedInUser.IsTrustedUser is true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Već ste trusted user!");
                Console.ResetColor();
                PopupService.ReturnToProfile();
                return loggedInUser;
            }

            foreach (var user in context.Users)
            {
                if (loggedInUser.UserName == user.UserName)
                {
                    user.IsTrustedUser = true;
                    loggedInUser = user;
                    break;
                }
            }
            loggedInUser.IsTrustedUser = true;
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Čestitamo, postali ste trusted user!");
            Console.ResetColor();
            PopupService.ReturnToProfile();
            return loggedInUser;
        }

        public User BecomeOrganizer(User loggedInUser)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            if (loggedInUser.ReputationPoints < 100000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Imate manje od potrebnih 100 000 bodova!");
                Console.ResetColor();
                PopupService.ReturnToProfile();
                return loggedInUser;
            }
            if (loggedInUser.Role is UserRole.Organizer)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Već ste organizator!");
                Console.ResetColor();
                PopupService.ReturnToProfile();
                return loggedInUser;
            }

            loggedInUser = context.Users
                .Where(u => u == loggedInUser)
                .FirstOrDefault();
            //loggedInUser = GetUser(loggedInUser);
            loggedInUser.Role = UserRole.Organizer;

            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Čestitamo, postali ste organizator!");
            Console.ResetColor();
            PopupService.ReturnToProfile();
            return loggedInUser;
        }

        public User GetUser(User loggedInUser)//u domain
        {
            return context.Users
                .Where(u => u == loggedInUser)
                .FirstOrDefault();
        }

        public User InternSubmenu(User loggedInUser)
        {
            var choice = (InternSubmenuChoice)InternSubmenuOutput();
            UserService user = new();
            switch (choice)
            {
                case InternSubmenuChoice.BecomeTrustedUser:
                    var changedUser = user.BecomeTrusted(loggedInUser);
                    if (changedUser is not null) loggedInUser = changedUser;
                    break;
                case InternSubmenuChoice.BecomeOrganizer:
                    changedUser = user.BecomeOrganizer(loggedInUser);
                    if (changedUser is not null) loggedInUser = changedUser;
                    break;
                case InternSubmenuChoice.Return:
                    PopupService.ReturnToProfile();
                    context.SaveChanges();
                    return loggedInUser;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 2)\n");
                    Console.ResetColor();
                    break;
            }
            return null;
        }

        public static char InternSubmenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Postani trusted user (potrebno minimalno 1000 bodova)\n" +
                "2 - Postani organizator (potrebno minimalno 100 000 bodova)\n" +
                "0 - Povratak na pregled profila");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public User OrganizerSubmenu(User loggedInUser)
        {
            DeactivationService user = new();
            while (true)
            {
                var choice = (OrganizerSubmenuChoice)OrganizerSubmenuOutput();
                switch (choice)
                {
                    case OrganizerSubmenuChoice.DeactivateTemporarily:
                    case OrganizerSubmenuChoice.DeactivatePermanently:
                    case OrganizerSubmenuChoice.Reactivate:
                        user.ChooseIntern(loggedInUser, choice);
                        break;
                    case OrganizerSubmenuChoice.Return:
                        PopupService.ReturnToProfile();
                        context.SaveChanges();
                        return loggedInUser;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 3)\n");
                        Console.ResetColor();
                        break;
                }
            }
        }

        public static char OrganizerSubmenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Privremeno deaktiviraj korisnikov profil\n" +
                "2 - Trajno deaktiviraj korisnikov profil\n" +
                "3 - Reaktiviraj deaktivirani profil\n" +
                "0 - Povratak na pregled profila");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }
    }
}
