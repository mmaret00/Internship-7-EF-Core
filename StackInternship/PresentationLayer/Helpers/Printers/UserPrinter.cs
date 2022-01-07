using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer;
using DomainLayer.Factories;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class UserPrinter
    {
        public static void PrintUsersHeader(PrintUsersMenuChoice choice, int numberOfPoints)
        {
            Console.Clear();
            switch (choice)
            {
                case PrintUsersMenuChoice.AllUsers:
                    Console.WriteLine("Svi korisnici:\n");
                    break;
                case PrintUsersMenuChoice.OnlyOrganizers:
                    Console.WriteLine("Organizatori:\n");
                    break;
                case PrintUsersMenuChoice.OnlyInterns:
                    Console.WriteLine("Interni:\n");
                    break;
                case PrintUsersMenuChoice.TrustedUsers:
                    Console.WriteLine("Trusted useri:\n");
                    break;
                case PrintUsersMenuChoice.LessThanReputationPoints:
                    Console.WriteLine($"Korisnici s manje od {numberOfPoints} reputacijskih bodova:\n");
                    break;
                case PrintUsersMenuChoice.MoreThanReputationPoints:
                    Console.WriteLine($"Korisnici s više od {numberOfPoints} reputacijskih bodova:\n");
                    break;
            }
        }

        public static void PrintUsers(PrintUsersMenuChoice choice)
        {
            var numberOfPoints = 0;
            if (choice is PrintUsersMenuChoice.LessThanReputationPoints
                || choice is PrintUsersMenuChoice.MoreThanReputationPoints)
            {
                numberOfPoints = UserHelper.InsertedNumberOfReputationPointsCheck();
            }
            PrintUsersHeader(choice, numberOfPoints);

            UserRepository ur = new();
            var userDetails = ur.GetUserDetailsList();
            var numberOfUsersPrinted = 0;
            foreach (var userDetail in userDetails)
            {
                if (choice is PrintUsersMenuChoice.OnlyOrganizers
                    && userDetail.Role is UserRole.Intern) continue;
                if (choice is PrintUsersMenuChoice.OnlyInterns
                    && userDetail.Role is UserRole.Organizer) continue;
                if (choice is PrintUsersMenuChoice.TrustedUsers
                    && (userDetail.IsTrustedUser is false
                    || userDetail.Role is UserRole.Organizer)) continue;
                if (choice is PrintUsersMenuChoice.LessThanReputationPoints
                    && userDetail.ReputationPoints >= numberOfPoints) continue;
                if (choice is PrintUsersMenuChoice.MoreThanReputationPoints
                    && userDetail.ReputationPoints <= numberOfPoints) continue;

                PrintDetailsOfUser(userDetail);
                numberOfUsersPrinted++;
            }
            if (numberOfUsersPrinted is 0)
            {
                StringHelper.OutputPainter("\nLista je prazna!", ConsoleColor.Red, ConsoleColor.Black);
            }
            PopupPrinter.ReturnToPrintMenu();
        }

        public static void PrintDetailsOfUser(UserDetails userDetail)
        {
            Console.ForegroundColor = UserHelper.ChooseUserPrintColor(userDetail);
            PrintUsersBasicData(userDetail);

            if (userDetail.PermanentDeactivation is true) {
                Console.WriteLine("Račun je trajno deaktiviran.");
            }
            if (userDetail.DeactivatedUntil is not null)
            {
                Console.WriteLine($"Račun je deaktiviran do {userDetail.DeactivatedUntil}");
            }
            Console.WriteLine("");
            Console.ResetColor();
        }
        
        public static void PrintUsersRegardingActivity(UserDetails userDetail)
        {
            if (userDetail.PermanentDeactivation is false && userDetail.DeactivatedUntil is null)
            {
                StringHelper.OutputPainter($"Korisničko ime: {userDetail.UserName}\nRačun je aktivan.\n", ConsoleColor.Green, ConsoleColor.Black);
                return;
            }
            StringHelper.OutputPainter($"Korisničko ime: {userDetail.UserName}", ConsoleColor.Red, ConsoleColor.Black);
            if (userDetail.PermanentDeactivation is true)
            {
                StringHelper.OutputPainter("Račun je trajno deaktiviran.\n", ConsoleColor.Red, ConsoleColor.Black);
                return;
            }
            StringHelper.OutputPainter($"Račun je deaktiviran do {userDetail.DeactivatedUntil}\n", ConsoleColor.Red, ConsoleColor.Black);
        }

        public static void PrintUsersBasicData(UserDetails userDetail)
        {
            Console.Write($"Korisničko ime: {userDetail.UserName}\n" +
                $"Reputacijski bodovi: {userDetail.ReputationPoints}\n" +
                $"Uloga: {userDetail.Role}\n");
            if (userDetail.Role is UserRole.Organizer)
            {
                return;
            }
            if (userDetail.IsTrustedUser)
            {
                Console.Write("Trusted user: da\n");
            }
            else Console.Write("Trusted user: ne\n");
        }

        public static void ProfileMenuHeader(int id)
        {
            UserRepository ur = new();
            PrintUsersBasicData(ur.GetSingleUsersDetails(id));
        }
    }
}
