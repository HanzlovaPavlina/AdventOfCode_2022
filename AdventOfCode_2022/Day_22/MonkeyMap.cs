using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_22 {

    class Walker {

        int row;
        int column;
        int direction;
        char[,] map;

        public int RowValue { get { return row+1; } }
        public int ColumnValue { get { return column+1; } }
        public int DirectionValue {
            get {
                switch (direction) {
                    case 'R': return 0;
                    case 'D': return 1;
                    case 'L': return 2;
                    case 'U': return 3;
                    default: return -1;
                }
            }
        }

        public Walker(int row, int col, int dir, char[,] map) {
            this.row = row;
            this.column = col;
            this.direction = dir;
            this.map = map;
        }

        public void Go(int steps) {
            while (steps > 0) {

                switch (direction) {
                    case 'R': GoRight(); break;
                    case 'L': GoLeft(); break;
                    case 'D': GoDown(); break;
                    case 'U': GoUp(); break;
                    default: break;
                    }
                steps--;
            }
        }

        public void Rotate(char rotation) {
            switch (direction) {
                case 'R': direction = rotation == 'R' ? 'D' : 'U'; break;
                case 'L': direction = rotation == 'R' ? 'U' : 'D'; break;
                case 'D': direction = rotation == 'R' ? 'L' : 'R'; break;
                case 'U': direction = rotation == 'R' ? 'R' : 'L'; break;
                default: break;
            }
        }

        void GoRight() {
            int newCol = column + 1;
            if (newCol == map.GetLength(1) || map[row, newCol] == ' ') {
                newCol = 0;
                while (map[row, newCol] == ' ') newCol++;
                }
            if (map[row, newCol] != '#') column = newCol;
        }

        void GoLeft() {
            int newCol = column - 1;
            if (newCol == -1 || map[row, newCol] == ' ') {
                newCol = map.GetLength(1) - 1;
                while (map[row, newCol] == ' ') newCol--;
                }
            if (map[row, newCol] != '#') column = newCol;
        }

        void GoDown() {
            int newRow = row + 1;
            if (newRow == map.GetLength(0) || map[newRow, column] == ' ') {
                newRow = 0;
                while (map[newRow, column] == ' ') newRow++;
                }
            if (map[newRow, column] != '#') row = newRow;
        }

        void GoUp() {
            int newRow = row - 1;
            if (newRow == -1 || map[newRow, column] == ' ') {
                newRow = map.GetLength(0) -1;
                while (map[newRow, column] == ' ') newRow--;
                }
            if (map[newRow, column] != '#') row = newRow;
        }
    }

    class MonkeyMap {

        char[,] map;
        List<int> steps;
        List<char> rotations;

        public MonkeyMap(string path) {

            steps = new List<int>();
            rotations = new List<char>();

            LoadMapAndMoves(path);
        }

        public int GetFinalPassword() {

            int startCol = 0;
            while (map[0, startCol] != '.') startCol++;
            Walker walker = new Walker(0, startCol, 'R', map);
            GoToEnd(walker, steps, rotations);

            return 1000 * walker.RowValue + 4 * walker.ColumnValue + walker.DirectionValue;
        }

        void LoadMapAndMoves(string path) {

            IEnumerable<string> lines = File.ReadLines(path);

            // get size of map
            int columnCount = 0;
            foreach (string line in lines) {
                if (line == "") break;
                if (line.Length > columnCount) columnCount = line.Length;
                }
            map = new char[lines.Count()-2, columnCount];

            // load map to array
            int row = 0;
            int col = 0;
            foreach (string line in lines) {
                if (line == "") break;

                col = 0;
                foreach (char pos in line.ToCharArray()) {
                    map[row, col] = pos;
                    col++;
                    }
                while(col < columnCount) {
                    map[row, col] = ' ';
                    col++;
                    }
                row++;
            }
            //PrintMap();

            // load moves
            var counts = Regex.Matches(lines.Last(), @"\d+");
            foreach (Match count in counts) steps.Add(Convert.ToInt32(count.Value));

            // load rotations
            var direstions = Regex.Matches(lines.Last(), @"[RL]");
            foreach (Match next in direstions) rotations.Add(Convert.ToChar(next.Value));
        }

        void GoToEnd(Walker walker, List<int> steps, List<char> rotations) {

            int stepsPos = 0;
            int rotationsPos = 0;

            while(stepsPos < steps.Count || rotationsPos < rotations.Count) {

                if(stepsPos < steps.Count) {
                    walker.Go(steps.ElementAt(stepsPos));
                    stepsPos++;
                }

                if(rotationsPos < rotations.Count) {
                    walker.Rotate(rotations.ElementAt(rotationsPos));
                    rotationsPos++;
                }
            }
        }

        void PrintMap() {
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++)
                    Console.Write(map[i, j]);
                Console.WriteLine();
                }
            }
        }
}
