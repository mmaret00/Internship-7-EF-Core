using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class PrintingUsersService
    {
        public StackInternshipDbContext context;
        public PrintingUsersService()
        {
            context = DbContextFactory.GetStackInternshipDbContext();
        }

        static public void PrintMenu()
        {
            while (true)
            {
                var choice = (PrintMenuChoice)PrintMenuOutput();
                PrintingUsersService user = new();

                switch (choice)
                {
                    case PrintMenuChoice.AllUsers:
                        user.PrintUsers();
                        break;
                    case PrintMenuChoice.UsersWithSpecificRole:
                        user.PrintUsersWithSpecificRole();
                        break;
                    case PrintMenuChoice.TrustedUsers:
                        user.PrintTrustedUsers();
                        break;
                    case PrintMenuChoice.MoreThanReputationPoints:
                        user.PrintUsersWithMoreReputationPoints();
                        break;
                    case PrintMenuChoice.LessThanReputationPoints:
                        user.PrintUsersWithLessReputationPoints();
                        break;
                    case PrintMenuChoice.Return:
                        PopupService.ReturnToDashboard();
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

        static public char PrintMenuOutput()
        {
            Console.WriteLine($"Odaberite željenu vrstu ispisa:\n" +
                "1 - Svi korisnici\n" +
                "2 - Samo korisnici s određenom ulogom\n" +
                "3 - Samo trusted useri\n" +
                "4 - Korisnici s više od određenog broja reputacijskih bodova\n" +
                "5 - Korisnici s manje od određenog broja reputacijskih bodova\n" +
                "0 - Povratak");

            char.TryParse(Console.ReadLine().Trim(), out char choice);

            return choice;
        }

        public void PrintUsers()
        {
            Console.Clear();
            Console.WriteLine("Svi korisnici:");
            context.Users
                .ToList()
                .ForEach(u =>
                {
                    PrintDetailsOfUser(u);
                });
            PopupService.ReturnToPrintMenu();
        }

        public void PrintUsersWithSpecificRole()
        {
            Console.Clear();
            var choice = ChecksAndVerifications.ChooseRole();
            Console.Clear();
            Console.WriteLine($"Korisnici s ulogom {choice}:");
            context.Users
                .Where(u => u.Role == choice)
                .ToList()
                .ForEach(u => {
                    PrintDetailsOfUser(u);
                });
            PopupService.ReturnToPrintMenu();
        }

        public void PrintTrustedUsers()
        {
            Console.Clear();
            Console.WriteLine("Svi korisnici koji su 'trusted user':");
            context.Users
                .Where(u => u.IsTrustedUser == true)
                .ToList()
                .ForEach(u => {
                    PrintDetailsOfUser(u);
                });
            PopupService.ReturnToPrintMenu();
        }

        public void PrintUsersWithLessReputationPoints()
        {
            Console.Clear();
            var numberOfPoints = ChecksAndVerifications.InsertedNumberOfReputationPointsCheck();
            Console.Clear();
            Console.Write($"Svi korisnici s manje od {numberOfPoints} bodova:");

            context.Users
                .Where(u => u.ReputationPoints < numberOfPoints)
                .ToList()
                .ForEach(u => {
                    PrintDetailsOfUser(u);
                });

            context.Users
                .Where(u => u.ReputationPoints > numberOfPoints)
                .ToList()
                .ForEach(u => {
                    PrintDetailsOfUser(u);
                });

            PopupService.ReturnToPrintMenu();
        }

        public void PrintUsersWithMoreReputationPoints()
        {
            Console.Clear();
            var numberOfPoints = ChecksAndVerifications.InsertedNumberOfReputationPointsCheck();
            Console.Clear();
            Console.Write($"Svi korisnici s više od {numberOfPoints} bodova:");

            context.Users
                .Where(u => u.ReputationPoints > numberOfPoints)
                .ToList()
                .ForEach(u => {
                    PrintDetailsOfUser(u);
                });
            PopupService.ReturnToPrintMenu();
        }

        public void PrintDetailsOfUser(User u)
        {
            if (u.PermanentDeactivation is true || u.DeactivatedUntil is not null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (u.Role is UserRole.Organizer)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            Console.Write($"\nKorisničko ime: {u.UserName}\n" +
                        $"Reputacijski bodovi: {u.ReputationPoints}\n" +
                        $"Uloga: {u.Role}\n" +
                        $"Trusted user: ");
            if (u.IsTrustedUser)
            {
                Console.Write("da\n");
            }
            else Console.Write("ne\n");

            if (u.PermanentDeactivation is true) {
                Console.WriteLine("Račun je trajno deaktiviran.");
            }
            if (u.DeactivatedUntil is not null)
            {
                Console.WriteLine($"Račun je deaktiviran do {u.DeactivatedUntil}");
            }
            Console.ResetColor();
        }
    }
}
