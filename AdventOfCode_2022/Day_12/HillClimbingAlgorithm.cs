using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_12 {

    enum State {
        FRESH = 0,
        OPEN = 1,
        CLOSED = 3
        }

    class Hill {

        char height;
        public List<Hill> neighbours;
        
        // for BFS
        public State state;
        public Hill before;
        public int distance;

        public Hill(char h) {
            this.height = h;

            // for BFS
            neighbours = new List<Hill>();
            state = State.FRESH;
            }

        public char getHeight() { return height; }

        public void AddNeighboringHill(Hill neighbour) {
            neighbours.Add(neighbour);
            }

        public bool CanBeClimbed(Hill from) {
            if (height <= from.height + 1) return true;
            return false;
            }
        }

    class HillClimbingAlgorithm {

        Hill[,] hillsMap;
        int height = 0;
        int width = 0;
        Hill start;
        Hill end;

        public HillClimbingAlgorithm(string path) {
            LoadMap(path);
            }

        // BFS
        public int GetShortestWayToEnd() {

            Queue<Hill> front = new Queue<Hill>();
            start.state = State.OPEN;
            start.distance = 0;
            front.Enqueue(start);

            while(front.Count != 0) {
                Hill nextHill = front.Dequeue();
                foreach(Hill neighbour in nextHill.neighbours) {
                    if(neighbour.state == State.FRESH && neighbour.CanBeClimbed(nextHill)) {
                        neighbour.state = State.OPEN;
                        neighbour.distance = nextHill.distance + 1;
                        neighbour.before = nextHill;
                        front.Enqueue(neighbour);
                        }
                    }
                nextHill.state = State.CLOSED;
                }
            WriteClimbingPaths("../../Day_12/output.txt");
            return end.distance;
            }

        // BFS
        public int GetShortestWayFromEnd() {

            Queue<Hill> front = new Queue<Hill>();
            end.state = State.OPEN;
            end.distance = 0;
            front.Enqueue(end);

            while (front.Count != 0) {
                Hill nextHill = front.Dequeue();
                foreach (Hill neighbour in nextHill.neighbours) {
                    if (neighbour.state == State.FRESH && nextHill.CanBeClimbed(neighbour)) {
                        neighbour.state = State.OPEN;
                        neighbour.distance = nextHill.distance + 1;
                        neighbour.before = nextHill;
                        front.Enqueue(neighbour);
                        }
                    }
                nextHill.state = State.CLOSED;
                }

            WriteClimbingPaths("../../Day_12/outputShortestA.txt");

            int shortest = Int32.MaxValue;
            foreach (Hill hill in hillsMap)
                if (hill.getHeight() == 'a' && hill.distance < shortest && hill.distance > 0)
                    shortest = hill.distance;
                return shortest;
        }

        void LoadMap(string path) {

            try {
                foreach (string line in File.ReadLines(path)) {
                    width = line.Length;
                    height++;
                }

                hillsMap = new Hill [height, width];
                int row = 0; int col = 0;

                // loading hills with heights, finding Start and End
                foreach(string line in File.ReadLines(path)) {
                    foreach(char next in line.ToCharArray()) {
                        if (next == 'S') {
                            hillsMap[row, col] = new Hill('a');
                            start = hillsMap[row, col];
                            }
                        else if (next == 'E') {
                            hillsMap[row, col] = new Hill('z');
                            end = hillsMap[row, col];
                            }
                        else hillsMap[row, col] = new Hill(next);
                        col++;
                        }
                    row++; col = 0;
                    }

                // finding all eighboring for each hill
                row = 0; col = 0;
                for(int j = 0; j < height; j++) {
                    for (int i = 0; i < width; i++) { 
                        if (j - 1 >= 0) hillsMap[j, i].AddNeighboringHill(hillsMap[j - 1, i]);
                        if (j + 1 < height) hillsMap[j, i].AddNeighboringHill(hillsMap[j + 1, i]);
                        if (i - 1 >= 0) hillsMap[j, i].AddNeighboringHill(hillsMap[j, i - 1]);
                        if (i + 1 < width) hillsMap[j, i].AddNeighboringHill(hillsMap[j, i + 1]);
                        }
                    }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
            }
        }

        public void WriteClimbingPaths(string path) {

            string fileText = "";
            int printHeight = 0;

            foreach (Hill h in hillsMap) {
                if (h == start) fileText += "   S";
                else if (h == end) fileText += "   E";
                else fileText += h.distance.ToString("D4");
                fileText += " ";
                printHeight++;
                if (printHeight % width == 0) fileText += "\r\n";
                }
                File.WriteAllText(path, fileText, Encoding.Unicode);
            }
        }
    }

