using DataLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class LoginHelper
    {
        public static bool CheckIsUserIsAllowedToLogin(User loggedInUser)
        {
            if (loggedInUser is null)
            {
                StringHelper.OutputPainter("Unijeli ste netočne podatke.", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToLoginMenu();
                return false;
            }
            if (loggedInUser.PermanentDeactivation is true)
            {
                StringHelper.OutputPainter("Profil vam je trajno deaktiviran.", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToLoginMenu();
                return false;
            }
            if (loggedInUser.DeactivatedUntil > DateTime.Now)
            {
                StringHelper.OutputPainter($"Profil vam je deaktiviran do {loggedInUser.DeactivatedUntil}.", ConsoleColor.Red, ConsoleColor.Black);
                PopupPrinter.ReturnToLoginMenu();
                return false;
            }
            return true;
        }
    }
}
