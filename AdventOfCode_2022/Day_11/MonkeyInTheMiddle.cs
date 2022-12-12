using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_11 {

    class Operation {
        char operation;
        int number;

        public Operation(string op, string num) {
            operation = Convert.ToChar(op);
            if (num == "old\r") number = 0;
            else number = Convert.ToInt32(num);
            }

        public int Calculate(int old) {
            if (operation == '*') return old * (number == 0 ? old : number);
            if (operation == '+') return old + (number == 0 ? old : number);
            return 0;
            }
        }

    class Condition {
        int divisor;
        int trueMonkey;
        int falseMonky;

        public Condition(int n, int t, int f) {
            divisor = n;
            trueMonkey = t;
            falseMonky = f;
            }

        public int Test(int number) {
            return number % divisor == 0 ? trueMonkey : falseMonky;
            }
        }

    class Monkey {
        public int number;
        public List<int> startingItems;
        public Operation operation;
        public Condition condition;
        public int inspectedItems;

        public Monkey(int number) {
            startingItems = new List<int>();
            this.number = number;
            inspectedItems = 0;
            }
        }

    class Monkeys {

        List<Monkey> monkeys;

        public Monkeys(string path) {
            monkeys = new List<Monkey>();
            LoadMonkeys(path);
            }

        public int playWitItems() {
            int counting = 20;

            while (counting > 0) {
                foreach (Monkey monkey in monkeys) {
                    foreach (int item in monkey.startingItems) {
                        int newItem = monkey.operation.Calculate(item);
                        int nextMonkey = monkey.condition.Test(newItem);
                        monkeys[nextMonkey].startingItems.Add(newItem);
                        monkey.inspectedItems++;
                        }
                    monkey.startingItems.Clear();
                    }
                counting--;
                /*foreach (Monkey monkey in monkeys) {
                    Console.Write($"monkey: {monkey.number} >>> ");
                    foreach (int item in monkey.startingItems) Console.Write(item + ", ");
                    Console.WriteLine("/n");
                        }*/
                }
            
            var sorted = monkeys.OrderBy(m => m.inspectedItems).ToList();
            Console.WriteLine($"== After round {counting} ==");
            foreach (Monkey monkey in monkeys)
                Console.WriteLine($"Monkey {monkey.number} inspected items {monkey.inspectedItems} times.");
            return sorted.Last().inspectedItems* sorted.ElementAt(sorted.Count-2).inspectedItems;
            }

        private void LoadMonkeys(string path) {
            
            try {
                string readedFile = File.ReadAllText(path);
                string[] lines = readedFile.Split('\n');

                for (int i = 0; i < lines.Count(); i += 7) {
                    // get monkey number
                    var regex = new Regex(@"(?<=Monkey )\d");
                    Match m = regex.Match(lines[i]);
                    Monkey monkey = new Monkey(Convert.ToInt32(m.Value));

                    // get starting items
                    MatchCollection matches = Regex.Matches(lines[i+1], @"\d{1,2}");
                    foreach (Match match in matches) monkey.startingItems.Add(Convert.ToInt32(match.Value));

                    // get operation
                    string[] ops = lines[i+2].Split(' ');
                    monkey.operation = new Operation(ops[ops.Length - 2], ops.Last());

                    // conditions
                    monkey.condition = new Condition(Convert.ToInt32(lines[i+3].Split(' ').Last()),
                                                     Convert.ToInt32(lines[i+4].Split(' ').Last()),
                                                     Convert.ToInt32(lines[i+5].Split(' ').Last()));
                    // add monkey to group
                    monkeys.Add(monkey);
                    }
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }
            

        }
    }
