// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Advent2022
{
    class Program
    {

        static void Main(string[] args)
        {
            long rez = 0;
            string[] lines = System.IO.File.ReadAllLines("input1.txt");
            long x = 1;
            int cycle = 1;
            var ccicles = new int[] { 20, 60, 100, 140, 180, 220 };
            var sprinte = new int[] { 0, 1, 2 };
            var spritel = 0;
            var screen = new bool[6, 40];
            var pos = 0;
            foreach (var line in lines)
            {
                if (sprinte.Contains(pos))
                {
                    screen[(cycle - 1) / 40, (cycle-1)%40] = true;
                }

                pos = (pos + 1) %40;
                sprinte[1] = (int)x;
                sprinte[0] = (int)x - 1;
                sprinte[2] = (int)x + 1;
                
                if (line == "noop")
                {
                    cycle++;
                }
                else
                {
                    var y = line.Split(" ");
                    int.TryParse(y[1], out int y1);
                    cycle++;
                    
                    if (sprinte.Contains(pos))
                    {
                        screen[(cycle - 1) / 40, (cycle - 1) % 40] = true;
                    }

                    pos = (pos + 1) % 40;
                    cycle++;
                    x += y1;
                    sprinte[1] = (int)x;
                    sprinte[0] = (int)x - 1;
                    sprinte[2] = (int)x + 1;
                }
            }


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (screen[i, j])
                        Console.Write("#");
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("sum " + rez);
        }

        private static long CalcStr(int cycle, long x)
        {
            return cycle * x;
        }
    }
}