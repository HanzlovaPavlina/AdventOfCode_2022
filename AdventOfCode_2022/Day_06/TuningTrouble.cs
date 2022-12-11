using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_06 {
    class TuningTrouble {
        
        private List<char> markerCharacters = new List<char>();

        public int GetStartOfPacketMarker(string path) {
            StreamReader sr = LoadInput(path);
            if (sr != null) return GetStartOfPacketMarker(sr);
            return -1;
            }

        private StreamReader LoadInput(string path) {
            try {
                FileStream fs = File.Open(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                return sr;
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            return null;
            }

        private int GetStartOfPacketMarker(StreamReader sr) {

            List<char> helper;
            int markerPosition = 0;
            bool positionFounded = false;
            char next;
            
            while ((next = (char)sr.Peek()) >= 0) {
                positionFounded = true;

                if (markerCharacters.Count() < 14) {
                    markerCharacters.Add((char)sr.Read());
                    markerPosition++;
                    continue;
                    }

                helper = markerCharacters.Distinct().ToList();
                if (markerCharacters.Count != helper.Count) {
                    positionFounded = false;
                    markerCharacters.RemoveAt(0);
                    markerCharacters.Add((char)sr.Read());
                    //TODO check end of streamLine
                    markerPosition++;
                    }
                
                if (positionFounded) {
                    foreach (char x in markerCharacters) Console.Write(x);
                    Console.WriteLine();
                    break;
                }
            }
            return markerPosition;
        }
    }
}
