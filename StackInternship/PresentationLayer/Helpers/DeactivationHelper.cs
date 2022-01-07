using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class DeactivationHelper
    {
        public static bool ChosenUserCheck(User loggedInUser, User chosenUser, OrganizerSubmenuChoice choice)
        {
            if (chosenUser.Id == loggedInUser.Id)
            {
                StringHelper.OutputPainter("Ne možete deaktivirati ni reaktivirati sami sebe!", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToDeactivationMenu();
                return false;
            }
            if (chosenUser.Role is UserRole.Organizer)
            {
                StringHelper.OutputPainter("Ne možete deaktivirati ni reaktivirati drugog organizatora!", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToDeactivationMenu();
                return false;
            }
            if ((chosenUser.PermanentDeactivation is false && chosenUser.DeactivatedUntil is null) && choice is OrganizerSubmenuChoice.Reactivate)
            {
                StringHelper.OutputPainter("Korisnik nije deaktiviran!", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToDeactivationMenu();
                return false;
            }
            return true;
        }
    }
}
