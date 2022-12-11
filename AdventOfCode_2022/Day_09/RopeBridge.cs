using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_09 {

    class RopeBridge {

        Grid map = new Grid(50, 50);

        //   PART 1
        //Coordinates[] rope = new Coordinates[2];
        //Coordinates[] lasts = new Coordinates[2];

        //   PART 2
        Coordinates[] rope = new Coordinates[10];
        Coordinates[] lasts = new Coordinates[10];

        public int GetTailVisitCount(string path) {
            LoadAndProcessMoves(path);
            int total = 0;

            foreach(List<Point> row in map.getRows())
                foreach(Point point in row) {
                    total += point.State == Status.Used ? 1 : 0;
                    }

            return total;
            }

        private void LoadAndProcessMoves(string path) {
            try {
                string[] nextMove = new string [2];
                for (int i = 0;i < rope.Length; i++) rope[i] = new Coordinates(25, 25);
                for (int i = 0; i < lasts.Length; i++) lasts[i] = new Coordinates(25, 25);
                map.getPoint(rope.Last().x, rope.Last().y).Use();


                foreach (string line in File.ReadLines(path)) {
                    nextMove = line.Split(' ');
                    string direction = nextMove[0];
                    int steps = Convert.ToInt32(nextMove[1]);

                    while (steps > 0) {
                        CheckMap(direction);
                        MoveTail(direction);
                        for (int i = 0; i < rope.Length - 1; i++) {
                            CheckAndMovePartOfRope(i, i+1);
                            }
                        map.getPoint(rope.Last().x, rope.Last().y).Use();
                        steps--;
                    }
                    Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 1: {rope[1].x},{rope[1].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 2: {rope[2].x},{rope[2].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 3: {rope[3].x},{rope[3].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 4: {rope[4].x},{rope[4].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 5: {rope[5].x},{rope[5].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 6: {rope[6].x},{rope[6].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 7: {rope[7].x},{rope[7].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 8: {rope[8].x},{rope[8].y}");
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 9: {rope[9].x},{rope[9].y}");
                    //Console.WriteLine("----------------------------------------------------------------------------------------------");
                    }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        private void CheckMap(string direction) {
            switch (direction) {
                case "R":
                    if (!map.isSpaceRight(rope[0]))
                        map.AddColumnRight();
                    break;
                case "L":
                    if (!map.isSpaceLeft(rope[0])) {
                        map.AddColumnLeft();
                        foreach (Coordinates c in rope)
                            c.x = c.x + 1;
                        }
                    break;
                case "U":
                    if (!map.isSpaceUp(rope[0])) {
                        map.AddRowUp();
                        foreach (Coordinates c in rope)
                            c.y = c.y + 1;
                        }
                    break;
                case "D":
                    if (!map.isSpaceDown(rope[0]))
                        map.AddRowDown();
                    break;
                default:
                    break;
                }
            }
        private void MoveTail(string direction) {
            lasts[0] = new Coordinates(rope[0].x, rope[0].y);

            switch (direction) {
                case "R":
                    rope[0].x = rope[0].x + 1;
                    break;
                case "L":
                    rope[0].x = rope[0].x - 1;
                    break;
                case "U":
                    rope[0].y = rope[0].y - 1;
                    break;
                case "D":
                    rope[0].y = rope[0].y + 1;
                    break;
                default:
                    break;
                }
            }

        private void CheckAndMovePartOfRope(int head, int tail) {
            if (Math.Abs(rope[tail].x - rope[head].x) > 1 || Math.Abs(rope[tail].y - rope[head].y) > 1) {
                lasts[tail] = new Coordinates(rope[tail].x, rope[tail].y);
                rope[tail] = new Coordinates(lasts[head].x, lasts[head].y);
                }
            }
        }
    }
