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
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 2)\n");
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
                    PopupService.GiveUpOnLogin();
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
                    Console.Write("Unesite lozinku: ");
                    break;
                case InputString.ChangeUserName:
                    Console.Write("Unesite novo korisničko ime: ");
                    break;
                case InputString.ChangePassword:
                    Console.Write("Unesite novu lozinku: ");
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
                RegisterNewUser(name, password);
            }

            else if (choice is LoginMenuChoice.Login)
            {
                UserService user = new();
                loggedInUser = user.LoginIntoUserAccount(name, password);
                Console.WriteLine($"\nPrijavljeni ste kao {loggedInUser.UserName}.");
                PopupService.ContinueToDashboard();
            }

            return loggedInUser;
        }

        static void RegisterNewUser(string name, string password)
        {
            UserService user = new();
            user.RegisterUser(name, password);
            PopupService.AfterRegistration();
        }

        static void DashboardMenu(User loggedInUser)
        {
            while (true)
            {
                var choice = (DashboardMenuChoice)DashboardMenuOutput();

                switch (choice)
                {
                    case DashboardMenuChoice.Users:
                        PrintAllUsers();
                        break;
                    case DashboardMenuChoice.MyProfile:
                        ShowUsersProfile(loggedInUser);
                        break;
                    case DashboardMenuChoice.Resources:
                        PopupService.ContinueToDepartments();
                        ResourceService.DepartmentMenu(loggedInUser);
                        break;
                    case DashboardMenuChoice.Exit:
                        PopupService.ReturnToLoginMenu();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Molimo unesite jedan od dopuštenih brojeva (0 - 4)\n");
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

        static public void PrintAllUsers()
        {
            Console.WriteLine("Svi korisnici:");
            UserService user = new();
            user.PrintUsers();
            PopupService.ReturnToDashboard();
        }

        static public void ShowUsersProfile(User loggedInUser)
        {
            UserService user = new();
            user.ChangeUsersDataMenu(loggedInUser);
        }
    }
}
