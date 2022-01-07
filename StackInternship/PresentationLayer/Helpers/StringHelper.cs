using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class StringHelper
    {
        public static ValidityOfStringType CheckIfEntryIsValid(string entry)
        {
            if (entry.Length is 0)
            {
                return ValidityOfStringType.GiveUp;
            }
            if (CheckIfStringIsAtLeastFiveLettersLong(entry))
            {
                return ValidityOfStringType.Valid;
            }
            return ValidityOfStringType.Unvalid;
        }

        static bool CheckIfStringIsAtLeastFiveLettersLong(string entry)
        {
            var success = entry.Length >= 5;
            if (!success)
            {
                OutputPainter("Unos treba sadržavati barem pet znakova!" , ConsoleColor.Red, ConsoleColor.Black);
            }
            return success;
        }

        public static bool ConfirmationCheck()
        {
            Console.WriteLine("Molimo unesite 'da' ili 'ne':");
            var choice = Console.ReadLine().Trim().ToUpper();

            if (choice is "DA")
            {
                return true;
            }
            else if (choice is "NE")
            {
                return false;
            }
            OutputPainter("Nedopušten unos.", ConsoleColor.Red, ConsoleColor.Black);
            return ConfirmationCheck();
        }

        public static void OutputPainter(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
