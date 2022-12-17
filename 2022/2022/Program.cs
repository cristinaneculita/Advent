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

namespace Advent2022
{


    class Valve
    {
        public string Name { get; set; }
        public int Rate { get; set; }
        public List<string> ValvesTo { get; set; }

        public Valve(string name, int rate, List<string> valvesTo)
        {
            Name = name;
            Rate = rate;
            ValvesTo = valvesTo;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var rez = 0;
            string[] lines = File.ReadAllLines("input1.txt");
            var vales = new List<Valve>();
            var count = lines.Length;
            foreach (var line in lines)
            {
                var x = line.Split("; ");
                var y = x[0].Split(" ");
                int.TryParse(y[1], out int y1);
                var z = x[1].Split(", ").ToList();
                var valve = new Valve(y[0], y1, z);
                vales.Add(valve);
            }

            var ci = vales.Count(r => r.Rate != 0);
            ci++;
            var paths = new int[count, count];

            var interestValves = new List<Valve>();
            interestValves.Add(vales.FirstOrDefault(v=>v.Name=="AA"));
            interestValves.AddRange(vales.Where(r => r.Rate != 0).ToList());
            
            foreach (var valve1 in vales)
            {
                var i =vales.IndexOf(valve1);
                foreach (var valve2 in valve1.ValvesTo)
                {
                    var j = vales.FindIndex(x => x.Name == valve2);
                    // paths[i, j] = new List<int>();
                    // paths[i, j].Add(j);
                    paths[i, j] = 1;
                } 
            }

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (paths[i, j] == 0 && i != j)
                        FindShortestPath(paths, i, j, count);
                }
            }
            var pathsInt = new int[ci, ci];
            var nameint = interestValves.Select(iv => iv.Name);
            var indi = 0;
            int indj = 0;
            for (int i = 0; i < count; i++)
            {
                if (nameint.Contains(vales[i].Name))
                {
                    for (int j = 0; j < count; j++)
                    {

                        if (nameint.Contains(vales[j].Name))
                        {
                            pathsInt[indi, indj] = paths[i, j];
                            indj++;
                        }
                    }
                    indi++;
                    indj = 0;
                }
            }


            //var min = 0;
            //while (min < 30)
            //{
            //    var x = FindMostEfective(paths, valves);
            //    GoThere(x, paths, valves, ref min);



            //}
            var nodeTurnedOn = new List<Valve>();
            nodeTurnedOn.Add(interestValves[0]);
            rez = (int)FindMax(0, 30,30, nodeTurnedOn, pathsInt, interestValves);

            Console.WriteLine("sum " + rez);

        }

        private static long FindMax(int start, int timeToGO1, int timetogo2, List<Valve> nodesTurnedOn, int[,] pathsInt, List<Valve> interestValves)
        {
            long best = 0;
           //  var currentValve = interestValves[start];
            // var currentTurnedOn = new List<Valve>(nodesTurnedOn);
            foreach (var valve in interestValves)
            {
                if (!nodesTurnedOn.Contains(valve) && interestValves[start]!=valve)
                {
                    var ind = interestValves.IndexOf(valve);

                    int newTimeToGo1 = timeToGO1 - pathsInt[start, ind] - 1;
                    nodesTurnedOn.Add(valve);
                    foreach (var valve2 in interestValves)
                    {
                        if (!nodesTurnedOn.Contains(valve2) && interestValves[start] != valve2)
                        {

                            int newtimetogo2 = timeToGO1 - pathsInt[start,ind] - 1;
                        }
                    }


                    if (newTimeToGo1 > 0)
                    {
                        long gain = newTimeToGo1 * valve.Rate +
                                    FindMax(ind, newTimeToGo1, nodesTurnedOn, pathsInt, interestValves);
                        if (gain > best)
                        {
                            best = gain;
                        }
                    }

                    nodesTurnedOn.Remove(valve);
                }
            }

            return best;


        }

       
        //int minDistance(int[] dist, bool[] sptSet)
        //{
        //    // Initialize min value
        //    int min = int.MaxValue, min_index = -1;

        //    for (int v = 0; v < V; v++)
        //        if (sptSet[v] == false && dist[v] <= min)
        //        {
        //            min = dist[v];
        //            min_index = v;
        //        }

        //    return min_index;
        //}
        static int[] dijkstra(int[,] graph, int src, int V)
        {
            int[] dist
                = new int[V]; // The output array. dist[i]
            // will hold the shortest
            // distance from src to i

            // sptSet[i] will true if vertex
            // i is included in shortest path
            // tree or shortest distance from
            // src to i is finalized
            bool[] sptSet = new bool[V];

            // Initialize all distances as
            // INFINITE and stpSet[] as false
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex
            // from itself is always 0
            dist[src] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                // Pick the minimum distance vertex
                // from the set of vertices not yet
                // processed. u is always equal to
                // src in first iteration.
                int u = minDistance(dist, sptSet, V);

                // Mark the picked vertex as processed
                sptSet[u] = true;

                // Update dist value of the adjacent
                // vertices of the picked vertex.
                for (int v = 0; v < V; v++)

                    // Update dist[v] only if is not in
                    // sptSet, there is an edge from u
                    // to v, and total weight of path
                    // from src to v through u is smaller
                    // than current value of dist[v]
                    if (!sptSet[v] && graph[u, v] != 0
                                   && dist[u] != int.MaxValue
                                   && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }

            // print the constructed distance array
            return dist;
        }
        static int minDistance(int[] dist, bool[] sptSet, int V)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }
        private static void FindShortestPath(int[,] paths, int i, int j, int count)
        {
            
           var r =  dijkstra(paths, i, count);
           for (int k = 0; k < count; k++)
           {
                paths[i, k] = r[k];
           }
            //var queue = new Stack<int>();
            //bool[] visited = new bool[count];
            //visited[i] = true;
            //queue.Push(i);
            //var dist = 0;
            //while (queue.Any())
            //{
            //    var f = queue.Pop();
            //    dist++;
            //    for (int k = 0; k < count; k++)
            //    {
            //        if (paths[f, k] == 1)
            //        {
            //            if(k==j)
            //                return dist;
            //            if (!visited[k])
            //            {
            //                visited[k]= true;
            //                queue.Push(k);
            //            }
            //        }

            //    }
            //}

            //return int.MaxValue;
        }

        //private static int Dijkstra(int[,] mat, int ii,int jj, int count )
        //{
        //    int dist = 0;
           
        //    var x = ii; var y = jj;
        //    var matdist = new int[count, count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        for (int j = 0; j < count; j++)
        //        {
        //            matdist[i, j] = Int32.MaxValue;
        //        }
        //    }
        //    matdist[0, 0] = 0;

          
        //}
        
    }

}
