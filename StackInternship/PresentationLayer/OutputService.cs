using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using DataLayer.Entities;

namespace PresentationLayer
{
    public class OutputService
    {
        static public bool LoginMenu()
        {
            while (true)
            {
                var choice = (LoginMenuChoice)LoginMenuOutput();

                switch (choice)
                {
                    case LoginMenuChoice.Login:
                    case LoginMenuChoice.Register:
                        var loggedInUser = EnterUserInfo(choice);
                        if (loggedInUser is not null)
                        {
                            DashboardMenu(loggedInUser);
                        }
                        break;
                    case LoginMenuChoice.Exit:
                        return true;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 2)\n");
                        Console.ResetColor();
                        break;
                }
            }
        }
        static public char LoginMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Prijavi se u račun\n" +
                "2 - Registracija novog korisnika\n" +
                "0 - Izlaz iz aplikacije");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }
        static public string EnterInfo(InputString choice)
        {
            string entry;
            EnterinfoPriorOutput(choice);

            while (true)
            {
                Console.Write("\nZa odustajanje od upisa, unesite prazan unos:\n");
                entry = Console.ReadLine().Trim();
                ValidityOfString validity = (ValidityOfString)ChecksAndVerifications.CheckIfEntryIsValid(entry);

                if (ValidityOfString.GiveUp == validity)
                {
                    PopupService.GiveUp();
                    return null;
                }

                else if (ValidityOfString.Unvalid == validity)
                {
                    Console.Write("\nMolimo ponovite unos.");
                }
                else return entry;
            }
        }

        static void EnterinfoPriorOutput(InputString choice)
        {
            switch (choice) {
                case InputString.NewUserName:
                    Console.Write("Unesite korisničko ime: ");
                    break;
                case InputString.NewPassword:
                    Console.Write("\nUnesite lozinku: ");
                    break;
                case InputString.ChangeUserName:
                    Console.Write("Unesite novo korisničko ime: ");
                    break;
                case InputString.ChangePassword:
                    Console.Write("\nUnesite novu lozinku: ");
                    break;
            }
        }

        static User EnterUserInfo(LoginMenuChoice choice)
        {
            Console.Clear();
            var name = EnterInfo(InputString.NewUserName);
            if (name is null)
            {
                return null;
            }

            var password = EnterInfo(InputString.NewPassword);
            if (password is null)
            {
                return null;
            }

            User loggedInUser = null;

            if (choice is LoginMenuChoice.Register)
            {
                var success = RegisterNewUser(name, password);
                if (!success)
                {
                    return null;
                }
            }

            UserService user = new();
            loggedInUser = user.LoginIntoUserAccount(name, password);
            Console.ForegroundColor = ConsoleColor.Red;
            if (loggedInUser is null)
            {
                Console.WriteLine("Unijeli ste netočne podatke.");
                Console.ResetColor();
                PopupService.ReturnToLoginMenu();
                return null;
            }
            if (loggedInUser.PermanentDeactivation is true)
            {
                Console.WriteLine("Profil vam je trajno deaktiviran.");
                Console.ResetColor();
                PopupService.ReturnToLoginMenu();
                return null;
            }
            if (loggedInUser.DeactivatedUntil > DateTime.Now)
            {
                Console.WriteLine($"Profil vam je deaktiviran do {loggedInUser.DeactivatedUntil}.");
                Console.ResetColor();
                PopupService.ReturnToLoginMenu();
                return null;
            }
            loggedInUser.DeactivatedUntil = null;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nPrijavljeni ste kao {loggedInUser.UserName}.");
            Console.ResetColor();
            PopupService.ContinueToDashboard();

            return loggedInUser;
        }

        static bool RegisterNewUser(string name, string password)
        {
            UserService user = new();
            var success = user.RegisterUser(name, password);
            if (!success)
            {
                return false;
            }
            PopupService.AfterRegistration();
            return true;
        }

        static void DashboardMenu(User loggedInUser)
        {
            while (true)
            {
                var choice = (DashboardMenuChoice)DashboardMenuOutput();

                switch (choice)
                {
                    case DashboardMenuChoice.Users:
                        Console.Clear();
                        PrintingUsersService.PrintMenu();
                        break;
                    case DashboardMenuChoice.MyProfile:
                        loggedInUser = ShowUsersProfile(loggedInUser);
                        break;
                    case DashboardMenuChoice.Resources:
                        PopupService.ContinueToDepartments();
                        loggedInUser = ResourceService.DepartmentMenu(loggedInUser);
                        break;
                    case DashboardMenuChoice.Exit:
                        PopupService.ReturnToLoginMenu();
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 5)\n");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static public char DashboardMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Resursi\n" +
                "2 - Neodgovoreno\n" +
                "3 - Popularno\n" +
                "4 - Korisnici\n" +
                "5 - Moj profil\n" +
                "0 - Odjava");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        static public User ShowUsersProfile(User loggedInUser)
        {
            UserService user = new();
            return user.ChangeUsersDataMenu(loggedInUser);
        }
    }
}
