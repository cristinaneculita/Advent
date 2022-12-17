using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;

namespace Advent2022
{


   public class Result {
   public long Lowest { get; set; }
   public bool  CanFall { get; set; }

   public Result(long lowest, bool canFall)
   {
       Lowest = lowest;
       CanFall = canFall;
   }
   }

   public class State:IEquatable<State>
   {
       public int[] View { get; set; }
       public int  Wind { get; set; }
       public int Rock { get; set; }
       public long Maxy { get; set; }
       public int  Numb{ get; set; }
       public State(int[] view, int wind, int rock)
       {
           View = view;
           Wind = wind;
           Rock = rock;
       }
       public bool Equals(State? other)
       {
           if (ReferenceEquals(null, other)) return false;
           if (ReferenceEquals(this, other)) return true;
           var viewseq = false;
           for (int i = 0; i < 7; i++)
           {
                if (other.View[i] != View[i])
                    return false;
           }
           return Wind == other.Wind && Rock == other.Rock;
       }

       public override bool Equals(object? obj)
       {
           if (ReferenceEquals(null, obj)) return false;
           if (ReferenceEquals(this, obj)) return true;
           if (obj.GetType() != this.GetType()) return false;
           return Equals((State)obj);
       }

       public override int GetHashCode()
       {
           return HashCode.Combine(View, Wind, Rock);
       }
       
   }



   public class Program
    {
        private static long height = 0;
        private static long L = 1000000;
        private static int l=7;
        static void Main(string[] args)
        {
            var rez = 0;
            string[] lines = File.ReadAllLines("input1.txt");
            int air = 0;
            var tower = new int[L, l];
            //nothing = 0
            //current rock = 2
            //stable rock = 1
            var states = new List<State>();
            var loop = 0;long heightloop = 0;
            long rest = 0;
            long howmany = 0;
            for (long i = 0; i < 16000; i++)
            {
                
                var x = GetRock(i);
                height = GetCurrentHeight(tower);
                var xxx = GetView(tower, height);
                var statenew = new State(xxx, air, x);
                statenew.Maxy = height;
                statenew.Numb = (int)i;
                if (states.Contains(statenew))
                {
                    var xy = states.FirstOrDefault(s => s.Equals(statenew));
                    Console.WriteLine("gasit "+ statenew.Numb + " "+statenew.Maxy);
                    Console.WriteLine("gasit 2 "+ xy.Numb + " "+xy.Maxy);
                    loop = statenew.Numb - xy.Numb;
                    heightloop = statenew.Maxy - xy.Maxy;
                     rest = 1000000000000 % loop;
                     howmany = 1000000000000 / loop;
                     if (i == rest + loop)
                         break;
                }
                else
                {
                    states.Add(statenew);
                }

                PositionRock(x, tower);
                Draw(tower, height + 6);
                var f = new Result(height + 3, true);
                while (true)
                {
                    PushAir(tower, lines[0][air], f.Lowest);
                   // Draw(tower, height + 6);
                    if (air < lines[0].Length - 1)
                        air++;
                    else air = 0;
                    f = Fall(tower, f.Lowest);
                   // Draw(tower, height + 6);
                    if (!f.CanFall)
                        break;

                }

                Rest(tower, f.Lowest);
               

            }

            height = GetCurrentHeight(tower);
            long rez2 = howmany * heightloop + (height - heightloop);
            Console.WriteLine("sum " + height);
            Console.WriteLine("rez " +rez2);

        }

        private static int[] GetView(int[,] tower,long height)
        {
            var res = new int[7];
            for (int c = 0; c < 7; c++)
            {
                int d = 0;
                for (int i = (int)height; i >= 0; i--)
                {
                    if (tower[i, c] == 0)
                        d++;
                    else
                        break;
                }

                res[c] = d-1;
            }

            return res;

        }

        private static void Draw(int[,] tower, long h)
        {
          //  Drawm(tower,h);
           
        }

        private static void Drawm(int[,] tower, long h)
        {
            Console.WriteLine();
            for (long j = h; j >= 0 && j >= h - 30; j--)
            {
                for (int k = 0; k < l; k++)
                {
                    if (tower[j, k] == 0)
                        Console.Write(".");
                    if (tower[j, k] == 1)
                        Console.Write("#");
                    if (tower[j, k] == 2)
                        Console.Write("@");
                }
                Console.WriteLine();
            }
        }

