using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Text;
using Ascii_labyrinth;

namespace Ascii_Maze
{
    internal class Program
    {
        static int mapSize = 20;
        static int[,] map; // 0 : empty, 1 : wall, 2 coins, 10 empty ntD, 11 : wall ntD, 12 : coins ntD

        static int playerX = 1;
        static int playerY = 1;

        static int numberOfCoins = 5;
        static int numberOfCoinsCollected = 0;

        static Stopwatch stopwatch = new Stopwatch();

        static StringBuilder screenBuffer = new StringBuilder();

        static Labyrinthdata labyrinthdata;

        static void Main(string[] args)
        {
            Console.SetWindowSize(mapSize * 2 + 6, mapSize + 5); //! Only on windows !
            Console.SetBufferSize(mapSize * 2 + 6, mapSize + 5);
            // Console.BackgroundColor = ConsoleColor.Gray;

            labyrinthdata = Input.HandleConfigInput();
            map = LabyrinthGenerator.CreateLabyrinth(labyrinthdata);
            
            mapSize = labyrinthdata.size;
            numberOfCoins = labyrinthdata.numberOfCoins;



            DrawAllMap();

            stopwatch.Start();

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                HandleInput(keyInfo);
            }
        }

        static void HandleInput(ConsoleKeyInfo input)
        {
            int lastPlayerX = playerX;
            int lastPlayerY = playerY;

            switch (input.KeyChar)
            {
                case 'd':
                    playerY++;
                    break;
                case 'q':
                    playerY--;
                    break;
                case 'z':
                    playerX--;
                    break;
                case 's':
                    playerX++;
                    break;
                case 'a':
                    //stop the timer 
                    //Console.Clear();
                    return;
                default: 
                   //Console.WriteLine("DS : ←→ | ZS : ↑↓");
                    break;
            }

            if (playerX > mapSize-1 || playerY > mapSize-1) // player escape the labyrinth
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("\n /$$     /$$                                                                /$$\n|  $$   /$$/                                                               | $$\n \\  $$ /$$//$$$$$$  /$$   /$$       /$$  /$$  /$$  /$$$$$$  /$$$$$$$       | $$\n  \\  $$$$//$$__  $$| $$  | $$      | $$ | $$ | $$ /$$__  $$| $$__  $$      | $$\n   \\  $$/| $$  \\ $$| $$  | $$      | $$ | $$ | $$| $$  \\ $$| $$  \\ $$      |__/\n    | $$ | $$  | $$| $$  | $$      | $$ | $$ | $$| $$  | $$| $$  | $$          \n    | $$ |  $$$$$$/|  $$$$$$/      |  $$$$$/$$$$/|  $$$$$$/| $$  | $$       /$$\n    |__/  \\______/  \\______/        \\_____/\\___/  \\______/ |__/  |__/      |__/\n                                                                               ");
                return;

            }

            if (map[playerX, playerY] == 1)
            {
                playerX = lastPlayerX; 
                playerY = lastPlayerY;
            }
            else if(map[playerX, playerY] == 2){
                numberOfCoinsCollected++;
                map[playerX, playerY] = 0;
            }

            int playerRange = 2 + numberOfCoinsCollected;
            for (int x = -playerRange;  x <= playerRange; x++)
            {
                for (int y = -playerRange; y <= playerRange; y++)
                {
                    if (playerX + x < 0 || playerY + y < 0 || playerX + x > mapSize - 1 || playerY + y > mapSize - 1)
                    {
                        continue;
                    }
                    if (map[playerX + x, playerY + y] > 9)
                    {

                        map[playerX + x, playerY + y] -= 10;
                    }

                }

            }


            DrawAllMap();
        }



        static void DrawAllMap()
        {
            screenBuffer.Clear();
            screenBuffer.Append($"\u001b[38;5;3m");
            screenBuffer.Append($"  {stopwatch.ElapsedMilliseconds / 1000}s.  {numberOfCoinsCollected}/{numberOfCoins}    \n");

            for (int x = 0; x < mapSize; x++)
            {
                screenBuffer.Append(" ");

                for (int y = 0; y < mapSize; y++)
                {
                    if (x == playerX && y == playerY)
                    {
                        screenBuffer.Append($"\u001b[38;5;15m");
                        screenBuffer.Append($"\u001b[48;5;8m");
                        screenBuffer.Append("@@");
                        continue;
                    }
                    if (map[x, y] == 0) // Empty cell
                    {
                        screenBuffer.Append($"\u001b[38;5;15m");
                        screenBuffer.Append($"\u001b[48;5;8m");
                        screenBuffer.Append("::");
                    }
                    else if (map[x, y] == 1) // Wall
                    {
                        screenBuffer.Append($"\u001b[38;5;15m");
                        screenBuffer.Append($"\u001b[48;5;8m");
                        screenBuffer.Append("▓▓");
                    }
                    else if (map[x, y] == 2) // coins
                    {
                        screenBuffer.Append($"\u001b[38;5;0m");
                        screenBuffer.Append($"\u001b[48;5;03m");
                        screenBuffer.Append("$$");
                    }
                    else // space not yet discovered 
                    {
                        screenBuffer.Append($"\u001b[38;5;15m");
                        screenBuffer.Append($"\u001b[48;5;0m");
                        screenBuffer.Append("  ");
                    }

                }

                screenBuffer.AppendLine();

            }


            Console.SetCursorPosition(0, 0);
            Console.Write(screenBuffer);
        }
    }
}

/* 

░
▒ 
▓
█ 

*/  