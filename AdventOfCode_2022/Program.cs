using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    class Program {
        static void Main(string[] args) {

            CalorieCounting_01 counting = new CalorieCounting_01();
            Console.WriteLine(counting.GetThreeMaxCalories());
            Console.ReadKey();
            
            }
        }
    }
