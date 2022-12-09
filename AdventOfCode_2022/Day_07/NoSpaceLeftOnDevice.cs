using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2022 {
    // *** Day 7: No Space Left On Device ***

    public enum DFSState {
        Fresh = 1,
        Open = 2,
        Closed = 3
    }

    class MyFile {
        string name;
        public int size;
        string suffix;

        public MyFile(string name, int size, string suffix) {
            this.name = name;
            this.size = size;
            this.suffix = suffix;
        }
    }

    class MyDirectory {

        internal string name;
        internal int size;
        internal List<MyDirectory> directories;
        internal List<MyFile> files;
        internal MyDirectory parent;
        internal DFSState state;

        public MyDirectory(string name, MyDirectory parent) {
            this.name = name;
            this.parent = parent;
            this.directories = new List<MyDirectory>();
            this.files = new List<MyFile>();
            state = DFSState.Fresh;
            size = 0;
        }

        internal void Resize(int addSize) {
            this.size += addSize;
            if (this.parent != null) parent.Resize(addSize);
            }

        internal int DFS_Part_1() {

            if (state != DFSState.Fresh) return 0; 
            state = DFSState.Open;
            int totalSize = 0;

            foreach (MyDirectory dir in directories) {
                totalSize += dir.DFS_Part_1();
                }
            state = DFSState.Closed;
            Console.WriteLine($"name: {name}, total: {totalSize}, size: {size}");
            foreach (MyDirectory dir in directories) Console.WriteLine($"subDirectory: {dir.name}, sizes: {dir.size}"); 

            if (size <= 100000) return totalSize + size;
            return totalSize;
        }

        internal int DFS_Part_2(int minSize, int neededSize) {

            if (state != DFSState.Fresh) return 0;
            state = DFSState.Open;

            foreach (MyDirectory dir in directories) {
                minSize = dir.DFS_Part_2(minSize, neededSize);
                }
            state = DFSState.Closed;
            Console.WriteLine($"name: {name}, lastSize: {minSize}, size: {size}");
            foreach (MyDirectory dir in directories) Console.WriteLine($"subDirectory: {dir.name}, sizes: {dir.size}");
            Console.WriteLine();

            if (size >= neededSize && size < minSize) return size;
            return minSize;
            }

        }

    class MyDevice : MyDirectory {

        public MyDevice(string path) : base("/", null) { LoadInput(path); }
        public int SolvePart_1() { return this.DFS_Part_1(); }

        public int SolvePart_2() {
            int neededPlace = 30000000 - (70000000 - this.size);
            Console.WriteLine("Needed Place >>> " + neededPlace);
            return this.DFS_Part_2(size, neededPlace);
            }

        private void LoadInput(string path) {

            MyDirectory actualDirectory = this;
            try {

                foreach (string line in File.ReadLines(path)) {

                    string[] lineParts = line.Split(' ');

                    switch (lineParts[0]) {
                        case "$":
                            if (lineParts[1] == "cd" && lineParts[2] == "/") actualDirectory = this;
                            else if (lineParts[1] == "cd" && lineParts[2] == "..") actualDirectory = actualDirectory.parent;
                            else if (lineParts[1] == "cd") actualDirectory = actualDirectory.directories.Where(d => d.name == lineParts[2]).First();
                            break;
                        case "dir":
                            actualDirectory.directories.Add(new MyDirectory(lineParts[1], actualDirectory));
                            break;
                        default:
                            string[] nameParts = lineParts[1].Split('.');
                            MyFile newFile = new MyFile(nameParts[0], Convert.ToInt32(lineParts[0]), nameParts.Length > 1 ? nameParts[1] : null);
                            actualDirectory.files.Add(newFile);
                            actualDirectory.Resize(newFile.size);
                            break;
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
