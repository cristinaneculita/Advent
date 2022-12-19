using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace Advent2022
{

    public class Blueprint

    {
        public int Id { get; set; }
        public int OreRobot { get; set; }
        public int ClayRobot { get; set; }
        public Tuple<int, int> Obsidian { get; set; }
        public Tuple<int, int> Geode { get; set; }

        public Blueprint(int id, int oreRobot, int clayRobot, Tuple<int, int> obsidian, Tuple<int, int> geode)
        {
            Id = id;
            OreRobot = oreRobot;
            ClayRobot = clayRobot;
            Obsidian = obsidian;
            Geode = geode;
            MaxObs = Math.Max(Math.Max(oreRobot, clayRobot), Math.Max(obsidian.Item1, geode.Item1));
        }

        public int MaxObs { get; set; }
    }

    public class state : IEquatable<state>
    {
        public int Ore { get; set; }
        public int Clay { get; set; }
        public int Obs { get; set; }
        public int Geode { get; set; }
        public int OreRObs { get; set; }
        public int ClayRobs { get; set; }
        public int ObsRobs { get; set; }
        public int GeodeRobs { get; set; }
        public int Time { get; set; }

        public state(int ore, int clay, int obs, int geode, int oreRObs, int clayRobs, int obsRobs, int geodeRobs, int time)
        {
            Ore = ore;
            Clay = clay;
            Obs = obs;
            Geode = geode;
            OreRObs = oreRObs;
            ClayRobs = clayRobs;
            ObsRobs = obsRobs;
            GeodeRobs = geodeRobs;
            Time = time;
        }

        public bool Equals(state? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Ore == other.Ore && Clay == other.Clay && Obs == other.Obs && Geode == other.Geode && OreRObs == other.OreRObs && ClayRobs == other.ClayRobs && ObsRobs == other.ObsRobs && GeodeRobs == other.GeodeRobs && Time == other.Time;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((state)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Ore);
            hashCode.Add(Clay);
            hashCode.Add(Obs);
            hashCode.Add(Geode);
            hashCode.Add(OreRObs);
            hashCode.Add(ClayRobs);
            hashCode.Add(ObsRobs);
            hashCode.Add(GeodeRobs);
            hashCode.Add(Time);
            return hashCode.ToHashCode();
        }
    }

    public class Program
    {
        private static Dictionary<state, int> dict;

        static void Main(string[] args)
        {
            var rez = 0;
            string[] lines = File.ReadAllLines("input1.txt");
            //var sum1 = 0;
            //int i = 1;
            //foreach (var line in lines)
            //{
            //    var x = int.TryParse(line, out int xx);
            //    sum1 += i * xx;
            //    i++;
            //}
            //Console.WriteLine("sum1 "+ sum1);
            var blues = new List<Blueprint>();
            foreach (var line in lines)
            {
                var x = line.Split(" ");
                int.TryParse(x[0], out int x0);
                int.TryParse(x[1], out int x1);
                int.TryParse(x[2], out int x2);
                int.TryParse(x[3], out int x3);
                int.TryParse(x[4], out int x4);
                int.TryParse(x[5], out int x5);
                int.TryParse(x[6], out int x6);

                var blu = new Blueprint(x0, x1, x2, new Tuple<int, int>(x3, x4), new Tuple<int, int>(x5, x6));

                blues.Add(blu);
            }

            long sum = 0;
            Random r = new Random();
            foreach (var blu in blues)
            {


                int ore = 0;
                int clay = 0;
                int obsidian = 0;
                int geode = 0;
                int OreRobs = 1;
                int ClayRobs = 0;
                int ObsRob = 0;
                int GeodeRob = 0;
                dict = new Dictionary<state, int>();
                var x = GetMaxGeode(ore, clay, obsidian, geode, OreRobs, ClayRobs, ObsRob, GeodeRob, 32, blu, r);
                sum += x * blu.Id;
                Console.WriteLine(x);
                //for (int i = 0; i < 24; i++)
                //{
                //    bool createdGeode, createdObsidianRob, createdClayRob, createdOreRob;
                //    if (ore >= blu.Geode.Item1 && obsidian >= blu.Geode.Item2)
                //    {
                //        createdGeode = true;
                //        ore -= blu.Geode.Item1;
                //        obsidian-= blu.Geode.Item2;
                //    }
                //    else if (ore >= blu.Obsidian.Item1 && clay >= blu.Obsidian.Item2)
                //    {
                //        createdObsidianRob = true;
                //        ore-= blu.Obsidian.Item1;
                //        clay-= blu.Obsidian.Item2;
                //    }
                //    else if(clay)

                //    ore = ore + OreRobs;
                //    if (ore > blu.OreRobot)
                //    {
                //        ore-=blu.OreRobot;
                //        OreRobs++;
                //    }


                //    clay = clay + ClayRobs;
                //    if(clay)



                //}




            }

            Console.WriteLine("sum " + sum);

        }

        private static int GetMaxGeode(int ore, int clay, int obsidian, int geode, int oreRobs, int clayRobs,
            int obsRob, int geodeRob, int time, Blueprint blu, Random r)
        {
            int gain = 0;
            state x = new state(ore, clay, obsidian, geode, oreRobs, clayRobs,
            obsRob, geodeRob, time);
            if (dict.ContainsKey(x))
                return dict[x];
            if (time == 0)
            {
                //if (!dict.ContainsKey(x))
                //   dict.Add(x,geode);
                return geode;
            }

            var best = 0;
            //pick to make geode robot
            if (ore >= blu.Geode.Item1 && obsidian >= blu.Geode.Item2)
            {
                //x = new state(ore + oreRobs - blu.Geode.Item1, clay + clayRobs, obsidian + obsRob - blu.Geode.Item2,
                //    geode + geodeRob, oreRobs,
                //    clayRobs, obsRob, geodeRob + 1, time - 1);
                //if (dict.ContainsKey(x))
                //    gain = dict[x];
                //else
                //{
                    gain = GetMaxGeode(ore + oreRobs - blu.Geode.Item1, clay + clayRobs,
                        obsidian + obsRob - blu.Geode.Item2, geode + geodeRob, oreRobs,
                        clayRobs, obsRob, geodeRob + 1, time - 1, blu, r);
                    //if(x.Time!=0) dict.TryAdd(x, gain);
                //}
                if (gain > best)
                {
                    best = gain;
                }
            }
        
        //pick to make obsidian robot

        if (ore >= blu.Obsidian.Item1 && clay >= blu.Obsidian.Item2)
            {
                //x = new state(ore + oreRobs - blu.Obsidian.Item1, clay + clayRobs - blu.Obsidian.Item2,
                //    obsidian + obsRob, geode + geodeRob, oreRobs,
                //    clayRobs, obsRob + 1, geodeRob, time - 1);
                //if (dict.ContainsKey(x))
                //    gain = dict[x];
                //else
                //{
                    gain = GetMaxGeode(ore + oreRobs - blu.Obsidian.Item1, clay + clayRobs - blu.Obsidian.Item2,
                        obsidian + obsRob, geode + geodeRob, oreRobs,
                        clayRobs, obsRob + 1, geodeRob, time - 1, blu, r);
                    //if (x.Time != 0) dict.TryAdd(x, gain);
               // }

                if (gain > best)
                {
                    best = gain;
                }
            }
            //pick to make a clay robbot
            // else if(ore >= blu.ClayRobot || ore >= blu.OreRobot)
            // {
            if (ore >= blu.ClayRobot && clayRobs < blu.Obsidian.Item2)
            {
                //x = new state(ore + oreRobs - blu.ClayRobot, clay + clayRobs, obsidian + obsRob,
                //    geode + geodeRob, oreRobs,
                //    clayRobs + 1, obsRob, geodeRob, time - 1);
                //if (dict.ContainsKey(x))
                //    gain = dict[x];
                //else
                //{
                    gain = GetMaxGeode(ore + oreRobs - blu.ClayRobot, clay + clayRobs, obsidian + obsRob,
                        geode + geodeRob, oreRobs,
                        clayRobs + 1, obsRob, geodeRob, time - 1, blu, r);

                    //if (x.Time != 0) dict.TryAdd(x, gain);
               // }

                if (gain > best)
                {
                    best = gain;
                }
            }

            //pick to make a ore robot
           // var maxorbSpent = Math.Max(blu.ClayRobot, blu.Obsidian.Item1,blu.Geode.Item1);
            if (ore >= blu.OreRobot && oreRobs < blu.MaxObs)
            {
                //x = new state(ore + oreRobs - blu.OreRobot, clay + clayRobs, obsidian + obsRob,
                //    geode + geodeRob, oreRobs + 1, clayRobs, obsRob, geodeRob, time - 1);
                //if (dict.ContainsKey(x))
                //    gain = dict[x];
                //else
                //{
                    gain = GetMaxGeode(ore + oreRobs - blu.OreRobot, clay + clayRobs, obsidian + obsRob,
                        geode + geodeRob, oreRobs + 1,
                        clayRobs, obsRob, geodeRob, time - 1, blu, r);
                   // if (x.Time != 0) dict.TryAdd(x, gain);
               // }

                if (gain > best)
                {
                    best = gain;
                }
            }
            //   }
            //   else
            //{
            //if (dict.ContainsKey(x))
            //    gain = dict[x];
            //else { 
            //x = new state(ore + oreRobs, clay + clayRobs, obsidian + obsRob, geode + geodeRob,
            //    oreRobs, clayRobs, obsRob, geodeRob, time - 1);
            //if (dict.ContainsKey(x))
            //    gain = dict[x];
            //else
            //{
            gain = GetMaxGeode(ore + oreRobs, clay + clayRobs, obsidian + obsRob, geode + geodeRob,
                    oreRobs, clayRobs, obsRob, geodeRob, time - 1, blu, r);

                
            //}

            //}
            if (gain > best)
                best = gain;
            //}

            if (x.Time != 0) dict.TryAdd(x, gain);
            return best;

        }
    }
}