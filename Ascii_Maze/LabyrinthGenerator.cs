using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascii_labyrinth
{
    static class LabyrinthGenerator
    {
        static int[,] map;
        static Labyrinthdata data;
        static Cell[,] cells;

        public static int[,] CreateLabyrinth(Labyrinthdata _labyrinthdata)
        {
            data = _labyrinthdata;
            map = new int[data.size, data.size];
            PopulateMap();
            MakePath();
            AddPath();
            MakeExit();
            SpawnCoins();
            //create labyrinth
            //-DFS
            //-Cell to remove
            //make The exit
            //coins
            //test



            return map;
        }

        static void PopulateMap()
        {
            cells = new Cell[data.size / 2, data.size / 2];
            int count = 0;
            for (int x = 0; x < data.size; x++)
            {
                for (int y = 0; y < data.size; y++)
                {
                    if ((x + 1) % 2 == 0 && (y + 1) % 2 == 0)
                    {
                        cells[x / 2, y / 2] = new Cell(x / 2, y / 2);
                        map[x, y] = 0;
                        count += 1;
                    }
                    else
                    {
                        map[x, y] = 11;
                    }
                }
            }
        }

        /*static void ShowMap()
        {
            Console.Clear();
            for (int x = 0; x < data.size; x++)
            {
                for (int y = 0; y < data.size; y++)
                {
                    if (map[x,y] == 0 || map[x,y] == 10)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write("▓▓");
                    }
                }
                Console.WriteLine("");
            }
             Console.ReadKey();
        }*/

        static void MakePath()
        {
            List<Cell> pile = new List<Cell>();

            int x = 0;
            int y = 0;

            Random rand = new Random();
            do
            {
                //ShowMap();
                Cell[] neighbourCells = GetNeighbourCell(x, y);


                if (neighbourCells.Length > 0)
                {
                    int lastX = x;
                    int lastY = y;

                    Cell nextCell = neighbourCells[rand.Next(0, neighbourCells.Length)];

                    pile.Add(nextCell);

                    x = nextCell.localX;
                    y = nextCell.localY;

                    cells[x, y].isExplored = true;


                    int _x = (lastX - x);
                    int _y = (lastY - y);

                    map[_x + x * 2 + 1, _y + y * 2 + 1] = 10;
                    map[x * 2 + 1, y * 2 + 1] = 10;


                }
                else
                {
                    Cell cell = pile[pile.Count - 1];
                    pile.RemoveAt(pile.Count - 1);

                    x = cell.localX;
                    y = cell.localY;
                }
            } while (pile.Count > 0);
        }

        static void AddPath()
        {
            int nbrOfSecondPAth = 175;
            Random rand = new Random();
            for (int i = 0;  i < nbrOfSecondPAth; i++)
            {
                int x = rand.Next(1, data.size / 2-1);
                int y = rand.Next(1, data.size / 2-1);

                int randomMove = rand.Next(0,5);
                int randX = rand.Next(-1, 2);
                int randY = rand.Next(-1, 2);
                if (randomMove <= 2)
                {
                    randY = 0;
                }
                else
                {
                    randX = 0;
                }

                map[randX + x * 2 + 1, randY + y * 2 + 1] = 10;
                map[x * 2 + 1, y * 2 + 1] = 10;

            }

        }

        static void MakeExit()
        {
            Random rand = new Random();
            for (int i = 0; i < data.numberOfExit; i++)
            {
                int pos = rand.Next(0, data.size / 2);
                if (rand.Next(0, 11) > 5)
                {
                    map[pos * 2 + 1, data.size - 1] = 10;
                }
                else
                {
                    map[data.size - 1, pos * 2 + 1] = 10;

                }
            }
        }


        static void SpawnCoins()
        {
            Random rand = new Random();
            for (int i = 0; i < data.numberOfCoins; i++)
            {
                int xPos = rand.Next(0, data.size / 2);
                int yPos = rand.Next(0, data.size / 2);

                map[xPos * 2 + 1, yPos * 2 + 1] = 2;
            }
        }

        static Cell[] GetNeighbourCell(int x, int y)
        {
            List<Cell> neightbourCell = new List<Cell>();
            if (x > 0)
            {
                if (!cells[x - 1, y].isExplored)
                    neightbourCell.Add(cells[x - 1, y]);
            }
            if (y > 0)
            {
                if (!cells[x, y - 1].isExplored)
                    neightbourCell.Add(cells[x, y - 1]);
            }
            if (x < data.size / 2 - 1)
            {
                if (!cells[x + 1, y].isExplored)
                    neightbourCell.Add(cells[x + 1, y]);
            }
            if (y < data.size / 2 - 1)
            {
                if (!cells[x, y + 1].isExplored)
                    neightbourCell.Add(cells[x, y + 1]);
            }
            return neightbourCell.ToArray();
        }


        struct Cell
        {
            public int localX;
            public int localY;

            public bool isExplored = false;

            public Cell(int xPos, int yPos)
            {
                localX = xPos;
                localY = yPos;
            }
        }

        /*struct Cell
        {
            public 
        }*/
    }

    public struct Labyrinthdata
    {
        public int size = 33;
        //x,y

        public bool randomExit = true; //
        public int numberOfExit = 1;

        public int numberOfCoins = 5;


        public bool AllLit = false; //
        public bool SeeAllMap = false; //

        public Labyrinthdata(int _size)
        {
            size = _size;
        }

    }
}
