using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_13 {
    class DistressSignal {

        public DistressSignal(string path) {

            LoadAndProcessInput(path);
            }

        void LoadAndProcessInput(string path) {

            try {
                foreach (string line in File.ReadLines(path)) {
                    foreach (char next in line.ToCharArray()) {
                        if (next == '[') {

                        }
                    }
                }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }
        }
    }
