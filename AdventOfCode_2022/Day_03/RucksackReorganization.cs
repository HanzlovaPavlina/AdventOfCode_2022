using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    class RucksackReorganization {

        public int GetSum() {
            return finalSum;
            }
        private char[] rucksackTypes = new char[300];
        private List<KeyValuePair<char, int>> priorities = new List<KeyValuePair<char, int>>()
        {
            new KeyValuePair<char, int>('a', 1),
            new KeyValuePair<char, int>('b', 2),
            new KeyValuePair<char, int>('c', 3),
            new KeyValuePair<char, int>('d', 4),
            new KeyValuePair<char, int>('e', 5),
            new KeyValuePair<char, int>('f', 6),
            new KeyValuePair<char, int>('g', 7),
            new KeyValuePair<char, int>('h', 8),
            new KeyValuePair<char, int>('i', 9),
            new KeyValuePair<char, int>('j', 10),
            new KeyValuePair<char, int>('k', 11),
            new KeyValuePair<char, int>('l', 12),
            new KeyValuePair<char, int>('m', 13),
            new KeyValuePair<char, int>('n', 14),
            new KeyValuePair<char, int>('o', 15),
            new KeyValuePair<char, int>('p', 16),
            new KeyValuePair<char, int>('q', 17),
            new KeyValuePair<char, int>('r', 18),
            new KeyValuePair<char, int>('s', 19),
            new KeyValuePair<char, int>('t', 20),
            new KeyValuePair<char, int>('u', 21),
            new KeyValuePair<char, int>('v', 22),
            new KeyValuePair<char, int>('w', 23),
            new KeyValuePair<char, int>('x', 24),
            new KeyValuePair<char, int>('y', 25),
            new KeyValuePair<char, int>('z', 26),
            new KeyValuePair<char, int>('A', 27),
            new KeyValuePair<char, int>('B', 28),
            new KeyValuePair<char, int>('C', 29),
            new KeyValuePair<char, int>('D', 30),
            new KeyValuePair<char, int>('E', 31),
            new KeyValuePair<char, int>('F', 32),
            new KeyValuePair<char, int>('G', 33),
            new KeyValuePair<char, int>('H', 34),
            new KeyValuePair<char, int>('I', 35),
            new KeyValuePair<char, int>('J', 36),
            new KeyValuePair<char, int>('K', 37),
            new KeyValuePair<char, int>('L', 38),
            new KeyValuePair<char, int>('M', 39),
            new KeyValuePair<char, int>('N', 40),
            new KeyValuePair<char, int>('O', 41),
            new KeyValuePair<char, int>('P', 42),
            new KeyValuePair<char, int>('Q', 43),
            new KeyValuePair<char, int>('R', 44),
            new KeyValuePair<char, int>('S', 45),
            new KeyValuePair<char, int>('T', 46),
            new KeyValuePair<char, int>('U', 47),
            new KeyValuePair<char, int>('V', 48),
            new KeyValuePair<char, int>('W', 49),
            new KeyValuePair<char, int>('X', 50),
            new KeyValuePair<char, int>('Y', 51),
            new KeyValuePair<char, int>('Z', 52)
        };
        private int finalSum = 0;


        public void LoadInput(string path) {

            string firstRucksack; string secondRucksack;

            //int i = 0;
            char type;

            foreach (string line in File.ReadLines(path)) {
                firstRucksack = line.Substring(0, line.Length / 2);
                secondRucksack = line.Substring(line.Length / 2, line.Length / 2);
                type = findType(firstRucksack, secondRucksack);
                finalSum += priorities.First(p => p.Key == type).Value;
                //rucksackTypes[i] = findType(firstRucksack, secondRucksack);
                //i++;
                //Console.WriteLine($"first: {firstRucksack}\nsecond: {secondRucksack}\n");
                }
            }

        private char findType(string first, string second) {

            foreach(char letter in first)
                if (second.Contains(letter)) return letter;
              
            return ' ';
        }
    }
}
