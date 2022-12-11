using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

struct Range {
    public int from;
    public int to;

    public Range(int from, int to) {
        this.from = from; this.to = to;
        }

    public int Length() {
        return to - from;
        }
    }

namespace AdventOfCode_2022.Day_04 {
    class CampCleanup {

        public int GetCleanUpFullyContains(string input) {
            LoadAndProcessInput(input);
            return fullyContains;
            }

        private int fullyContains = 0;

        private void LoadAndProcessInput(string path) {

            int[] matchesValues = new int[4];
            int i = 0;

            string pattern = @"\d*";

            foreach (string line in File.ReadLines(path)) {
                try {
                    Match match = Regex.Match(line, pattern, RegexOptions.None, TimeSpan.FromSeconds(1));
                    i = 0;

                    // Handle match here...
                    while (match.Success) {
                        if (match.Value != "") {
                            matchesValues[i] = Convert.ToInt32(match.Value);
                            i++;
                            }
                        match = match.NextMatch();
                        }
                    fullyContains += CheckPartOvelap(new Range(matchesValues[0], matchesValues[1]), new Range(matchesValues[2], matchesValues[3]));
                    }
                catch (RegexMatchTimeoutException) {
                    // Do nothing: assume that exception represents no match.
                    }
                }
            }

        // --- Part One ---
        private int CheckFullyOverlap(Range first, Range second) {
            if (first.from >= second.from && first.to <= second.to) return 1; 
            if(first.from <= second.from && first.to >= second.to) return 1;
            return 0;
            }

        // --- Part Two ---
        private int CheckPartOvelap(Range first, Range second) {
            if (CheckFullyOverlap(first, second) == 1) return 1;
            if (first.from <= second.from && first.to >= second.from) return 1;
            if (first.to >= second.to && first.from <= second.to) return 1;
            return 0;
            }
    }
}
