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
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Advent2022
{


    
    public class Program
    {
        static void Main(string[] args)
        {
            double rez = 0;
            var map = new int[6000, 500];

            string[] lines = File.ReadAllLines("input1.txt");
            foreach (var line in lines)
            {
                var x =line.Split(" -> ");

                for (int i = 0; i < x.Length-1; i++)
                {
                    var y1 = x[i].Split(",");
                    var y2 = x[i + 1].Split(",");
                    int.TryParse(y1[0], out int y11);
                    int.TryParse(y1[1], out int y12);
                    int.TryParse(y2[0], out int y21);
                    int.TryParse(y2[1], out int y22);
                    var xmin = Math.Min(y11, y21);
                    var xmax = Math.Max(y11, y21);
                    var ymin = Math.Min(y12, y22);
                    var ymax = Math.Max(y12, y22);

                    for (int j = xmin; j <=xmax; j++)
                    {
                        for (int k = ymin; k <=ymax; k++)
                        {
                            map[j, k] = 1;
                        }
                    }
                }
            }

            var maxjos = 0;
            for (int i = 0; i < 6000; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    if (map[i, j] == 1)
                    {
                        if(j>maxjos)
                            maxjos = j;
                    }
                }
            }


            for (int i = 0; i < 6000; i++)
            {
                map[i, maxjos+2] = 1;
            }

            var xs = 500;
            var ys = 0;
            var sandInMap = false;
            var numbersand = 0;
            do
            {
                numbersand++;
                sandInMap = PourSand(xs, ys, map, maxjos);
                
            } while (sandInMap);

            numbersand--;

            Console.WriteLine("sum " + numbersand);
        }

        private static bool PourSand(int xsi, int ysi, int[,] map, int maxjos)
        {
            var xs = xsi;
            var ys = ysi;
            while (true)
            {
               //down
              
               if (map[xs, ys+1] != 1 && map[xs, ys+1] != 2)
               {
                   ys++;
                   //if(ys<=maxjos)
                        continue;
               }

               if (map[xs - 1, ys + 1] != 1 && map[xs - 1, ys + 1] != 2)
               {
                   ys++;
                   xs--;
                   //if (ys <= maxjos)
                       continue;
               }
               if (map[xs + 1, ys + 1] != 1 && map[xs + 1, ys + 1] != 2)
               {
                   ys++;
                   xs++;
                   //if (ys <= maxjos)
                        continue;
               }
               
                map[xs,ys] = 2;
                if (map[500, 0] == 2)
                    return false;
                return true;

            }
        }
    }

}