using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer
{
    static class Program
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
