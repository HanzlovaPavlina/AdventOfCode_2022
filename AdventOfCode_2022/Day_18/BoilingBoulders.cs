using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_18 {

    class Point3D {

        int x;
        int y;
        int z;

        public Point3D(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
            }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Z { get { return z; } }
        }

    class BigCube {

        bool[,,] loadedCubes;

        int maxX;
        int maxY;
        int maxZ;
        int max;

        public BigCube(string path) {
            LoadCubes(path);
            }

        public int GetSurfaceArea() {
            return GetExposedArea(loadedCubes);
            }

        public int GetSurfaceAreaWithoutBubble() {
            return 0;
            }

        private int GetExposedArea(bool[,,] cubes) {

            int total = 0;
            total += GetExposedSidesCount_X(cubes);
            total += GetExposedSidesCount_Y(cubes);
            total += GetExposedSidesCount_Z(cubes);
            //Console.WriteLine("X> " + GetExposedSidesCount_X() + ", Y> " + GetExposedSidesCount_Y() + ", Z>" + GetExposedSidesCount_Z());

            return total;
            }

        void LoadCubes(string path) {
            try {

                List<Point3D> coordinates = new List<Point3D>();
                string[] point = new string[3];

                foreach (string line in File.ReadLines(path)) {
                    point = line.Split(',');
                    coordinates.Add(new Point3D(Convert.ToInt32(point[0]), Convert.ToInt32(point[1]), Convert.ToInt32(point[2])));
                    if (maxX < coordinates.Last().X) maxX = coordinates.Last().X;
                    if (maxY < coordinates.Last().Y) maxY = coordinates.Last().Y;
                    if (maxZ < coordinates.Last().Z) maxZ = coordinates.Last().Z;
                    }

                max = Math.Max(maxX, Math.Max(maxY, maxZ)) + 2;
                loadedCubes = new bool[max, max, max];
                foreach (Point3D p in coordinates) {
                    loadedCubes[p.X, p.Y, p.Z] = true;
                    }
                }
            catch (Exception e) {
                // Let the user know what went wrong.
                Console.WriteLine("{0} File can't be load:", e);
                }
            }

        private int GetExposedSidesCount_Z(bool[,,] cubes) {

            int total = 0;
            bool cubeBefore = false;
            bool cubeActual;

            for (int x = 0; x < max; x++) {
                for (int y = 0; y < max; y++) {
                    cubeBefore = false;
                    for (int z = 0; z < max; z++) {
                        cubeActual = cubes[x, y, z];
                        if (!cubeActual && cubeBefore) total++;
                        if (cubeActual && !cubeBefore) total++;

                        if (cubeActual) cubeBefore = true;
                        else cubeBefore = false;
                        }
                    }
                }
            return total;
            }

        private int GetExposedSidesCount_Y(bool[,,] cubes) {

            int total = 0;
            bool cubeBefore = false;
            bool cubeActual;

            for (int z = 0; z < max; z++) {
                for (int x = 0; x < max; x++) {
                    cubeBefore = false;
                    for (int y = 0; y < max; y++) {
                        cubeActual = cubes[x, y, z];
                        if (!cubeActual && cubeBefore) total++;
                        if (cubeActual && !cubeBefore) total++;

                        if (cubeActual) cubeBefore = true;
                        else cubeBefore = false;
                        }
                    }
                }
            return total;
            }

        private int GetExposedSidesCount_X(bool[,,] cubes) {

            int total = 0;
            bool cubeBefore = false;
            bool cubeActual;

            for (int y = 0; y < max; y++) {
                for (int z = 0; z < max; z++) {
                    cubeBefore = false;
                    for (int x = 0; x < max; x++) {
                        cubeActual = cubes[x, y, z];
                        if (!cubeActual && cubeBefore) total++;
                        if (cubeActual && !cubeBefore) total++;

                        if (cubeActual) cubeBefore = true;
                        else cubeBefore = false;
                        }
                    }
                }
            return total;
            }

        private void DrawCube(bool[,,] cubes) {
            for (int y = 0; y < max; y++) {
                for (int z = 0; z < max; z++) {
                    for (int x = 0; x < max; x++) {
                        if (cubes[x, y, z]) Console.Write('#');
                        else Console.Write('.');
                        }
                    Console.WriteLine();
                    }
                Console.WriteLine();
                Console.WriteLine();
                }
            }
        }
    }

