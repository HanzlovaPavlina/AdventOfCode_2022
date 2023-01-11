using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_20 {

    class Number {

        long value;
        Number before;
        Number after;

        public Number(long value) {
            this.value = value;
            }

        public Number(long value, Number before, Number after) {
            this.value = value;
            this.before = before;
            this.after = after;
            }

        public void Conect(Number before, Number after) {
            this.before = before;
            this.after = after;
            }

        public Number Before { get { return before;  } }
        public Number After { get { return after; } }

        public void ChangeBefore(Number before) {
            this.before = before;
            }
        public void ChangeAfter(Number after) {
            this.after = after;
            }

        public long Get() {
            return value;
            }
        public long GetBefore() {
            return before.Get();
            }
        public long GetAfter() {
            return after.Get();
            }
        }

    class GrovePositioningSystem {

        List<Number> numbers;
        Number zero;
        int puzzle_part;

        public GrovePositioningSystem(string path, int part) {
            numbers = new List<Number>();
            puzzle_part = part;
            LoadInput(path, puzzle_part);
            //PrintNumbers();
            //PrintNumbersWithRelatives();
            }

        public long GetGroveCoordinates() {

            long total = 0;
            int mixingCount = puzzle_part == 2 ? 10 : 1;
            for (int i = 0; i < mixingCount; i++) MixingNumbers();

            Number actual = zero;
            int count = 2999;

            while(count >= 0) {
                actual = actual.After;
                if (count == 1000 || count == 2000 || count == 0) {
                    total += actual.Get();
                    Console.WriteLine(actual.Get());
                    }
                count--;
                }
            //PrintNumbers();
            return total;
            }

        void LoadInput(string path, int part) {
            try {
                using (StreamReader sr = new StreamReader(path)) {

                    Number num1 = new Number(part == 1 ? Convert.ToInt64(sr.ReadLine()) : Convert.ToInt64(sr.ReadLine()) * 811589153);
                    numbers.Add(num1);
                    Number num2 = new Number(part == 1 ? Convert.ToInt64(sr.ReadLine()) : Convert.ToInt64(sr.ReadLine()) * 811589153);
                    numbers.Add(num2);
                    Number num3 = new Number(part == 1 ? Convert.ToInt64(sr.ReadLine()) : Convert.ToInt64(sr.ReadLine()) * 811589153);
                    numbers.Add(num3);
                    num2.Conect(num1, num3);

                    while (sr.Peek() >= 0) {
                        num1 = num2;
                        num2 = num3;
                        num3 = new Number(part == 1 ? Convert.ToInt64(sr.ReadLine()) : Convert.ToInt64(sr.ReadLine()) * 811589153);
                        numbers.Add(num3);
                        num2.Conect(num1, num3);
                        if (num3.Get() == 0) zero = num3;
                        }
                    num3.Conect(num2, numbers.ElementAt(0));
                    numbers.First().Conect(num3, numbers.ElementAt(1));
                    }
                //PrintNumbers();
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        long MixingNumbers() {

            long shift;
            Number actual;
            Number before;
            Number after;

            int repeating = numbers.Count - 1;
            int pos = 0;

            while (pos < numbers.Count) {
                actual = numbers.ElementAt(pos);
                shift = actual.Get() % repeating;

                if (shift != 0) {
                    before = actual.Before;
                    before.ChangeAfter(actual.After);

                    after = actual.After;
                    after.ChangeBefore(actual.Before);
                    
                    while (shift != 0) {
                        before = shift > 0 ? before.After : before.Before;
                        shift += shift > 0 ? -1 : 1;
                        }
                
                    actual.ChangeBefore(before);
                    actual.ChangeAfter(before.After);
                    before.After.ChangeBefore(actual);
                    before.ChangeAfter(actual);
                }

                pos++;
                }
            return zero.GetAfter();
            }


        void PrintNumbers() {

            Number actual = numbers.First();
            int count = numbers.Count;

            while(count > 0) {
                Console.Write(actual.Get() + ", ");
                actual = actual.After;
                count--;
                }
            Console.WriteLine();
            }

        void PrintNumbersWithRelatives() {

            foreach (Number num in numbers) {
                Console.Write($"({num.GetBefore()}){num.Get()}({num.GetAfter()}), ");
                }
            Console.WriteLine();
            }
        }
    }
