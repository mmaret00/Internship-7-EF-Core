using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class ChecksAndVerifications
    {
        static public ValidityOfString CheckIfEntryIsValid(string entry)
        {
            if (CheckIfStringIsEmpty(entry))
            {
                return ValidityOfString.GiveUp;
            }
            if (CheckIfStringIsAtLeastFiveLettersLong(entry))
            {
                return ValidityOfString.Valid;
            }
            return ValidityOfString.Unvalid;
        }

        static bool CheckIfStringIsEmpty(string name)
        {
            if (name.Length is 0)
            {
                return true;
            }
            return false;
        }

        static bool CheckIfStringIsAtLeastFiveLettersLong(string entry)
        {
            if (entry.Length < 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unos treba sadržavati barem pet znakova!");
                Console.ResetColor();
                return false;
            }
            return true;
        }

        static public bool ConfirmationCheck()
        {
            while (true)
            {
                Console.WriteLine("Molimo unesite 'da' ili 'ne':");
                var choice = Console.ReadLine().Trim().ToUpper();

                if (choice is "DA") return true;
                else if (choice is  "NE") return false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nedopušten unos.");
                Console.ResetColor();
            }
        }

        static public UserRole ChooseRole()
        {
            while (true)
            {
                Console.WriteLine("Unesite broj uloge:\n" +
                    "1 - Pripravnici\n" +
                    "2 - Organizatori");
                var choice = Console.ReadLine().Trim().ToUpper();

                if (choice is "1") return UserRole.Intern;
                else if (choice is "2") return UserRole.Organizer;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nedopušten unos.");
                Console.ResetColor();
            }
        }

        static public int InsertedNumberOfReputationPointsCheck()
        {
            while (true)
            {
                Console.WriteLine("Molimo unesite željeni broj bodova:");
                var success = int.TryParse(Console.ReadLine().Trim(), out int choice);
                if(success) return choice;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nedopušten unos.");
                Console.ResetColor();
            }
        }
    }
}
