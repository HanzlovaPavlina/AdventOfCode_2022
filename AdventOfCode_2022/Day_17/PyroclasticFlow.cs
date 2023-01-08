using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_17 {

    class Moves {

        List<int> direcions;
        int actualPos;

        public Moves() {
            direcions = new List<int>();
            actualPos = -1;
            }

        public void Add(char directon) {
            direcions.Add(directon == '<' ? -1 : 1);
            }

        public int Next() {
            if (++actualPos == direcions.Count()) actualPos = 0;
            return direcions.ElementAt(actualPos);
            }

        public int Actual() {
            return direcions.ElementAt(actualPos);
            }

        public int GetActualPositon() {
            return actualPos;
            }
        }
    class PyroclasticFlow {

        #region Rock
        static int[][,] Rock = new int[5][,]
        {
            new int[,] // ----
            {
                { 1, 1, 1, 1 }
            },
            new int[,] // +
            {
                { 0, 1, 0 },
                { 1, 1, 1 },
                { 0, 1, 0 }
            },
            new int[,] // J
            {
                { 0, 0, 1 },
                { 0, 0, 1 },
                { 1, 1, 1 }
            },
            new int[,] // I
            {
                { 1 },
                { 1 },
                { 1 },
                { 1 }
            },
            new int[,] // O
            {
                { 1, 1 },
                { 1, 1 }
            }
        };
        #endregion

        Moves moves;
        List<int[]> chamber;

        public PyroclasticFlow(string path) {
            moves = new Moves();
            chamber = new List<int[]>();
            for (int j = 0; j < 3; j++) chamber.Add(new int[7]);
            LoadMoves(path);
            }

        public int GetTowerOfRocksHeigth(int count) {

            int total = MoveRocks(count);
            return total;
            }

        void LoadMoves(string path) {
            try {
                using (StreamReader sr = new StreamReader(path)) {

                    while (sr.Peek() >= 0) {
                        moves.Add((char)sr.Read());
                    }
                }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        int MoveRocks(int rocksCount) {

            int[,] newRock;
            Coordinates position;
            int towerHeight = 0;

            for (int i = 0; i < rocksCount; i++) {

                newRock = Rock[i % 5];
                // add new rows to chamber >>> tower + 3 units + rock
                int startHeigth = towerHeight + 3 + newRock.GetLength(0);
                int newRows = startHeigth - chamber.Count;
                for (int j = 0; j < newRows; j++) chamber.Add(new int[7]); // row positions >>> 0123456
                position = new Coordinates(2, startHeigth-1);

                while (true) {

                    // jet of gas pushes rock, if it is posible
                    if (!Collides(new Coordinates(position.x + moves.Next(), position.y), newRock)) {
                        position.x += moves.Actual();
                        }

                    // try fall 1 unit or rest
                    if (Collides(new Coordinates(position.x, position.y - 1), newRock)) {
                        Rest(position, newRock);
                        if(position.y + 1 > towerHeight) towerHeight = position.y + 1;
                        break; 
                        }
                    else position.y--;
                    }
                //DrawChamber(towerHeight);
                }
            return towerHeight;
            }

        bool Collides(Coordinates pos, int[,] rock) {
            if (pos.y - (rock.GetLength(0) - 1) < 0) return true;
            if (pos.x < 0 || pos.x + rock.GetLength(1) > 7) return true;

            for (int y = rock.GetLength(0) - 1; y >= 0 ; y--)
                for (int x = rock.GetLength(1) - 1; x >= 0; x--) {
                    if (rock[y, x] == 0) continue;
                    if (chamber.ElementAt(pos.y - y)[pos.x + x] == 1) return true;
                    }

            return false;
            }

        void Rest(Coordinates pos, int[,] rock) {
            for (int y = 0; y < rock.GetLength(0); y++)
                for (int x = 0; x < rock.GetLength(1); x++) {
                    if (rock[y, x] == 0) continue;
                    chamber.ElementAt(pos.y - y)[pos.x + x] = rock[y, x];
                    }
            }

        void DrawChamber(int height) {
            for (int y = chamber.Count-1; y >= 0; y--) {
                for (int x = 0; x < 7; x++) {
                    Console.Write(chamber.ElementAt(y)[x] == 0 ? '.' : '#');
                    }
                Console.WriteLine();
                }
            Console.WriteLine("Actual move: " + moves.Actual() + ", pos: " + moves.GetActualPositon());
            Console.WriteLine("tower heigth: " + height);
            Console.WriteLine("---------------------------------------");
            }
        }
    }
