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

            //MyDevice device = new MyDevice("../../Day_07/input.txt");
            //Console.WriteLine("TOTAL >> " + device.SolvePart_2());

            //TreeTopTreeHouse treeHouse = new TreeTopTreeHouse("../../Day_08/input.txt");
            //Console.WriteLine(treeHouse.getVisibleTreeCount());
            //Console.WriteLine(treeHouse.getBestScenicScore());

            //Day_09.RopeBridge rope = new Day_09.RopeBridge();
            //Console.WriteLine(rope.GetTailVisitCount("../../Day_09/input.txt"));

            //Day_10.Signal_2 signal = new Day_10.Signal_2("../../Day_10/input.txt");
            //Console.WriteLine(signal.getStrength());

            //Day_10.Signal_2 signal = new Day_10.Signal_2("../../Day_10/input.txt");
            //Console.WriteLine(signal.getStrength());

            //Day_11.Monkeys monkeys = new Day_11.Monkeys("../../Day_11/testInput.txt");
            //Console.WriteLine(monkeys.playWitItems());

            //Day_12.HillClimbingAlgorithm hills = new Day_12.HillClimbingAlgorithm("../../Day_12/input.txt");
            //Console.WriteLine(hills.GetShortestWayFromEnd());

            //Day_13.DistressSignal signal = new Day_13.DistressSignal("../../Day_13/testInput.txt");
            //Console.WriteLine();

            //Day_14.RegolithReservoir cave = new Day_14.RegolithReservoir("../../Day_14/input.txt");
            //Console.WriteLine(cave.PourSand());
            //Console.WriteLine(cave.PourAndFullSand());

            /*
             * Day_15.BeaconExclusionZone zone_1 = new Day_15.BeaconExclusionZone("../../Day_15/test.txt");
            Console.WriteLine("Part_1 test data: " + zone_1.OccupiedPositionsCountAtRow(10));
            Console.WriteLine("Part_2 test data: " + zone_1.GetTuningFrequency(0, 20));

            Day_15.BeaconExclusionZone zone_2 = new Day_15.BeaconExclusionZone("../../Day_15/input.txt");
            Console.WriteLine("Part_1 my input: " + zone_2.OccupiedPositionsCountAtRow(2000000));
            Console.WriteLine("Part_2 my input: " + zone_2.GetTuningFrequency(0, 4000000)); 
            */

            //Day_16.ProboscideaVolcanium cave = new Day_16.ProboscideaVolcanium("../../Day_16/testInput.txt");
            //Console.WriteLine(cave.GetMaxRealesedPressure(30));

            //Day_17.PyroclasticFlow tetris = new Day_17.PyroclasticFlow("../../Day_17/input.txt");
            //Console.WriteLine("After 2022 rocks: " + tetris.GetTowerOfRocksHeigth(1000000000));

            /*
             * Day_18.BigCube cube_test = new Day_18.BigCube("../../Day_18/test.txt");
            Console.WriteLine("Part_1 test data: " + cube_test.GetSurfaceArea());
            Console.WriteLine("Part_2 test data: " + cube_test.GetSurfaceAreaWithoutBubble());

            Day_18.BigCube cube = new Day_18.BigCube("../../Day_18/input.txt");
            Console.WriteLine("Part_1 input: " + cube.GetSurfaceArea());
            Console.WriteLine("Part_2 input: " + cube.GetSurfaceAreaWithoutBubble());
            */

            //Day_20.GrovePositioningSystem system = new Day_20.GrovePositioningSystem("../../Day_20/input.txt", 2);
            //Console.WriteLine("Part_2: " + system.GetGroveCoordinates());

            //Day_21.MonkeyMath_02 match = new Day_21.MonkeyMath_02("../../Day_21/test.txt");
            //Console.WriteLine("Part_2 test data: " + match.GetResult());

            //Day_21.MonkeyMath_02 match_input = new Day_21.MonkeyMath_02("../../Day_21/input.txt");
            //Console.WriteLine("Part_2 input data: " + match_input.GetResult());

            Day_22.MonkeyMap map = new Day_22.MonkeyMap("../../Day_22/input.txt");
            Console.WriteLine("Part_1: " + map.GetFinalPassword());

            Console.ReadKey();
            }
        }
    }
