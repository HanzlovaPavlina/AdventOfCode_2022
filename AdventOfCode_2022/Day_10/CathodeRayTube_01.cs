using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_10 {

    // CathodeRayTube

    class Signal_1 {

        private int strength;

        public Signal_1(string path) {
            LoadSignal(path);
            }

        public int getStrength() {
            return strength;
            }

        private void LoadSignal(string path) {

            try {
                string[] entries = new string[2];
                int cycleTime = 0; int steps;
                int registerX = 1;

                foreach (string line in File.ReadLines(path)) {
                    entries = line.Split(' ');

                    steps = entries[0] == "noop" ? 1 : 2;
                    while(steps > 0) {
                        cycleTime++;
                        checkStrength(cycleTime, registerX);
                        steps--;
                        }
                    if (entries[0] == "addx")
                        registerX += Convert.ToInt32(entries[1]);
                    }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
            }
        }

        private void checkStrength(int cycle, int register) {
            if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220) {
                strength += cycle * register;

                // Console.WriteLine($"cycle: {cycle}, register: {register}, strenght: {strength}");
                }
        }
    }
}
