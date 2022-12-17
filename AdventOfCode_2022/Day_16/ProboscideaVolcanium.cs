using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_16 {

    enum Status {
        Fresh,
        Open,
        Closed
        }

    class Valve {

        string name;
        int flowRate;
        public bool closed;
        public Status state;
        public List<Valve> connectedValves;

        public string Name {
            get { return name; }
            }
        public int FlowRate {
            get { return flowRate; }
            }

        public Valve(string name, int rate) {
            this.name = name;
            this.flowRate = rate;
            connectedValves = new List<Valve>();
            state = Status.Fresh;
            closed = true;
            }

        public void ConnectValve(Valve valve) {
            connectedValves.Add(valve);
            }

        public void SortConnectedValves() {
            connectedValves = connectedValves.OrderBy(v => v.flowRate).Reverse().ToList();
            }

        public bool isPeek() {
            if (connectedValves.Count == 1) return true;
            return false;
            }
        }

    class ProboscideaVolcanium {

        List<Valve> volcano;
        int[,] realesedPressures;
        string[,] befores;

        public ProboscideaVolcanium(string path) {

            volcano = new List<Valve>();
            LoadInput(path);
            }

        public int GetMaxRealesedPressure(int timeToErupt) {

            Valve start = volcano.First(v => v.Name == "AA");
            realesedPressures = new int[volcano.Count, 30];
            befores = new string[volcano.Count, 30];
            List<string> closedValves = new List<string>();
            foreach (Valve v in volcano) if (v.FlowRate > 0) closedValves.Add(v.Name);

            Console.WriteLine("Start.");
            ReleasePressure(start, "", timeToErupt, closedValves, 0);

            int maxPressure = 0;
            //int count = 1;
            foreach (int pressure in realesedPressures) {
                if (pressure > maxPressure) maxPressure = pressure;
                //Console.Write(pressure + "  ");
                //count++;
                //if (count % 30 == 0) Console.WriteLine();
                }
                
            int count = 1;
            foreach (string b in befores) {
                Console.Write(b + "_");
                count++;
                if (count % 30 == 0) Console.WriteLine();
                }
            return maxPressure;
            }


        void LoadInput(string path) {

            try {
                List<string[]> splitLines = new List<string[]>();

                // add valves to volcano
                foreach (string line in File.ReadLines(path)) {
                    string[] words = line.Split(new Char[] { ',', ';', '=', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    splitLines.Add(words);
                    Valve newValve = new Valve(words[1], Convert.ToInt32(words[5]));
                    volcano.Add(newValve);
                    }

                // add cpnnected valves to each valve in volcano
                foreach (string[] line in splitLines) {
                    Valve actualValve = volcano.First(v => v.Name == line[1]);
                    for (int i = 10; i < line.Length; i++) actualValve.ConnectValve(volcano.First(v => v.Name == line[i]));
                    }

                //sort connected valves in each valve descending
                foreach (Valve valve in volcano) {
                    valve.SortConnectedValves();
                    }

                Console.WriteLine("Input loaded.");
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        // TODO >>> diffrent algorytm, this is wrong and slow 
        private void ReleasePressure(Valve actual, string parent, int timeToErupt, List<string> closed, int pressure) {
            
            timeToErupt--;
            if (timeToErupt == 0 || closed.Count == 0) return;

            List<string> newClosed = new List<string>();
            foreach (string s in closed) newClosed.Add(s);

            foreach (Valve child in actual.connectedValves) {
                if (child.Name == parent && !child.isPeek()) continue;
                ReleasePressure(child, actual.Name, timeToErupt, closed, pressure);
                }

            timeToErupt--;
            foreach (Valve child in actual.connectedValves) {
                if (child.Name == parent && !child.isPeek()) continue;

                if (child.FlowRate > 0 && newClosed.Remove(child.Name)) {
                    pressure = ((timeToErupt+1) * child.FlowRate) + pressure;
                    if (realesedPressures[volcano.FindIndex(v => v.Name == child.Name), timeToErupt+1] < pressure) {
                        realesedPressures[volcano.FindIndex(v => v.Name == child.Name), timeToErupt + 1] = pressure;
                        befores[volcano.FindIndex(v => v.Name == child.Name), timeToErupt + 1] = parent;
                        }

                    if (timeToErupt == 0 || newClosed.Count == 0) continue;
                    ReleasePressure(child, actual.Name, timeToErupt, newClosed, pressure);
                }
            }
        }
    }
}
