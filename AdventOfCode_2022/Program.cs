using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    class Program {
        static void Main(string[] args) {

            //CalorieCounting_01 counting = new CalorieCounting_01();
            //Console.WriteLine(counting.GetThreeMaxCalories());

            //RockPaperScissors_01 newGame = new RockPaperScissors_01();
            //Console.WriteLine(newGame.GetFinalScore("../../Day_02/input.txt"));

            //RucksackReorganization_02 rucksacks = new RucksackReorganization_02();
            //Console.WriteLine(rucksacks.GetSum("../../Day_03/input.txt"));

            CampCleanup camp = new CampCleanup();
            Console.WriteLine(camp.GetCleanUpFullyContains("../../Day_04/input.txt"));

            Console.ReadKey();
            }
        }
    }
