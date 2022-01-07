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
    public class Reader
    {
        public static char GetMenuChoice()
        {
            char.TryParse(Console.ReadLine().Trim(), out char choice);
            return choice;
        }

        public static int GetDeactivationLength()
        {
            Console.WriteLine("Unesi broj dana koliko želite da deaktivacija traje.\n" +
                "Za odustajanje unesite 0:");
            var correct = int.TryParse(Console.ReadLine().Trim(), out int numberOfDays);

            if (!correct)
            {
                StringHelper.OutputPainter("Molimo unesite pozitivni cijeli broj! " +
                    "Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                numberOfDays = GetDeactivationLength();
            }
            if (numberOfDays is 0)
            {
                PopupPrinter.GiveUp();
                return 0;
            }
            if (numberOfDays < 0)
            {
                StringHelper.OutputPainter("Nije dopušteno unositi negativne brojeve! " +
                    "Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                numberOfDays = GetDeactivationLength();
            }
            return numberOfDays;
        }

        public static User FindUserFromUserName()
        {
            var enteredUserName = EnterCredentials(InputStringType.NewUserName);
            if (enteredUserName is null)
            {
                return null;
            }

            UserRepository ur = new();
            var chosenUser = ur.GetUserFromUserName(enteredUserName);

            if (chosenUser is null)
            {
                StringHelper.OutputPainter("Ne postoji osoba s tim imenom. " +
                    "Molimo ponovite unos.", ConsoleColor.Red, ConsoleColor.Black);
                chosenUser = FindUserFromUserName();
            }
            return chosenUser;
        }

        public static string EnterCredentials(InputStringType choice)
        {
            string entry;
            UserHelper.EnterCredentialsHeader(choice);

            Console.Write("\nZa odustajanje od upisa, unesite prazan unos:\n");
            entry = Console.ReadLine().Trim();
            ValidityOfStringType validity = StringHelper.CheckIfEntryIsValid(entry);

            if (validity is ValidityOfStringType.GiveUp)
            {
                PopupPrinter.GiveUp();
                return null;
            }
            else if (validity is ValidityOfStringType.Unvalid)
            {
                Console.Write("\nMolimo ponovite unos.");
                entry = EnterCredentials(choice);
            }
            return entry;
        }
    }
}
