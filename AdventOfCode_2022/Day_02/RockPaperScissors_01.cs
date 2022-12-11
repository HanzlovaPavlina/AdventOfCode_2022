using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_02 {
    class RockPaperScissors_01 {

        public int GetFinalScore(string path) {
            LoadAndProcessInput(path);
            return finalScore;
            }
        
        string[] gameShapes = new string[2];
        private int finalScore;

        Dictionary<char, Dictionary<char, int>> resultScores = new Dictionary<char, Dictionary<char, int>>(){
            {'X', new Dictionary<char, int>() {{'A', 3}, {'B', 0}, {'C', 6}}},
            {'Y', new Dictionary<char, int>() {{'A', 6}, {'B', 3}, {'C', 0}}},
            {'Z', new Dictionary<char, int>() {{'A', 0}, {'B', 6}, {'C', 3}}},
        };
        Dictionary<char, int> moveScores = new Dictionary<char, int>() { { 'X', 1 }, { 'Y', 2 }, { 'Z', 3 } };

        // get score for one game
        private int GetScore(int shape_1, int shape_2) {
            int resultScore =  resultScores.First(s => s.Key == shape_2).Value
                                           .First(s => s.Key == shape_1).Value;
            int moveScore = moveScores.First(s => s.Key == shape_2).Value;
            return resultScore + moveScore;
        }

        // load input and process one game per game
        // add scores into final score for all games
        private void LoadAndProcessInput(string path) {
            char shape_1, shape_2;
            int actualScore;

            foreach (string line in File.ReadLines(path)) {
                gameShapes = line.Split(' ');
                shape_1 = char.Parse(gameShapes[0]);
                shape_2 = char.Parse(gameShapes[1]);

                actualScore = GetScore(shape_1, shape_2);
                finalScore += actualScore;
                // Console.WriteLine($"shape_1: {gameShapes[0]}={shape_1}, shape_2: {gameShapes[1]}={shape_2}, score: {actualScore}");
            }
        }
    }
}
