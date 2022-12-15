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
    class Point
    {
        public long x { get; set; }
        public long y { get; set; }

        public Point(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Item
    {
        public Point Sensor { get; set; }
        public Point Beacon { get; set; }
        public long Dist { get; set; }
        public Item(Point sensor, Point beacon)
        {
            Sensor = sensor;
            Beacon = beacon;
            Dist = Getdist(Beacon, Sensor);
        }

        private long Getdist(Point item1, Point item2)
        {
            return (Math.Abs(item1.x - item2.x) + Math.Abs(item1.y - item2.y));
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            double rez = 0;
            var items = new List<Item>();
            string[] lines = File.ReadAllLines("input1.txt");
            foreach (var line in lines)
            {
                var x = line.Split(" ");
                long.TryParse(x[0], out long x0);
                long.TryParse(x[1], out long x1);
                long.TryParse(x[2], out long x2);
                long.TryParse(x[3], out long x3);

                var item = new Item(new Point(x0, x1), new Point(x2, x3));
                items.Add(item);
            }

            long minx = int.MaxValue, miny = int.MaxValue, maxx = int.MinValue, maxy = int.MinValue;

            foreach (var item in items)
            {
                if (item.Beacon.x < minx)
                    minx = item.Beacon.x;
                if (item.Beacon.y < miny)
                    miny = item.Beacon.y;
                if (item.Beacon.x > maxx)
                    maxx = item.Beacon.x;
                if (item.Beacon.y > maxy)
                    maxy = item.Beacon.y;

                if (item.Sensor.x < minx)
                    minx = item.Sensor.x;
                if (item.Sensor.y < miny)
                    miny = item.Sensor.y;
                if (item.Sensor.x > maxx)
                    maxx = item.Sensor.x;
                if (item.Sensor.y > maxy)
                    maxy = item.Sensor.y;

            }

            //var shift = -1440697;
            ////var shift = 10;
            //// var shiftx = 20;
            //var shiftx = 2000000;
            //foreach (var item in items)
            //{
            //    item.Sensor.y += shift;
            //    item.Beacon.y += shift;
            //    item.Beacon.x += shiftx;
            //    item.Sensor.x += shiftx;
            //}


            //var notallowed = new bool[7000000];
            ////var adjustedy = 2000000 + shift;
            ////var adjustedy = 10;
            //for (int adjustedy = -1350400; adjustedy <= 4000000 + shift; adjustedy++)
            //{
            //    if (adjustedy % 100 == 0)
            //    {
            //        Console.WriteLine(adjustedy);
            //    }

            //    // for (int i = 0 + shiftx; i < 4000000 + shiftx; i++)

            //    foreach (var item in items)
            //    {
            //        //var dist = Getdist(item.Beacon, item.Sensor);
            //        if (AroundTarget(item, item.Dist, adjustedy))
            //        {
            //            var difFromTarget = Math.Abs(adjustedy - item.Sensor.y);
            //            var distleft = item.Dist - difFromTarget;
            //            MarkNotAllowedNew(distleft, notallowed, adjustedy, item);
            //        }

            //        if (item.Beacon.y == adjustedy)
            //            notallowed[item.Beacon.x] = true;
            //        if (item.Sensor.y == adjustedy)
            //            notallowed[item.Sensor.x] = true;
            //        //MarkNotAllowed(item, notallowed, adjustedy);
            //    }




            //    for (int i = 0 + shiftx; i < 4000000 + shiftx; i++)
            //    {
            //        if (notallowed[i] == false)
            //        {
            //            Console.WriteLine("x= " + (i - shiftx));
            //            Console.WriteLine("y= " + (adjustedy - shift));
            //        }

            //        notallowed[i] = false;


            //    }
            //    //Console.WriteLine(adjustedy + " ");
            //}
            //foreach (var item in items)
            //{
            //    var dist = Getdist(item.Beacon, item.Sensor);
            //    if (AroundTarget(item, dist, adjustedy))
            //    {
            //        var difFromTarget = Math.Abs(adjustedy - item.Sensor.y);
            //        var distleft = dist - difFromTarget;
            //        MarkNotAllowedNew(distleft, notallowed, adjustedy, item);
            //    }

            //    //MarkNotAllowed(item, notallowed, adjustedy);
            //}

            //foreach (var item in items)
            //{
            //    if (item.Beacon.y == adjustedy)
            //        notallowed[item.Beacon.x] = false;
            //}


            //for (int i = 0; i < 7000000; i++)
            //{
            //    if (notallowed[i])
            //        rez++;
            //}
            //distanta fata de toate sursele este mai mare decat distanta itemului
            var point = new Point(0, 0);
            var howmany = 0;
            var tot = items.Count;
            var marked = new int[4000000];
            var cand = new List<Point>();
            long xm, ym;

            foreach (var item in items)
            {
                var i = item.Dist + 1;
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x - j;
                    ym = item.Sensor.y - i + j;
                    //if (adsustedy == ym) notallowed[xm] = true;
                    //if (item.Beacon.x == xm && item.Beacon.y == ym)
                    //    found = true;
                    cand.Add(new Point(xm, ym));
                }

                //stanga-jos
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x - i + j;
                    ym = item.Sensor.y - i + j;
                    //if (adsustedy == ym) notallowed[xm] = true;
                    //if (item.Beacon.x == xm && item.Beacon.y == ym)
                    //    found = true;
                    cand.Add(new Point(xm, ym));
                }

                //jos-dr
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x + j;
                    ym = item.Sensor.y + i - j;
                    //if (adsustedy == ym) notallowed[xm] = true;
                    //if (item.Beacon.x == xm && item.Beacon.y == ym)
                    //    found = true;
                    cand.Add(new Point(xm, ym));
                }

                //dreapta-sus
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x + i - j;
                    ym = item.Sensor.y - j;
                    //if (adsustedy == ym) notallowed[xm] = true;
                    //if (item.Beacon.x == xm && item.Beacon.y == ym)
                    //    found = true;
                    cand.Add(new Point(xm, ym));
                }
            }
            //for (int i = 0; i < 4000000; i++)
            //{
            //    foreach (var item in items)
            //    {
            //        var difx = Math.Abs(item.Sensor.x - i);
            //        for (int j = 0; j < 4000000; j++)
            //        {
            //            if ((difx + Math.Abs(item.Sensor.y - j) > item.Dist))
            //                marked[j]++;
            //        }
            //    }

            //    for (int j = 0; j < 4000000; j++)
            //    {
            //        //if (marked[j] == 35)
            //        //    Console.WriteLine("Ceva");
            //        marked[j] = 0;
            //    }
            //}

            foreach (var c in cand)
            {
                howmany = 0;
                foreach (var item in items)
                {
                    if (Getdist(item.Sensor, c) > item.Dist)
                        howmany++;
                }

                if (howmany == tot)
                {
                    if (c.x is >= 0 and <= 4000000 && c.y >=0 && c.y <=4000000)
                    Console.WriteLine($"x= {c.x} y={c.y}");
                }

            }
        

        //for (int j = 0; j < 4000000; j++)
                //{
                //    point.y = j;
                //    howmany = 0;
                //    foreach (var item in items)
                //    {
                //        if (Getdist(item.Sensor, point) > item.Dist)
                //            howmany++;
                //    }

                //    if (howmany == tot)
                //        Console.WriteLine($"x= {i} y={j}");
                //}
            }
           // Console.WriteLine("sum " + rez);
        

        
        private static void MarkNotAllowedNew(long distleft, bool[] notallowed, int adjustedy, Item item)
        {
            for (long i = item.Sensor.x - distleft; i <= item.Sensor.x + distleft; i++)
                notallowed[i] = true;
        }

        private static bool AroundTarget(Item item, long dist, int adjustedy)
        {
            var ymax = item.Sensor.y + dist;
            var ymin = item.Sensor.y - dist;
            if (ymin <= adjustedy && adjustedy <= ymax)
                return true;
            return false;
        }

        private static long Getdist(Point item1, Point item2)
        {
            return (Math.Abs(item1.x - item2.x) + Math.Abs(item1.y - item2.y));
        }

        private static void MarkNotAllowed(Item item, bool[] notallowed, int adsustedy)
        {
            var i = 1;
            long xm, ym;
            var found = false;
            while (true)
            {
                //sus-stg
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x - j;
                    ym = item.Sensor.y - i + j;
                    if (adsustedy == ym) notallowed[xm] = true;
                    if (item.Beacon.x == xm && item.Beacon.y == ym)
                        found = true;
                }
                //stanga-jos
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x - i + j;
                    ym = item.Sensor.y - i + j;
                    if (adsustedy == ym) notallowed[xm] = true;
                    if (item.Beacon.x == xm && item.Beacon.y == ym)
                        found = true;
                }
                //jos-dr
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x + j;
                    ym = item.Sensor.y + i - j;
                    if (adsustedy == ym) notallowed[xm] = true;
                    if (item.Beacon.x == xm && item.Beacon.y == ym)
                        found = true;
                }
                //dreapta-sus
                for (int j = 0; j < i; j++)
                {
                    xm = item.Sensor.x + i - j;
                    ym = item.Sensor.y - j;
                    if (adsustedy == ym) notallowed[xm] = true;
                    if (item.Beacon.x == xm && item.Beacon.y == ym)
                        found = true;
                }

                if (found)
                    break;
                i++;
            }
        }
    }

}
