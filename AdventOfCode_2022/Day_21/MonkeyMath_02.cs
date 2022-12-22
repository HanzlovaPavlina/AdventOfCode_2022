using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode_2022.Day_21 {

    class MonkeyMath_02 {

        List<Monkey> monkeys;

        public MonkeyMath_02(string path) {
            monkeys = new List<Monkey>();
            LoadMonkeys(path);
            }

        public BigInteger GetResult() {

            Monkey root = monkeys.First(m => m.Name == "root");
            Monkey rootFirst = monkeys.First(m => m.Name == root.FirstMonkey);
            Monkey rootSecond = monkeys.First(m => m.Name == root.SecondMonkey);
            Monkey humn = monkeys.First(m => m.Name == "humn");
            bool first = GetResult(rootFirst);
            bool second = GetResult(rootSecond);

            if (!first) SendResult(rootFirst, rootSecond.Result);
            else SendResult(rootSecond, rootFirst.Result);

            return humn.Result;
            }

        void LoadMonkeys(string path) {
            try {
                string[] properties;

                foreach (string line in File.ReadLines(path)) {
                    properties = line.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                    if (properties[0] == "root") monkeys.Add(new Monkey(properties[0], properties[1], '=', properties[3]));
                    else if (properties[0] == "humn") monkeys.Add(new Monkey(properties[0]));
                    else if (properties.Length == 2) {
                        monkeys.Add(new Monkey(properties[0], Convert.ToInt32(properties[1])));
                        }
                    else monkeys.Add(new Monkey(properties[0], properties[1], Convert.ToChar(properties[2]), properties[3]));
                        
                    }
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        bool GetResult(Monkey monkey) {

            if (monkey.Name == "humn") return false;

            if (monkey.state != State.DONE) {

                Monkey first = monkeys.First(m => m.Name == monkey.FirstMonkey);
                Monkey second = monkeys.First(m => m.Name == monkey.SecondMonkey);

                BigInteger firstResult = 0;
                BigInteger secondResult = 0;

                if (GetResult(first) && GetResult(second)) {
                    monkey.SaveResult(Count(monkey.Operation, first.Result, second.Result));
                    monkey.state = State.DONE;
                    }
                else return false;
                }
            return true;
            }

        void SendResult(Monkey monkey, BigInteger result) {

            if (monkey.Name == "humn") {
                monkey.SaveResult(result);
                return;
                }

            Monkey first = monkeys.First(m => m.Name == monkey.FirstMonkey);
            Monkey second = monkeys.First(m => m.Name == monkey.SecondMonkey);

            if(first.state == State.FRESH && second.state == State.FRESH) {
                GetResult(first);
                GetResult(second);
                }

            if (first.state == State.FRESH) {
                BigInteger firstResult = CountFirst(monkey.Operation, result, second.Result);
                first.SaveResult(firstResult);
                first.state = State.DONE;
                SendResult(first, firstResult);
                }
            if (second.state == State.FRESH) {
                BigInteger secondResult = CountSecond(monkey.Operation, result, first.Result);
                second.SaveResult(secondResult);
                second.state = State.DONE;
                SendResult(second, secondResult);
                }
            }
        
        private BigInteger Count(char op, BigInteger first, BigInteger second) {

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

        BigInteger CountFirst(char op, BigInteger result, BigInteger second) {

            BigInteger first = 0;

            switch (op) {
                case '+':
                    first = result - second;
                    break;
                case '-':
                    first = result + second;
                    break;
                case '/':
                    first = result * second;
                    break;
                case '*':
                    first = result / second;
                    break;
                default:
                    break;
                }
            return first;
            }

        BigInteger CountSecond(char op, BigInteger result, BigInteger first) {

            BigInteger second = 0;

            switch (op) {
                case '+':
                    second = result - first;
                    break;
                case '-':
                    second = - result + first;
                    break;
                case '/':
                    second = first / result;
                    break;
                case '*':
                    second = result / first;
                    break;
                default:
                    break;
                }
            return second;
            }
        }
    }
