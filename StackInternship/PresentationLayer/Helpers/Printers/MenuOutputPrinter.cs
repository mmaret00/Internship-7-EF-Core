using DataLayer.Entities.Enums;
using DataLayer.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class MenuOutputPrinter
    {
        public static char PrintUsersMenuOutput()
        {
            Console.WriteLine($"Odaberite željenu vrstu ispisa:\n" +
                "1 - Svi korisnici\n" +
                "2 - Samo organizatori\n" +
                "3 - Samo interni\n" +
                "4 - Samo trusted useri\n" +
                "5 - Korisnici s više od određenog broja reputacijskih bodova\n" +
                "6 - Korisnici s manje od određenog broja reputacijskih bodova\n" +
                "0 - Povratak");

            return Reader.GetMenuChoice();
        }
        public static char ProfileMenuOutput()
        {
            Console.WriteLine($"\nOdaberite akciju:\n" +
                "1 - Promjena korisničkog imena\n" +
                "2 - Promjena lozinke\n" +
                "0 - Povratak na dashboard");

            return Reader.GetMenuChoice();
        }

        public static char DashboardMenuOutput(UserRole usersRole)
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Resursi\n" +
                "2 - Neodgovoreno\n" +
                "3 - Popularno\n" +
                "4 - Korisnici\n" +
                "5 - Moj profil");
            if (usersRole is UserRole.Organizer)
            {
                Console.WriteLine($"6 - Deaktivacija/reaktivacija korisnika");
            }
            Console.WriteLine("0 - Odjava");

            return Reader.GetMenuChoice();
        }
        public static char OrganizerSubmenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Privremeno deaktiviraj korisnikov profil\n" +
                "2 - Trajno deaktiviraj korisnikov profil\n" +
                "3 - Reaktiviraj deaktivirani profil\n" +
                "0 - Povratak na dashboard");

            return Reader.GetMenuChoice();
        }

        public static char DepartmentMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Dev\n" +
                "2 - Dizajn\n" +
                "3 - Marketing\n" +
                "4 - Multimedija\n" +
                "5 - Generalno\n" +
                "0 - Povratak na dashboard");

            return Reader.GetMenuChoice();
        }

        public static char EntryActionsMenuOutput(ListResourcesType listResourcesType)
        {
            Console.Write("Odaberite akciju:\n" +
                "1 - Upvote, " +
                "2 - Downvote, " +
                "3 - Dodavanje novog resursa, " +
                "4 - Odgovori na resurs / Komentiraj odgovor\n" +
                "5 - Uređivanje, " +
                "6 - Brisanje, ");
            if (listResourcesType is not ListResourcesType.Unanswered)
            {
                Console.Write("7 - Pregled komentara odgovora, ");
            }
            Console.Write("0 - Povratak\n");

            return Reader.GetMenuChoice();
        }

        public static char AnswerActionsMenuOutput()
        {
            Console.WriteLine("\nOdaberite akciju:\n" +
                "1 - Upvote, " +
                "2 - Downvote, " +
                "3 - Dodavanje novog komentara, " +
                "4 - Uređivanje, " +
                "5 - Brisanje, " +
                "0 - Povratak");

            return Reader.GetMenuChoice();
        }

        public static char EntryActionsSubmenuOutput()
        {
            Console.WriteLine("\nOdaberite akciju:\n" +
                "1 - Upvote, " +
                "2 - Downvote, " +
                "3 - Pregled komentara, " +
                "4 - Komentiraj, " +
                "5 - Uređivanje, " +
                "6 - Brisanje, " +
                "0 - Odustajanje");

            return Reader.GetMenuChoice();
        }

        public static char LoginMenuOutput()
        {
            Console.WriteLine($"Odaberite akciju:\n" +
                "1 - Prijavi se u račun\n" +
                "2 - Registracija novog korisnika\n" +
                "0 - Izlaz iz aplikacije");

            return Reader.GetMenuChoice();
        }
    }
}
