using KartuvesDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KartuvesDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new KartuvesContext())
            {
                Hangman.Kartuves();
            }

            //------------------------------------------
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

      
    }
}