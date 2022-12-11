using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022.Day_09 {

        public enum Status {
            Fresh = 0,
            Used = 1,
            }

        public class Point {
            Status state;
            public Status State { get { return state; } }

            public Point() {
                state = Status.Fresh;
                }

            public void Use() {
                state = Status.Used;
                }
            }

        public class Coordinates {
            public int x;
            public int y;

        public Coordinates(int x, int y) {
            this.x  = x;
            this.y = y;
            }

        public bool checkDistance(Coordinates A, Coordinates B) {
            if (Math.Abs(A.x  - B.x ) <= 1 && Math.Abs(A.y = B.y) <= 1) return true;
            return false;
            }
        }

        public class Grid {

            int height;
            int width;
            List<List<Point>> grid;


            public Grid(int height, int width) {
                this.height = height;
                this.width = width;
            grid = new List<List<Point>>();

                for (int i = 0; i < height; i++) {
                    grid.Add(new List<Point>());
                    for (int j = 0; j < width; j++)
                        grid.ElementAt(i).Add(new Point());
                    }
                }

            public Point getPoint(int x, int y) {
                return grid.ElementAt(y).ElementAt(x);
                }

            public List<List<Point>> getRows() {
                return grid;
                }

            public void AddColumnRight() {
                for(int j = 0; j < height; j++) {
                        grid.ElementAt(j).Add(new Point());
                    }
                width++;
                }

            public void AddColumnLeft() {
                for (int j = 0; j < height; j++) {
                    grid.ElementAt(j).Insert(0, new Point());
                    }
                width++;
                }

            public void AddRowDown() {
                grid.Add(new List<Point>());

                for (int i = 0; i < width; i++) {
                    grid.Last().Add(new Point());
                    }
                height++;
                }

            public void AddRowUp() {
                grid.Insert(0, new List<Point>());

                for (int i = 0; i < width; i++) {
                    grid.First().Add(new Point());
                    }
                height++;
            }

            public bool isSpaceRight(Coordinates c) {
                return c.x +1 == width ? false : true;
                }

            public bool isSpaceLeft(Coordinates c) {
                return c.x -1 < 0 ? false : true;
                }

            public bool isSpaceUp(Coordinates c) {
                return c.y-1 < 0 ? false : true;
                }

            public bool isSpaceDown(Coordinates c) {
                return c.y+1 == height ? false : true;
                }

            }

    }


