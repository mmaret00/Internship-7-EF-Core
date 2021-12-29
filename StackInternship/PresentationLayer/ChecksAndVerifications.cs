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
                Console.WriteLine("Unos treba sadržavati barem pet znakova!");
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
                Console.WriteLine("Nedopušten unos.");
            }
        }
    }
}
