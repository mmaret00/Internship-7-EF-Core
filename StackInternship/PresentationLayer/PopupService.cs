using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class PopupService
    {
        static public void ReturnToMenu()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na glavni izbornik.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToDashboard()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na dashboard.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ContinueToDashboard()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak na dashboard.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ContinueToDepartments()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak na izbor kategorija.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToDepartments()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na izbor kategorija.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToPrintMenu()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na izbor ispisa.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToResources()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na pregled resursa.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void SuccessfulEntry()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uspješno ste unijeli novi resurs.");
            Console.ResetColor();
            ReturnToResources();
        }

        static public void SuccessfulEdit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uspješno ste uredili resurs.");
            Console.ResetColor();
            ReturnToResources();
        }

        static public void SuccessfulDelete()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uspješno ste obrisali resurs.");
            Console.ResetColor();
            ReturnToResources();
        }

        static public void ClickAnyKeyToContinue()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ClickAnyKeyToReturn()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToProfile()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na pregled profila.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToDeactivationMenu()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na izbor deaktivacije/reaktivacije.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void GiveUp()
        {
            Console.Clear();
            Console.WriteLine("Odustali ste.");
            ClickAnyKeyToReturn();
        }

        static public void UserEnteredUnacceptableChoice()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unijeli ste nedopušten unos, molimo ponovite ga.");
            Console.ResetColor();
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak izbora.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ReturnToLoginMenu()
        {
            Console.WriteLine("Povratak na početni zaslon.");
            ClickAnyKeyToContinue();
        }

        static public void AfterRegistration()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uspješno ste izradili novi račun.");
            Console.ResetColor();
            ClickAnyKeyToContinue();
        }
    }
}
