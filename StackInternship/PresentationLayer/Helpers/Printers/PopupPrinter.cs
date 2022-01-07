using DataLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class PopupPrinter
    {
        public static void ReadKeyAndClear()
        {
            Console.ReadKey();
            Console.Clear();
        }

        public static void ReturnToDashboard()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na dashboard.");
            ReadKeyAndClear();
        }

        public static void ContinueToDashboard()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak na dashboard.");
            ReadKeyAndClear();
        }

        public static void ContinueToDepartments()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak na izbor kategorija.");
            ReadKeyAndClear();
        }

        public static void ReturnToDepartments()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na izbor kategorija.");
            ReadKeyAndClear();
        }

        public static void ReturnToPrintMenu()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na izbor ispisa.");
            ReadKeyAndClear();
        }

        public static void ReturnToResources()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na pregled resursa.");
            ReadKeyAndClear();
        }

        public static void SuccessfulEntry()
        {
            StringHelper.OutputPainter("Uspješno ste unijeli novu objavu.", ConsoleColor.Green, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void SuccessfulComment()
        {
            StringHelper.OutputPainter("Uspješno ste unijeli novi komentar.", ConsoleColor.Green, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void SuccessfulEdit()
        {
            StringHelper.OutputPainter("Uspješno ste uredili objavu.", ConsoleColor.Green, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void SuccessfulDelete()
        {
            StringHelper.OutputPainter("Uspješno ste obrisali objavu." , ConsoleColor.Green, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void ClickAnyKeyToContinue()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak.");
            ReadKeyAndClear();
        }

        public static void ClickAnyKeyToReturn()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak.");
            ReadKeyAndClear();
        }

        public static void ReturnToProfile()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na pregled profila.");
            ReadKeyAndClear();
        }

        public static void ReturnToDeactivationMenu()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na izbor deaktivacije/reaktivacije.");
            ReadKeyAndClear();
        }

        public static void GiveUpOnDeactivation()
        {
            Console.WriteLine("Odustali ste.");
            ReturnToDeactivationMenu();
        }

        public static void GiveUp()
        {
            Console.WriteLine("Odustali ste.");
            ClickAnyKeyToReturn();
        }


        public static void ReturnToLoginMenu()
        {
            Console.WriteLine("Povratak na početni zaslon.");
            ClickAnyKeyToReturn();
        }

        public static void AfterRegistration()
        {
            StringHelper.OutputPainter("Uspješno ste izradili novi račun." , ConsoleColor.Green, ConsoleColor.Black);
            ClickAnyKeyToContinue();
        }

        public static void UnallowedEntry(int max)
        {
            Console.Clear();
            StringHelper.OutputPainter($"Molimo unesite jedan od dopuštenih brojeva (0 - {max})\n" , ConsoleColor.Red, ConsoleColor.Black);
        }

        public static void NoComments()
        {
            StringHelper.OutputPainter("Objava nema komentara!", ConsoleColor.Red, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void VoteForOwnEntry()
        {
            Console.Clear();
            StringHelper.OutputPainter("Ne možete glasati za svoju objavu!" , ConsoleColor.Red, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void AlreadyVoted()
        {
            Console.Clear();
            StringHelper.OutputPainter("Već ste glasali za ovu objavu!", ConsoleColor.Red, ConsoleColor.Black);
            ReturnToResources();
        }

        public static void SuccessfulVote()
        {
            Console.Clear();
            StringHelper.OutputPainter("Uspješno ste glasali!" , ConsoleColor.Green, ConsoleColor.Black);
            ClickAnyKeyToContinue();
        }

        public static void NotEnoughPoints(int minimumPoints, EntryActionChoice vote)
        {
            Console.Clear();
            StringHelper.OutputPainter($"Nemate dovoljnih {minimumPoints} bodova, " +
                $"pa ne možete izvršiti {vote}!", ConsoleColor.Red, ConsoleColor.Black);
            ClickAnyKeyToContinue();
        }

        public static void UserNameTaken()
        {
            Console.Clear();
            StringHelper.OutputPainter("Već postoji osoba s ovim korisničkim imenom!", ConsoleColor.Red, ConsoleColor.Black);
            ReturnToLoginMenu();
        }

        public static void SameNameAsBefore()
        {
            Console.Clear();
            StringHelper.OutputPainter("Unijeli ste korisničko ime koje već imate!", ConsoleColor.Red, ConsoleColor.Black);
            ReturnToProfile();
        }

        public static void SuccessfulNameChange()
        {
            Console.Clear();
            StringHelper.OutputPainter("Uspješno ste promijenili ime!", ConsoleColor.Green, ConsoleColor.Black);
            ClickAnyKeyToContinue();
        }

        public static void SuccessfulPasswordChange()
        {
            StringHelper.OutputPainter("Uspješno ste promijenili lozinku!", ConsoleColor.Green, ConsoleColor.Black);
            ClickAnyKeyToContinue();
        }

        public static void NoEntriesMadeToday()
        {
            StringHelper.OutputPainter("Danas nije napravljena ni jedna objava!", ConsoleColor.Red, ConsoleColor.Black);
            ReturnToDashboard();
        }

        public static void NotEnoughPointsToComment()
        {
            Console.Clear();
            StringHelper.OutputPainter($"Budući da imate manje od 3 reputacijska boda, ne možete komentirati!", ConsoleColor.Red, ConsoleColor.Black);
            ClickAnyKeyToContinue();
        }
    }
}
