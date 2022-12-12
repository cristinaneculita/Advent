// See https://aka.ms/new-console-template for more information


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Advent2022
{
    class Point : IEquatable<Point>
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return x == other.x && y == other.y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }

    class Program
    {
        private static int c = 143;
        private static int l = 41;
        static int min_dist = int.MaxValue;
        static void Main(string[] args)
        {
            double rez = 0;
            
         

            var i = 0;
            string[] lines = System.IO.File.ReadAllLines("input1.txt");
            c = lines[0].Length;
            l = lines.Length;
            var puz = new char[l, c];

            var vis = new bool[l, c];
            var dist = new int[l, c];
            foreach (var line in lines)
            {
                var x = line.ToCharArray();
                for (int j = 0; j < line.Length; j++)
                {
                    puz[i, j] = x[j];
                }
                i++;
            }

            int sl = 0, sc = 0, el = 0, ec = 0;
            Stack<Point> q = new Stack<Point>();
        
            for (int j = 0; j < l; j++)
            {
                for (int k = 0; k < c; k++)
                {
                    dist[j, k] = Int32.MaxValue;
                    if (puz[j, k] == 'S')
                    {
                        sl = j;
                        sc = k;
                        puz[j, k] = 'a';
                        dist[j, k] = 0;
                       
                    }

                    if (puz[j, k] == 'E')
                    {
                        el = j;
                        ec = k;
                        puz[j, k] = 'z';
                    }

                    if (puz[j, k] == 'a')
                    {
                        dist[j, k] = 0;
                        q.Push(new Point(j, k));
                    }

                }
            }
           

            while (q.Any())
            {
                Point p = q.Pop();
                var xs = p.x - 1;
                var ys = p.y;
                if (isSqafeq(puz, p.x, p.y, xs, ys))
                    if (dist[xs, ys] > dist[p.x, p.y] + 1)
                    {
                        dist[xs, ys] = dist[p.x, p.y] + 1;
                        q.Push(new Point(xs, ys));
                    }
                xs = p.x + 1;
                ys = p.y;
                if (isSqafeq(puz, p.x, p.y, xs, ys))
                    if (dist[xs, ys] > dist[p.x, p.y] + 1)
                    {
                        dist[xs, ys] = dist[p.x, p.y] + 1;
                        q.Push(new Point(xs, ys));
                    }
                xs = p.x;
                ys = p.y - 1;
                if (isSqafeq(puz, p.x, p.y, xs, ys))
                    if (dist[xs, ys] > dist[p.x, p.y] + 1)
                    {
                        dist[xs, ys] = dist[p.x, p.y] + 1;
                        q.Push(new Point(xs, ys));
                    }
                xs = p.x;
                ys = p.y + 1;
                if (isSqafeq(puz, p.x, p.y, xs, ys))
                    if (dist[xs, ys] > dist[p.x, p.y] + 1)
                    {
                        dist[xs, ys] = dist[p.x, p.y] + 1;
                        q.Push(new Point(xs, ys));
                    }

            }

            //for (int j = 0; j < l; j++)
            //{
            //    for (int k = 0; k < c; k++)
            //    {
            //        if (dist[j,k]!=Int32.MaxValue)
            //            Console.Write(dist[j,k]);
            //        else Console.Write("*");
            //    }   
            //    Console.WriteLine();
            //}
            //FindShortestPath(puz,vis,sl,sc,el,ec, ref min_dist, 0 );



            Console.WriteLine("sum " + dist[el, ec]);
        }

        private static bool isSqafeq(char[,] puz, int xv, int yv, int x, int y)
        {
            return (x >= 0 && x < l && y >= 0 && y < c) && (puz[xv, yv] + 1 >= puz[x, y] );
        }

        //static void FindShortestPath(char[,] puz, bool[,] visited, int i, int j, int x, int y, ref int min_dist, int dist)
        //{
        //    if (i == x && j == y)
        //    {
        //        min_dist = Math.Min(min_dist, dist);
        //        Console.WriteLine("dist " + min_dist);
        //        return ;
        //    }

        //    visited[i, j] = true;
        //    // go to the top cell
        //    if (isSafe(puz, visited, i, j, i - 1, j))
        //    {
        //        FindShortestPath(puz, visited, i - 1, j, x, y, ref min_dist, dist + 1);
        //    }
        //    // go to the bottom cell
        //    if (isSafe(puz, visited, i,j,i + 1, j))
        //    {
        //        FindShortestPath(puz, visited, i + 1, j, x, y, ref min_dist, dist + 1);
        //    }

        //    // go to the right cell
        //    if (isSafe(puz, visited,i,j, i, j + 1))
        //    {
        //        FindShortestPath(puz, visited, i, j + 1, x, y, ref min_dist , dist + 1);
        //    }

        //    // go to the left cell
        //    if (isSafe(puz, visited,i,j, i, j - 1))
        //    {
        //        FindShortestPath(puz, visited, i, j - 1, x, y, ref min_dist, dist + 1);
        //    }

        //    // backtrack: remove (i, j) from the visited matrix
        //    visited[i,j] = false;

        //}

        private static bool isSafe(char[,] puz, bool[,] visited, int xv, int yv, int x, int y)
        {
            return (x >= 0 && x < l && y >= 0 && y < c) && (puz[xv, yv] + 1 == puz[x, y] || puz[xv, yv] == puz[x, y]) && !visited[x, y];
        }
    }
}