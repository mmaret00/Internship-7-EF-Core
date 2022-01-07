using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    class UserHelper
    {
        public static int InsertedNumberOfReputationPointsCheck()
        {
            Console.WriteLine("Molimo unesite željeni broj bodova.");
            var success = int.TryParse(Console.ReadLine().Trim(), out int choice);
            if (success)
            {
                return choice;
            }
            StringHelper.OutputPainter("Nedopušten unos." , ConsoleColor.Red, ConsoleColor.Black);
            return InsertedNumberOfReputationPointsCheck();
        }

        public static UserChangeResult NewNameCheck(string newUserName, User loggedInUser)
        {
            if (newUserName is null)
            {
                return UserChangeResult.GiveUp;
            }
            if (newUserName == loggedInUser.UserName)
            {
                PopupPrinter.SameNameAsBefore();
                return UserChangeResult.SameAsBefore;
            }
            Console.WriteLine($"Jeste li sigurni da želite promijeniti ime iz {loggedInUser.UserName} u {newUserName}?");
            if (!StringHelper.ConfirmationCheck() || !PasswordValidation(loggedInUser))
            {
                PopupPrinter.ReturnToProfile();
                return UserChangeResult.GiveUp;
            }
            return UserChangeResult.Success;
        }

        public static UserChangeResult NewPasswordCheck(string newPassword, User loggedInUser)
        {
            if (newPassword is null || !PasswordValidation(loggedInUser))
            {
                PopupPrinter.GiveUp();
                return UserChangeResult.GiveUp;
            }
            Console.WriteLine($"Jeste li sigurni da želite promijeniti lozinku iz {loggedInUser.Password} u {newPassword}?");
            if (!StringHelper.ConfirmationCheck())
            {
                PopupPrinter.ReturnToProfile();
                return UserChangeResult.GiveUp;
            }
            return UserChangeResult.Success;
        }

        public static bool PasswordValidation(User loggedInUser)
        {
            Console.WriteLine("Unesite dosadašnju lozinku za nastavak:\n" +
            "Za odustajanje unesite prazan unos:");
            var enteredPassword = Console.ReadLine().Trim();
            if (enteredPassword.Length is 0)
            {
                return false;
            }

            UserRepository ur = new();
            var correct = ur.CheckIfPasswordIsCorrect(loggedInUser, enteredPassword);
            if(!correct)
            {
                StringHelper.OutputPainter("Unijeli ste pogrešnu lozinku! " +
                    "Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                correct = PasswordValidation(loggedInUser);
            }
            return correct;
        }

        public static User SetNewUserName(User loggedInUser)
        {
            UserRepository ur = new();
            var newUserName = Reader.EnterCredentials(InputStringType.ChangeUserName);
            if (ur.CheckIfUserNameIsTaken(newUserName))
            {
                StringHelper.OutputPainter($"Korisničko ime {newUserName} je zauzeto! " +
                    $"Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                return SetNewUserName(loggedInUser);
            }
            var resultOfNameChange = NewNameCheck(newUserName, loggedInUser);
            if (resultOfNameChange is UserChangeResult.Success)
            {
                loggedInUser = ur.ChangeUserName(loggedInUser, newUserName);
                PopupPrinter.SuccessfulNameChange();
            }
            return loggedInUser;
        }

        public static User SetNewPassword(User loggedInUser)
        {
            Console.Clear();
            UserRepository ur = new();
            var newPassword = Reader.EnterCredentials(InputStringType.ChangePassword);
            var resultOfPasswordChange = NewPasswordCheck(newPassword, loggedInUser);
            if (resultOfPasswordChange is UserChangeResult.Success)
            {
                loggedInUser = ur.ChangePassword(loggedInUser, newPassword);
                PopupPrinter.SuccessfulPasswordChange();
            }
            return loggedInUser;
        }

        public static ConsoleColor ChooseUserPrintColor(UserDetails userDetail)
        {
            if (userDetail.Role is UserRole.Organizer)
            {
                return ConsoleColor.DarkYellow;
            }
            if (userDetail.PermanentDeactivation is true || userDetail.DeactivatedUntil > DateTime.Now)
            {
                return ConsoleColor.Red;
            }
            return ConsoleColor.DarkCyan;
        }

        public static void EnterCredentialsHeader(InputStringType choice)
        {
            switch (choice)
            {
                case InputStringType.NewUserName:
                    Console.Write("Unesite korisničko ime: ");
                    break;
                case InputStringType.NewPassword:
                    Console.Write("\nUnesite lozinku: ");
                    break;
                case InputStringType.ChangeUserName:
                    Console.Write("Unesite novo korisničko ime: ");
                    break;
                case InputStringType.ChangePassword:
                    Console.Write("Unesite novu lozinku: ");
                    break;
            }
        }
    }
}