        private static void Rest(int[,] tower, long h)
        {
            for (long i = h; i < h+4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tower[i, j] == 2)
                        tower[i, j] = 1;
                }
            }
        }

        private static Result Fall(int[,] tower, long lowest)
        {

            if (HasPlaceToFall(tower, lowest))
            {
                FallRock(tower, lowest);
                return new Result(lowest-1,true);
            }

            return new Result(lowest,false);
        }

        private static void FallRock(int[,] tower, long h)
        {
            for (long i = h; i < h + 4; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    if (tower[i, j] == 2)
                    {
                        tower[i-1,j] = 2;
                        tower[i, j] = 0;
                    }
                }
            }
        }

        private static bool HasPlaceToFall(int[,] tower, long h)
        {
            for (long i = h; i < h+4; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    if (i == 0)
                        return false;
                    if (tower[i,j]==2 && tower[i - 1, j] == 1)
                        return false;
                }
            }

            return true;
        }

        private static void PushAir(int[,] tower, char c, long h)
        {
            var unableToMove = false;
            switch (c)
            {
                case '<':
                    for (long i = h; i < h+4; i++)
                    {
                        if (tower[i, 0] == 2)
                        {
                            unableToMove = true;
                            break;
                        }
                        for (int j = 1; j < l; j++)
                        {
                            if(tower[i,j]==2 && tower[i, j - 1]==1)
                            {
                                unableToMove = true;
                                break;
                            }
                        }
                    }

                    if (!unableToMove)
                    {
                        for (long i = h; i < h + 4; i++)
                        {
                            var vect2 = new int[7];
                            for (int j = 0; j < 7; j++)
                            {
                                if (tower[i, j] == 2)
                                {
                                    vect2[j] = 2;
                                    tower[i, j] = 0;
                                }

                            }
                            for (int j = 1; j < l; j++)
                            {
                                if (vect2[j] == 2)
                                    tower[i, j - 1] = vect2[j];
                            }

                            //tower[i, l - 1] = 0;
                        }
                    }
                    break;
                case '>':
                    for (long i = h; i < h + 4; i++)
                    {
                        if (tower[i, l-1] == 2)
                        {
                            unableToMove = true;
                            break;
                        }

                        for (int j = l - 2; j >= 0; j--)
                        {
                            if (tower[i,j]==2 && tower[i, j + 1] ==1)
                            {
                                unableToMove = true;
                                break;
                            }
                        }
                    }

                    if (!unableToMove)
                    {
                        for (long i = h; i < h + 4; i++)
                        {
                            var vect2 = new int[7];
                            for (int j = 0; j < 7; j++)
                            {
                                if (tower[i, j] == 2)
                                {
                                    vect2[j] = 2;
                                    tower[i, j] = 0;
                                }
                               
                            }
                            for (int j = l - 2; j >= 0; j--)
                            {
                                if (vect2[j]==2)
                                    tower[i, j + 1] = vect2[j];
                            }

                            //tower[i, 0] = 0;
                        }
                    }
                    break;
            }
        }

        private static void PositionRock(int i, int[,] tower)
        {
            
            switch (i)
            {
                case 0:
                    tower[height + 3, 2] = 2;
                    tower[height + 3, 3] = 2;
                    tower[height + 3, 4] = 2;
                    tower[height + 3, 5] = 2;
                    break;
                case 1:
                    tower[height + 3, 3] = 2;
                    tower[height + 4, 2] = 2;
                    tower[height + 4, 3] = 2;
                    tower[height + 4, 4] = 2;
                    tower[height + 5, 3] = 2;
                    break;
                case 2:
                    tower[height + 3, 2] = 2;
                    tower[height + 3, 3] = 2;
                    tower[height + 3, 4] = 2;
                    tower[height + 4, 4] = 2;
                    tower[height + 5, 4] = 2;
                    break;
                case 3:
                    tower[height + 3, 2] = 2;
                    tower[height + 4, 2] = 2;
                    tower[height + 5, 2] = 2;
                    tower[height + 6, 2] = 2;
                    break;
                case 4:
                    tower[height + 3, 2] = 2;
                    tower[height + 3, 3] = 2;
                    tower[height + 4, 2] = 2;
                    tower[height + 4, 3] = 2;
                    break;
            }
        }

        private static int GetCurrentHeight(int[,] tower)
        {
            int i = 0;
            bool exists;
            for (i = 0; i < L; i++)
            {
                exists = false;
                for (int j = 0; j < l; j++)
                {
                    if (tower[i, j] == 1)
                    {
                        exists = true;
                        break;
                    }
                }
                if(!exists)
                { break; }
            }
            return i;
        }

        private static int GetRock(long i)
        {
            return (int)(i % 5);
        }
    }

}
