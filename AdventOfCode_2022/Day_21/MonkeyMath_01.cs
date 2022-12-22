using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode_2022.Day_21 {

    enum State {
        FRESH = 0,
        DONE = 1
        }

    class Monkey {

        string name;
        public State state;
        BigInteger result;
        string firstMonkey;
        string secondMonkey;
        char operation;

        public string Name { get {return name;} }
        public BigInteger Result { get { return result; } }
        public char Operation { get { return operation; } }
        public string FirstMonkey { get { return firstMonkey; } }
        public string SecondMonkey { get { return secondMonkey; } }

        public Monkey(string name) {
            this.name = name;
            this.state = State.FRESH;
            }

        public Monkey(string name, BigInteger result) {
            this.name = name;
            this.result = result;
            this.state = State.DONE;
            }

        public Monkey(string name, string first, char op, string second) {
            this.name = name;
            this.firstMonkey = first;
            this.secondMonkey = second;
            this.operation = op;
            this.state = State.FRESH;
            }

        public void SaveResult(BigInteger res) {
            result = res;
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

        void LoadMonkeys(string path) {
            try {
                string[] properties;

                foreach (string line in File.ReadLines(path)) {
                    properties = line.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                    if (properties.Length == 2) {
                        monkeys.Add(new Monkey(properties[0], Convert.ToInt32(properties[1])));
                        }
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
