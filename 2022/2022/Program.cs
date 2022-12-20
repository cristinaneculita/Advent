using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.IO.Enumeration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Xml.XPath;

namespace Advent2022
{



    public class Program
    {

        public class Position
        {
            public long Pos { get; set; }

            public Position(long pos)
            {
                Pos = pos;
            }
        }

        static void Main(string[] args)
        {
            var rez = 0;
            string[] lines = File.ReadAllLines("input1.txt");
            var inp = new List<Position>();
            int i = 0;
            foreach (var line in lines)
            {
                int.TryParse(line, out int x);
                var y = new Position(x);
                inp.Add(y);
            }

            var old = new Position[lines.Length];
            for (int j = 0; j < lines.Length; j++)
            {
                old[j] = inp[j];
            }

            for (int j = 0; j < lines.Length; j++)
            {
                inp[j].Pos = 811589153 * inp[j].Pos;
            }

            for (int k = 0; k < 10; k++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    // Console.WriteLine(j);
                    var x = inp.IndexOf(old[j]);
                    inp = MoveN(j, old[j].Pos, inp, lines.Length, x);
                }
            }
            //var prev = new int[lines.Length];


            int jj;
            for (jj = 0; jj < lines.Length; jj++)
            {
                if (inp[jj].Pos == 0)
                    break;
            }

            long x1 = Find(inp, jj, lines.Length);



            Console.WriteLine("sum " + x1);
        }

        private static long Find(List<Position> inp, int i, int L)
        {
            var p = i;
            long sum = 0;
            for (int j = 0; j < 3000; j++)
            {
                if (j == 1000)
                    sum += inp[p].Pos;
                if (j == 2000)
                    sum += inp[p].Pos;
                p = Rightn(p, L);

            }

            sum += inp[p].Pos;
            return sum;
        }

        private static int Rightn(int p, int L)
        {
            if (p < L - 1)
                return p + 1;
            return 0;
        }

        private static List<Position> MoveN(int pos1, long nr, List<Position> inp, int L, int pos)
        {
            Position aux;
            nr = nr % (L - 1);
            if (nr > 0)
            {
                for (long j = 0; j < nr; j++)
                {
                    var r = Rightn(pos, L);
                    aux = inp[r];
                    inp[r] = inp[pos];
                    inp[pos] = aux;
                    pos = r;
                }
            }
            else if (nr < 0)
            {
                for (long j = 0; j < -nr; j++)
                {
                    var l = Leftn(pos, L);
                    aux = inp[l];
                    inp[l] = inp[pos];
                    inp[pos] = aux;
                    pos = l;
                }
            }

            return inp;

        }

        private static int Leftn(int pos, int L)
        {
            if (pos >= 1)
                return pos - 1;
            return L - 1;

        }


    }
}