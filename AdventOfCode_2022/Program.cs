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

            //RockPaperScissors_02 newGame = new RockPaperScissors_02();
            //Console.WriteLine(newGame.GetFinalScore("../../Day_02/input.txt"));

            RucksackReorganization rucksacks = new RucksackReorganization();
            rucksacks.LoadInput("../../Day_03/input.txt");
            Console.WriteLine(rucksacks.GetSum());

            Console.ReadKey();
            
            }
        }
    }
