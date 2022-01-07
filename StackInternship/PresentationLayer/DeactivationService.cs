using DataLayer.Entities;
using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer;
using DomainLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    class DeactivationService
    {
        public static User ChooseIntern(User loggedInUser, OrganizerSubmenuChoice choice)
        {
            Console.Clear();
            GetUsersWithActivityStatus();
            var chosenUser = Reader.FindUserFromUserName();
            if (chosenUser is null || !DeactivationHelper.ChosenUserCheck(loggedInUser, chosenUser, choice))
            {
                return chosenUser;
            }
            return SelectDeactivationType(chosenUser, choice);
        }

        public static void GetUsersWithActivityStatus()
        {
            UserRepository ur = new();
            var userDetails = ur.GetUserDetailsList();
            Console.WriteLine("Interni:\n");
            foreach (var userDetail in userDetails)
            {
                if (userDetail.Role is UserRole.Organizer) continue;
                UserPrinter.PrintUsersRegardingActivity(userDetail);
            }
        }

        public static User SelectDeactivationType(User chosenUser, OrganizerSubmenuChoice choice)
        {
            switch (choice)
            {
                case OrganizerSubmenuChoice.DeactivateTemporarily:
                    chosenUser = DeactivateInternTemporarily(chosenUser);
                    return chosenUser;
                case OrganizerSubmenuChoice.DeactivatePermanently:
                    DeactivateInternPermanently(chosenUser);
                    return chosenUser;
                case OrganizerSubmenuChoice.Reactivate:
                    ReactivateIntern(chosenUser);
                    return chosenUser;
                default:
                    return chosenUser;
            }
        }

        public static User DeactivateInternTemporarily(User userToDeactivate)
        {
            var numberOfDays = Reader.GetDeactivationLength();
            if (numberOfDays is 0)
            {
                return userToDeactivate;
            }
            DeactivationRespository dr = new();
            var deactivatedUntil = dr.DeactivateInternTemporarily(userToDeactivate, numberOfDays);
            StringHelper.OutputPainter($"Korisnik {userToDeactivate.UserName} je deaktiviran do {deactivatedUntil}.", ConsoleColor.Green, ConsoleColor.Black);
            PopupPrinter.ReturnToDeactivationMenu();
            return userToDeactivate;
        }

        public static void DeactivateInternPermanently(User userToDeactivate)
        {
            DeactivationRespository dr = new();
            dr.DeactivateInternPermanently(userToDeactivate);
            StringHelper.OutputPainter($"Korisnik {userToDeactivate.UserName} je trajno deaktiviran." , ConsoleColor.Green, ConsoleColor.Black);
            PopupPrinter.ReturnToDeactivationMenu();
        }

        public static void ReactivateIntern(User userToReactivate)
        {
            DeactivationRespository dr = new();
            dr.ReactivateIntern(userToReactivate);
            StringHelper.OutputPainter($"Korisnik {userToReactivate.UserName} je reaktiviran." , ConsoleColor.Green, ConsoleColor.Black);
            PopupPrinter.ReturnToDeactivationMenu();
        }
    }
}
