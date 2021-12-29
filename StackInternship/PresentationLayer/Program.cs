using DataLayer.Entities;
using DomainLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                var correctEntry = OutputService.LoginMenu();
                if (correctEntry) return;
            }
        }
    }
}
