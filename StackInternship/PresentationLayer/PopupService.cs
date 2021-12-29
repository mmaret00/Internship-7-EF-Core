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

        static public void ReturnToResources()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za povratak na pregled resursa.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void ClickAnyKeyToContinue()
        {
            Console.WriteLine("\nKliknite bilo koju tipku za nastavak.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void GiveUpOnLogin()
        {
            Console.Clear();
            Console.WriteLine("Odustali ste od prijave na račun.");
            PopupService.ReturnToLoginMenu();
        }

        static public void UserEnteredUnacceptableChoice()
        {
            Console.WriteLine("Unijeli ste nedopušten unos, molimo ponovite ga.");
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
            Console.WriteLine("Uspješno ste izradili novi račun.");
            ClickAnyKeyToContinue();
        }
    }
}
