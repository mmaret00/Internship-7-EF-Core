using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class LoginService
    {
        public static User EnterUsersInfo(LoginMenuChoice choice)
        {
            Console.Clear();
            var name = Reader.EnterCredentials(InputStringType.NewUserName);
            if (name is null)
            {
                return null;
            }
            var password = Reader.EnterCredentials(InputStringType.NewPassword);
            if (password is null)
            {
                return null;
            }

            if (choice is LoginMenuChoice.Register && !RegisterNewUser(name, password))
            {
                return null;
            }
            return ProceedWithLogin(name, password);
        }

        public static User ProceedWithLogin(string name, string password)
        {
            UserRepository ur = new();
            User loggedInUser = ur.LogIntoUserAccount(name, password);
            if (!LoginHelper.CheckIsUserIsAllowedToLogin(loggedInUser))
            {
                return null;
            }
            if (loggedInUser.DeactivatedUntil is not null)
            {
                ur.RemoveExpiredTemporaryDeactivation(loggedInUser.Id);
            }
            StringHelper.OutputPainter($"\nPrijavljeni ste kao {loggedInUser.UserName}.", ConsoleColor.Green, ConsoleColor.Black);
            PopupPrinter.ContinueToDashboard();
            return loggedInUser;
        }

        public static bool RegisterNewUser(string name, string password)
        {
            UserRepository ur = new();
            var success = ur.RegisterUser(name, password);
            if (success)
            {
                PopupPrinter.AfterRegistration();
            }
            else
            {
                PopupPrinter.UserNameTaken();
            }
            return success;
        }
    }
}
