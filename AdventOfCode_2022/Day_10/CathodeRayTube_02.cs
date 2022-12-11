using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_10 {

    // CathodeRayTube

    class Signal_2 {

        public Signal_2(string path) {
            LoadAndDrawSignal(path);
            }

        private void LoadAndDrawSignal(string path) {

            try {
                string[] entries = new string[2];
                int cycleTime = 0; int steps;
                int registerX = 1;

                foreach (string line in File.ReadLines(path)) {
                    entries = line.Split(' ');

                    steps = entries[0] == "noop" ? 1 : 2;
                    while(steps > 0) {
                        CRTdraw(cycleTime++, registerX);
                        if (cycleTime % 40 == 0) {
                            cycleTime = 0;
                            Console.WriteLine();
                            }
                        steps--;
                        }
                    if (entries[0] == "addx") registerX += Convert.ToInt32(entries[1]);
                    }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
            }
        }

        public void CRTdraw(int cycleTime, int register) {
            if (cycleTime >= register - 1 && cycleTime <= register + 1) Console.Write('#');
            else Console.Write('.');
            if (cycleTime > 40) Console.Write($"reg: {register}, cycle: {cycleTime}");
            }
        }
}
