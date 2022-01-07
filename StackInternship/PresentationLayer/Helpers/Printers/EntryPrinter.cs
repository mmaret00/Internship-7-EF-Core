using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class EntryPrinter
    {
        public static void PrintPrimaryEntry(EntryDetails entryDetails)
        {
            if (entryDetails.AuthorsRole is UserRole.Intern)
            {
                StringHelper.OutputPainter($"Id: {entryDetails.Id}, Autor: {entryDetails.AuthorUserName} " +
                    $"({entryDetails.AuthorsRole}), Kategorija: {entryDetails.Department}"
                    , ConsoleColor.DarkCyan, ConsoleColor.Black);
            }
            if (entryDetails.AuthorsRole is UserRole.Organizer)
            {
                StringHelper.OutputPainter($"Id: {entryDetails.Id}, Autor: {entryDetails.AuthorUserName} " +
                    $"({entryDetails.AuthorsRole}), Kategorija: {entryDetails.Department}"
                    , ConsoleColor.DarkYellow, ConsoleColor.Black);
            }
            Console.WriteLine($"Objavljeno: {entryDetails.Published}");
            StringHelper.OutputPainter($"{entryDetails.Content}", ConsoleColor.Black, ConsoleColor.Gray);
            Console.Write($"{entryDetails.ViewCount} pregleda, glasovi: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{(char)24}:{entryDetails.UpvoteCount} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{(char)25}:{entryDetails.DownvoteCount}");
            Console.ResetColor();
            Console.WriteLine($" , {entryDetails.CommentCount} odgovora\n");
        }

        public static void PrintSecondaryEntry(EntryDetails entryDetails, EntryType entryType)
        {
            if (entryDetails.AuthorsRole is UserRole.Intern)
            {
                StringHelper.OutputPainter($"\tId: {entryDetails.Id}, Autor: {entryDetails.AuthorUserName} " +
                    $"({entryDetails.AuthorsRole})"
                    , ConsoleColor.DarkCyan, ConsoleColor.Black);
            }
            if (entryDetails.AuthorsRole is UserRole.Organizer)
            {
                StringHelper.OutputPainter($"\tId: {entryDetails.Id}, Autor: {entryDetails.AuthorUserName} " +
                    $"({entryDetails.AuthorsRole})"
                    , ConsoleColor.DarkYellow, ConsoleColor.Black);
            }
            Console.WriteLine($"\tObjavljeno: {entryDetails.Published}");
            Console.Write("\t");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"{entryDetails.Content}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"\n\t{entryDetails.ViewCount} pregleda, ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{(char)24}:{entryDetails.UpvoteCount} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{(char)25}:{entryDetails.DownvoteCount}");
            Console.ResetColor();
            if (entryType is EntryType.Answer)
            {
                Console.WriteLine($" , {entryDetails.CommentCount} komentara");
                return;
            }
            Console.WriteLine("");
        }
    }
}
