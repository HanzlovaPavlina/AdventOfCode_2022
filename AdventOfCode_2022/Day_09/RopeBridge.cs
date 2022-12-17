using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_09 {

    class RopeBridge {

        private static int gridSize = 20;
        private static int ropeLength = 10; // for part_1 = 2, for part_2 = 10

        Grid map = new Grid(gridSize, gridSize);
        Coordinates[] rope = new Coordinates[ropeLength];
        Coordinates[] lasts = new Coordinates[ropeLength];

        public int GetTailVisitCount(string path) {
            LoadAndProcessMoves(path);
            int total = 0;

            foreach (List<Point> row in map.getRows()) {
                foreach (Point point in row) {
                    total += point.State == Status.Used ? 1 : 0;
                    }
                }
            drawMap();
            return total;
            }

        private void LoadAndProcessMoves(string path) {
            try {
                string[] nextMove = new string [2];
                // head of rope with other pars of rope including tail - start position
                for (int i = 0; i < rope.Length; i++) rope[i] = new Coordinates(gridSize/2-1, gridSize/2-1);
                // last positions of rope parts for moving - start position
                for (int i = 0; i < lasts.Length; i++) lasts[i] = new Coordinates(gridSize/2-1, gridSize/2-1);
                map.getPoint(lasts.Last().x, lasts.Last().y).Use();

                foreach (string line in File.ReadLines(path)) {

                    // read moves one by one
                    nextMove = line.Split(' ');
                    string direction = nextMove[0];
                    int steps = Convert.ToInt32(nextMove[1]);

                    while (steps > 0) { // how many steps in one direction
                        CheckMap(direction); //check if is possible go this way
                        MoveRope(direction); // mve tail of rope
                        for (int i = 0; i+1 < rope.Length; i++) {
                            // move every part of rope if it is necessary
                            CheckAndMovePartOfRope(i, i+1);  
                            }
                        map.getPoint(rope.Last().x, rope.Last().y).Use();
                        steps--;
                    }
                    //Console.WriteLine($"move: {direction} steps: {steps} head:{rope[0].x},{rope[0].y} 1: {rope[1].x},{rope[1].y}");
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
        private void MoveRope(string direction) {
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

        private void CheckAndMovePartOfRope(int first, int second) {
            if (Math.Abs(rope[second].x - rope[first].x) > 1 || Math.Abs(rope[second].y - rope[first].y) > 1) {
                lasts[second] = new Coordinates(rope[second].x, rope[second].y);
                rope[second] = new Coordinates(lasts[first].x, lasts[first].y);
                }
            }

        private void drawMap() {

            foreach (List<Point> row in map.getRows()) {
                foreach (Point point in row) {
                    Console.Write(point.State == Status.Used ? '#' : '.');
                    }
                Console.WriteLine();
                }
            }
        }
    }
