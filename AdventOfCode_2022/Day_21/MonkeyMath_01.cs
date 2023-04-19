using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode_2022.Day_21 {

    enum State {
        FRESH = 0,  // monkey doesn´t know result yet
        DONE = 1   // monkey knows result
        }

    class Monkey {

        public string Name { get; }
        public State state;
        public BigInteger Result { get; private set; }
        public string FirstMonkey { get; }
        public string SecondMonkey { get; }
        public char Operation { get; }

        public Monkey(string name) {
            this.Name = name;
            this.state = State.FRESH;
            }

        public Monkey(string name, BigInteger result) {
            this.Name = name;
            this.Result = result;
            this.state = State.DONE;
            }

        public Monkey(string name, string first, char op, string second) {
            this.Name = name;
            this.FirstMonkey = first;
            this.SecondMonkey = second;
            this.Operation = op;
            this.state = State.FRESH;
            }

        public void SaveResult(BigInteger res) {
            Result = res;
            }
        }

    class MonkeyMath_01 {

        List<Monkey> monkeys;

        public MonkeyMath_01(string path) {
            monkeys = new List<Monkey>();
            LoadMonkeys(path);
            }

        public BigInteger GetResult() {

            Monkey root = monkeys.First(m => m.Name == "root");
            return GetResult(root);
            }

        // load txt input and create list of monkeys
        void LoadMonkeys(string path) {
            try {
                string[] properties;

                foreach (string line in File.ReadLines(path)) {
                    properties = line.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                    // monkey: number
                    if (properties.Length == 2) {
                        monkeys.Add(new Monkey(properties[0], Convert.ToInt32(properties[1])));
                        }
                    // monkey: monkey1 operation monkey2
                    else {
                        monkeys.Add(new Monkey(properties[0], properties[1], Convert.ToChar(properties[2]), properties[3]));
                        }
                    }
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        // DFS algorithm for result searching
        BigInteger GetResult(Monkey monkey) {

            if (monkey.state != State.DONE) {

                Monkey first = monkeys.First(m => m.Name == monkey.FirstMonkey);
                Monkey second = monkeys.First(m => m.Name == monkey.SecondMonkey);

                BigInteger firstResult = GetResult(first);
                BigInteger secondResult = GetResult(second);
                
                monkey.SaveResult(Count(monkey.Operation, firstResult, secondResult));
                }

            monkey.state = State.DONE;
            return monkey.Result;
            }
        
        // performing mathematical operation
        public BigInteger Count(char op, BigInteger first, BigInteger second) {

            BigInteger result = 0;

            switch (op) {

                case '*':
                    result = first * second;
                    break;
                case '+':
                    result = first + second;
                    break;
                case '/':
                    result = first / second;
                    break;
                case '-':
                    result = first - second;
                    break;
                default:
                    break;
                }

            return result;
            }
        }
    }
