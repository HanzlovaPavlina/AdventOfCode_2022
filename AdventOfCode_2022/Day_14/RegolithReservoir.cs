using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_14 {
    class RegolithReservoir {

        char[,] reservoir;
        int width = 400;  
        int height = 170;  
        Coordinates entry;
        int count;
        int maxDeep;
        int actualDeep;

        public RegolithReservoir(string path) {
            reservoir = new char[height, width];
            for(int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++)
                    reservoir[i, j] = '.';
                }

            LoadReservoir(path);
            entry = new Coordinates(width / 2, 0);
            reservoir[entry.y, entry.x] = '+';

            DrawReservoir();
            }

        public int PourSand() {

            actualDeep = 0;
            while (actualDeep < maxDeep) FallSand(entry);
            //DrawReservoir();

            return count;
            }

        public int PourAndFullSand() {

            actualDeep = 0;
            for (int i = 0; i < width; i++) reservoir[maxDeep + 2, i] = '#';

            while (reservoir[entry.y, entry.x] != 'o') FallSand(entry);
            //DrawReservoir();

            return count;
            }

        private void LoadReservoir(string path) {

            try {
                maxDeep = 0;
                foreach (string line in File.ReadLines(path)) {
                    MatchCollection matches = Regex.Matches(line, @"[0-9]*,[0-9]*");
                    
                    string[] coordinatesFrom = matches[0].Value.Split(',');
                    Coordinates from = new Coordinates(Convert.ToInt32(coordinatesFrom[0]) - (500-width/2), Convert.ToInt32(coordinatesFrom[1]));

                    for (int i = 1; i < matches.Count; i++) {
                        string[] coordinatesTo = matches[i].Value.Split(',');
                        Coordinates to = new Coordinates(Convert.ToInt32(coordinatesTo[0]) - (500 - width / 2), Convert.ToInt32(coordinatesTo[1]));
                        if (maxDeep < to.y) maxDeep = to.y;
                        //Console.WriteLine($"X: {to.x}, Y: {to.y}");

                        for (int y = from.y; y <= to.y; y++) reservoir[y, from.x] = '#';
                        for (int y = to.y; y <= from.y; y++) reservoir[y, to.x] = '#';
                        for (int x = from.x; x <= to.x; x++) reservoir[from.y, x] = '#';
                        for (int x = to.x; x <= from.x; x++) reservoir[to.y, x] = '#';
                        from = to;
                        }
                    }
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        private void FallSand(Coordinates from) {

            if (from.y+1 == height) return;

            Coordinates to;
            if (reservoir[from.y + 1, from.x] == '.') to = new Coordinates(from.x, from.y + 1);
            else if (reservoir[from.y + 1, from.x - 1] == '.') to = new Coordinates(from.x - 1, from.y + 1);
            else if (reservoir[from.y + 1, from.x + 1] == '.') to = new Coordinates(from.x + 1, from.y + 1);
            else {
                count++;
                reservoir[from.y, from.x] = 'o';
                return;
                }

            reservoir[from.y, from.x] = '.';
            reservoir[to.y, to.x] = 'o';
            actualDeep = to.y;

            FallSand(to);
            }

        private void DrawReservoir() {
            // draw loaded input to console
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Console.Write(reservoir[y, x]);
                    }
                Console.WriteLine();
                }
            }
        }
    }

