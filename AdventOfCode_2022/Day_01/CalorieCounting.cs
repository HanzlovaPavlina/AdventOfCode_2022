using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    class CalorieCounting_01 {

        public int GetMaxCalories() {
            LoadInput();
            return maxCalories;
            }

        public int GetThreeMaxCalories() {
            GetMaxCalories();
            Array.Sort(calories);
            Array.Reverse(calories);
            // Console.WriteLine($"1: {calories[0]}, 2: {calories[1]}, 3: {calories[3]}");
            return calories[0] + calories[1] + calories[2];
        }

        private string path = "../../Day_01/input.txt";
        private int[] calories = new int[2000];
        private int elves = 0;
        private int maxCalories = 0;

        private void LoadInput() {

            foreach (string line in File.ReadLines(path)) {

                if (line == "") {
                    if (calories[elves] > maxCalories) maxCalories = calories[elves];
                    elves++;
                    continue;
                }

                calories[elves] += Convert.ToInt32(line);
                // Console.WriteLine($"i: {elves}, SUMA: {calories[elves]}, caried calories: {line}");
            }
        }
    }
}


            /*
             * try {
                var fs = File.OpenRead(path);
                Console.WriteLine(fs.CanRead);
                using (StreamReader sr = new StreamReader(File.OpenRead(path))) {
                    string line;
                    
                    while ((line = sr.ReadLine()) != null) {
                        calories[elves] = 0;
                        while ((line = sr.ReadLine()) != "\n") {
                            calories[elves] += Convert.ToInt32(line);
                            Console.WriteLine($"i: {elves}, SUMA: {calories[elves]}, caried calories: {line}");
                            }
                        }
                        if (calories[elves] > maxCalories) maxCalories = calories[elves];
                        elves++;
                    }
                }
            catch (Exception e) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            */
