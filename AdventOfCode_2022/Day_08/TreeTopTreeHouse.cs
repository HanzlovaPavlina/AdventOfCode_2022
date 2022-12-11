using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_08 {
    class TreeTopTreeHouse {

        public TreeTopTreeHouse(string path) {
            LoadInput(path);
            }

        List<List<int>> treesMap = new List<List<int>>();

        public int getVisibleTreeCount() {

            int visibleTreeCount = 0;

            for(int i = 0; i < treesMap.Count; i++) 
                for (int j = 0; j < treesMap.ElementAt(i).Count; j++)
                    visibleTreeCount += isVisible(i, j);
                
            return visibleTreeCount;
            }

        public int getBestScenicScore() {
            int bestScore = 0;
            int helperScore = 0;

            for (int i = 1; i < treesMap.Count - 1; i++) {
                for (int j = 1; j < treesMap.ElementAt(i).Count - 1; j++) {
                    helperScore = getScore(i, j);
                    if (helperScore > bestScore)
                        bestScore = helperScore;
                    }
                }
            return bestScore;
            }

        private void LoadInput(string path) {
            try {
                int lineCount = 0;
                List<int> actualLine;

                foreach (string line in File.ReadLines(path)) {
                    treesMap.Add(new List<int>());
                    actualLine = treesMap.ElementAt(lineCount);
                    actualLine.AddRange(line.Select(c => Convert.ToInt32(c) - 48));
                    lineCount++;
                    }
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        private int isVisible(int x, int y) {
            int treeHeight = treesMap.ElementAt(y).ElementAt(x);
            
            if (isVisibleFromRight(treeHeight, x, y)) return 1;
            if (isVisibleFromLeft(treeHeight, x, y)) return 1;
            if (isVisibleFromTop(treeHeight, x, y)) return 1;
            if (isVisibleFromDown(treeHeight, x, y)) return 1;
            return 0;
            }

        private bool isVisibleFromRight(int height, int x, int y) {

            for (int i = x+1; i < treesMap.ElementAt(y).Count; i++)
                if (treesMap.ElementAt(y).ElementAt(i) >= height)
                    return false;
            return true;
            }

        private bool isVisibleFromLeft(int height, int x, int y) {
            for (int i = x-1; i >= 0; i--)
                if (treesMap.ElementAt(y).ElementAt(i) >= height)
                    return false;
            return true;
            }

        private bool isVisibleFromTop(int height, int x, int y) {
            for (int i = y-1; i >= 0; i--)
                if (treesMap.ElementAt(i).ElementAt(x) >= height)
                    return false;
            return true;
            }

        private bool isVisibleFromDown(int height, int x, int y) {
            for (int i = y+1; i < treesMap.Count; i++)
                if (treesMap.ElementAt(i).ElementAt(x) >= height)
                    return false;
            return true;
            }

        private int getScore(int x, int y) {
            int treeHeight = treesMap.ElementAt(y).ElementAt(x);

            int left = getScoreFromLeft(treeHeight, x, y);
            int right = getScoreFromRight(treeHeight, x, y);
            int top = getScoreFromTop(treeHeight, x, y);
            int down = getScoreFromDown(treeHeight, x, y);
            return left * right * top * down;
            }

        private int getScoreFromRight(int height, int x, int y) {
            for (int i = x + 1; i < treesMap.ElementAt(y).Count; i++)
                if (treesMap.ElementAt(y).ElementAt(i) >= height) 
                    return i-x;
            return treesMap.ElementAt(y).Count - 1 - x;
            }

        private int getScoreFromLeft(int height, int x, int y) {
            for (int i = x - 1; i >= 0; i--)
                if (treesMap.ElementAt(y).ElementAt(i) >= height)
                    return x - i;
            return x;
            }

        private int getScoreFromTop(int height, int x, int y) {
            for (int i = y - 1; i >= 0; i--)
                if (treesMap.ElementAt(i).ElementAt(x) >= height)
                    return y - i;
            return y;
            }
        private int getScoreFromDown(int height, int x, int y) {
            for (int i = y + 1; i < treesMap.Count; i++)
                if (treesMap.ElementAt(i).ElementAt(x) >= height)
                    return i - y;
            return treesMap.Count - 1 - y;
            }
        }
}
