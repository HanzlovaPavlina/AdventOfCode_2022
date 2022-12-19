using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_15 {

    // https://adventofcode.com/2022/day/15

    class Sensor {

        Coordinates position;
        Coordinates beacon;
        int distance;

        public int X {
            get { return position.x; }
            }
        public int Y {
            get { return position.y; }
            }
        public Coordinates Bacon {
            get { return beacon; }
            }
        public int Distance {
            get { return distance; }
            }

        public Sensor(Coordinates posS, Coordinates posB) {
            this.position = posS;
            this.beacon = posB;
            distance = Math.Abs(posS.x - posB.x) + Math.Abs(posS.y - posB.y);
            }
        }

    class Segment {
        int from;
        int to;
        int lenght;

        public int Lenght {
            get { return lenght; }
            }

        public int From {
            get { return from; }
            }

        public int To {
            get { return to; }
            }

        public Segment(int from, int center) {
            this.from = from;
            lenght = 1 + (center - from) * 2;
            this.to = 2 * center - from;
            }
        }

    class BeaconExclusionZone {

        Sensor [] sensors;
        List<Coordinates> beacons;

        public BeaconExclusionZone(string path) {
            LoadInput(path);
            }

        // TODO: the result overflows, how to process large numbers ???
        public int GetTuningFrequency(int min, int max) {

            Coordinates vacancy = null;

            while(min != max) {
                List<Segment> occupiedSegments = GetOccupiedSegments(min).OrderBy(s => s.From).ToList();

                int i = 0;
                for (int j = 1; j < occupiedSegments.Count; j++) {
                    int overlap = CheckVacancy(occupiedSegments[i], occupiedSegments[j]);
                    if(overlap == 1) {
                        vacancy = new Coordinates(occupiedSegments[j].From - 1, min);
                        min = max-1;
                        break;
                        }
                    if (overlap != 0) i = j;
                    }
                min++;
                }
            Console.WriteLine("X: " + vacancy.x + ", Y: " + vacancy.y);
            return vacancy.x * 4000000 + vacancy.y;
            }

        public int OccupiedPositionsCountAtRow(int row) {

            List<Segment> occupiedSegments = GetOccupiedSegments(row).OrderBy(s => s.From).ToList();

             foreach (Segment s in occupiedSegments) Console.WriteLine("from: " + s.From + ", to: " + s.To);

            int occupiedPoints = occupiedSegments.First().Lenght;
            int i = 0;
            for(int j = 1; j < occupiedSegments.Count; j++) {
                int overlapPoints = CheckOverlap(occupiedSegments[i], occupiedSegments[j]);
                occupiedPoints += overlapPoints;
                if (overlapPoints != 0) i = j;
                }

            foreach (Coordinates b in beacons)
                if (b.y == row) occupiedPoints--; 
            
            return occupiedPoints;
            }

        void LoadInput(string path) {

            try {
                int countSensors = 0;
                foreach (string line in File.ReadLines(path)) countSensors++;
                sensors = new Sensor[countSensors];
                beacons = new List<Coordinates>();

                countSensors = 0;
                int minX = 0; int maxX = 0;

                foreach (string line in File.ReadLines(path)) {
                    string[] words = line.Split(new Char[] { ',', ':', '=', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    Coordinates sensorCoord = new Coordinates(Convert.ToInt32(words[3]), Convert.ToInt32(words[5]));
                    Coordinates closestBaconCoord = new Coordinates(Convert.ToInt32(words[11]), Convert.ToInt32(words[13]));
                    sensors[countSensors] = new Sensor(sensorCoord, closestBaconCoord);
                    countSensors++;

                    if (!beacons.Exists(b => b.x == closestBaconCoord.x && b.y == closestBaconCoord.y)) beacons.Add(closestBaconCoord);

                    if (Convert.ToInt32(words[3]) < minX) minX = Convert.ToInt32(words[3]);
                    if (Convert.ToInt32(words[11]) < minX) minX = Convert.ToInt32(words[11]);
                    if (Convert.ToInt32(words[5]) > maxX) maxX = Convert.ToInt32(words[5]);
                    if (Convert.ToInt32(words[13]) > maxX) maxX = Convert.ToInt32(words[13]);
                    }
            }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        private List<Segment> GetOccupiedSegments(int row) {

            List<Segment> occupiedSegments = new List<Segment>();

            foreach (Sensor s in sensors) {
                //Console.WriteLine("sensor: " + s.X + "," + s.Y + " > beacon: " + s.Bacon.x + "," + s.Bacon.y);
                if (s.Y - s.Distance > row || s.Y + s.Distance < row) continue; // no penetration

                Segment newSegment;
                newSegment = new Segment(s.X - (s.Distance - Math.Abs(s.Y - row)), s.X);
                //Console.WriteLine("from: " + newSegment.From + ", to: " + newSegment.To + ", lenght: " + newSegment.Lenght);
                occupiedSegments.Add(newSegment);
                }
            return occupiedSegments;
            }
        
        private int CheckOverlap(Segment first, Segment second) {
            if (first.To >= second.To) return 0; // second segment is part of first segment
            else if (second.From > first.To) return second.Lenght; // segments not overlaping
            
            return second.To - first.To; // second segment start in first segment and continue behind first segment
            }

        /// check for free place between segments
        /// -1 == second segment start in first segment and continue behind first segment
        /// 0 == second segment is fullz part of first segment
        /// 1 == segments not overlaping, there is free place
        private int CheckVacancy(Segment first, Segment second) {
            if (first.To >= second.To) return 0; // second segment is part of first segment
            else if (second.From > first.To) return 1; // segments not overlaping

            return -1;
            }
        }
    }
