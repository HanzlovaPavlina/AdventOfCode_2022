using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    class RockPaperScissors_02 {

        public int GetFinalScore(string path) {
            LoadInput(path);
            return finalScore;
            }

        int[] games = new int[2500];
        string[] gameShapes = new string[2];
        int[,] scoreTable = new int[3, 3] { { 3,4,8 }, { 1,5,9 }, { 2,6,7 } };
        private int finalScore;

        private int GetScore(int shape_1, int shape_2) {
            return scoreTable[shape_1, shape_2];
            }

        private void LoadInput(string path) {
            int shape_1, shape_2, actualScore;

            foreach (string line in File.ReadLines(path)) {
                gameShapes = line.Split(' ');
                shape_1 = (char.Parse(gameShapes[0])) - 65;
                shape_2 = (char.Parse(gameShapes[1])) - 88;

                actualScore = GetScore(shape_1, shape_2);
                finalScore += actualScore;
                Console.WriteLine($"shape_1: {gameShapes[0]}={shape_1}, end: {gameShapes[1]}={shape_2}, score: {actualScore}");
                }
        }
    }
}
