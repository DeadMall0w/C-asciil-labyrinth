using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_labyrinth
{
    static class Input
    {

        static Labyrinthdata labyrinthdata;
        public static Labyrinthdata HandleConfigInput()
        {
            labyrinthdata = new Labyrinthdata(33);

            

            ConsoleKeyInfo input;

            bool inputValid = false;
            while (!inputValid)
            {
                Console.Clear();
                Console.WriteLine("'1' Play");
                Console.WriteLine("'2' Settings");
                Console.WriteLine("'3' Credit");
                Console.WriteLine("'4' Quit");

                input = Console.ReadKey();
                inputValid = true;
                switch (input.KeyChar)
                {
                    case '1': // play with basic settings
                        break;
                    case '2': // change settings
                        ChangeSettings();
                        inputValid = false;
                        break;
                    case '3':
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                    default:
                        inputValid = false;
                        break;
                };
            }



            return labyrinthdata;


        }

        public static void ChangeSettings()
        {
            ConsoleKeyInfo input;
            do
            {
                Console.Clear();
                ShowchangeableSettings();
                input = Console.ReadKey();

                Console.Clear();
                switch (input.KeyChar)
                {
                    case '1': // size of the map
                        ChangeSize();
                        break;
                }

            } while (input.KeyChar != '0');

        }

        static void ChangeSize()
        {
            Console.WriteLine($"Enter new size, current : {labyrinthdata.size}");
            labyrinthdata.size = GetNumberInput();



        }

        static void ShowchangeableSettings()
        {

            Console.WriteLine($"'1' size = {labyrinthdata.size}");
            Console.WriteLine($"'2' random exit = {labyrinthdata.randomExit}");
            Console.WriteLine($"'3' number of exit = {labyrinthdata.numberOfExit}");
            Console.WriteLine($"'4' number of coins = {labyrinthdata.numberOfCoins}");
            Console.WriteLine($"'5' all the map lit = {labyrinthdata.AllLit}");
            //Console.WriteLine($"'6' size = {labyrinthdata.size}");


            Console.WriteLine("'0' exit settings");    
        }

        static int GetNumberInput()
        {
            Console.Write(" = ");
            string input = Console.ReadLine();
            int number = -1;
            try
            {
                number = Math.Abs(int.Parse(input));
            }
            catch (Exception)
            {

                Console.WriteLine("Incorrect input, please retry");
            }


            return number;


        }
    }
}

//the art of coding
