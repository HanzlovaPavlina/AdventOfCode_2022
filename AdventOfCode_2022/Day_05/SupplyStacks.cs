using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    class SupplyStacks {

        public string GetTopsOfStacks(string input) {

            LoadAndProcessInput(input);


            //what crate ends up on top of each stack?
            string tops = "";
            foreach (Stack<char> x in stacks) if (x != null)
                    tops += x.Peek();

            return tops;
            }
        
        private Stack<char>[] stacks = new Stack<char>[10];

        private void LoadAndProcessInput(string path) {
            int pos; bool fillStacks = true;
            MatchCollection matches;
            Stack<char>[] helperStacks = new Stack<char>[10];

            foreach (string line in File.ReadLines(path)) {

                // stacks are prepare, reverse them
                if (line.Count() == 0) {  
                    for (int i = 1; i < 10; i++) stacks[i] = new Stack<char>(helperStacks[i]);
                    fillStacks = false;
                    }
                // read and prepare supply stack 
                else if (fillStacks) { 
                    matches = Regex.Matches(line, @"[a-zA-Z]{1}");
                    foreach (Match m in matches) {
                        pos = (m.Index - 1) / 4 + 1;
                        // Console.WriteLine("'{0}' found at index {1} so its in stack {2}", m.Value, m.Index, pos);
                        if (helperStacks[pos] == null) helperStacks[pos] = new Stack<char>();
                        helperStacks[pos].Push(Convert.ToChar(m.Value));
                        }
                    }
                // read moves in stacks
                else { 
                    matches = Regex.Matches(line, @"\d{1,2}", RegexOptions.None, TimeSpan.FromSeconds(1));
                    // Console.WriteLine("'count {0}' from {1} to {2}", Convert.ToInt32(matches[0].Value), Convert.ToInt32(matches[1].Value), Convert.ToInt32(matches[2].Value));
                    MoveCratesByCrateMover_9001(Convert.ToInt32(matches[0].Value), Convert.ToInt32(matches[1].Value), Convert.ToInt32(matches[2].Value));
                    }
                }
            }

        // takes cranes one by one
        private void MoveCratesByCrateMover_9000(int count, int from, int to) {

            for (int i = 0; i < count; i++) {
                char x = stacks[from].Pop();
                stacks[to].Push(x);
                }
        }

        // takes cranes more cranes at once
        private void MoveCratesByCrateMover_9001(int count, int from, int to) {

            Stack<char> helper = new Stack<char>();
            for (int i = 0; i < count; i++) {
                char x = stacks[from].Pop();
                helper.Push(x);
                }

            for (int i = 0; i < count; i++) {
                char x = helper.Pop();
                stacks[to].Push(x);
            }
        }
    }
}
