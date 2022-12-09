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

            //CampCleanup camp = new CampCleanup();
            //Console.WriteLine(camp.GetCleanUpFullyContains("../../Day_04/input.txt"));

            //SupplyStacks stacks = new SupplyStacks();
            //Console.WriteLine(stacks.GetTopsOfStacks("../../Day_05/input.txt"));

            //TuningTrouble trouble = new TuningTrouble();
            //Console.WriteLine(trouble.GetStartOfPacketMarker("../../Day_06/input.txt"));

            MyDevice device = new MyDevice("../../Day_07/input.txt");
            Console.WriteLine("TOTAL >> " + device.SolvePart_2());

            Console.ReadKey();
            }
        }
    }
