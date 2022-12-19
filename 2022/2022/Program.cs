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
using System.Windows.Markup;

namespace Advent2022
{

    public class Cubelet
    {
        public int x { get; set; }
        public int y  { get; set; }
        public int z { get; set; }

        public Cubelet(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            SE = 6;
        }

        public int SE {get; set; }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            var rez = 0;
            string[] lines = File.ReadAllLines("input1.txt");
            var cubes = new List<Cubelet>();
            foreach (var line in lines)
            {
                var x = line.Split(",");
                int.TryParse(x[0], out int x0);
                int.TryParse(x[1], out int x1);
                int.TryParse(x[2], out int x2);

                cubes.Add(new Cubelet(x0,x1,x2));

            }
            rez =SumCubes(cubes);




            var rez2 =PartTwo(lines);
     

            Console.WriteLine("sum " + rez);
            Console.WriteLine(rez2);

        }
        public static string PartTwo(string[] lines)
        {
            var neighbors = new List<(int x, int y, int z)> { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };
            var linesi = lines.Select(x => x.Split(",").Select(int.Parse).ToArray());
            var cubes = linesi.Select(cube => (x: cube[0], y: cube[1], z: cube[2])).ToHashSet();
            var answer = 0;
            var maxX = cubes.Max(x => x.x);
            var maxY = cubes.Max(y => y.y);
            var maxZ = cubes.Max(z => z.z);
            var minX = cubes.Min(x => x.x);
            var minY = cubes.Min(y => y.y);
            var minZ = cubes.Min(z => z.z);

            var xRange = Enumerable.Range(minX, maxX + 1).ToList();
            var yRange = Enumerable.Range(minY, maxY + 1).ToList();
            var zRange = Enumerable.Range(minZ, maxZ + 1).ToList();


            bool isOutside((int x, int y, int z) cube)
            {
                if (cubes.Contains(cube)) return false;

                var checkedCubes = new HashSet<(int x, int y, int z)>();
                var queue = new Queue<(int x, int y, int z)>();
                queue.Enqueue(cube);
                while (queue.Any())
                {
                    var tempCube = queue.Dequeue();
                    if (checkedCubes.Contains(tempCube)) continue;
                    checkedCubes.Add(tempCube);
                    if (!xRange.Contains(tempCube.x) || !yRange.Contains(tempCube.y) || !zRange.Contains(tempCube.z))
                    {
                        return true;
                    }
                    if (!cubes.Contains(tempCube))
                    {
                        foreach (var (dx, dy, dz) in neighbors)
                        {
                            queue.Enqueue((tempCube.x + dx, tempCube.y + dy, tempCube.z + dz));
                        }
                    }
                }
                return false;
            }
            foreach (var (x, y, z) in cubes)
            {
                foreach (var (dx, dy, dz) in neighbors)
                {
                    if (isOutside((x + dx, y + dy, z + dz)))
                    {
                        answer++;
                    }
                }
            }
            return answer.ToString();
        }

        //private static List<Cubelet> FindTrappedCube(List<Cubelet> cubes, int i, int j, Tuple<bool, bool, bool, bool> sameLine)
        //{
        //    if (!sameLine.Item3)
        //    {
        //        var ctrline = FindAllCubesInLineZ(cubes[i], cubes[j]);
        //        foreach (var cubeInLine in ctrline)
        //        {
        //            //daca are vecini pe x si pe y adauga la posibilul cub intern ce e intre vecini
        //            //verifica daca e cub intern
        //            var nx = FindNeighx(cubes, cubeInLine);
        //            var ny = FindNeighy(cubes, cubeInLine);
        //            if()
        //            if (nx.Any() && ny.Any())
        //            {
        //                var candidate = FindCand(nx[0], nx[1], ny[0], ny[1], cubes[i], cubes[j]);
        //                if (InternCube(cubes, candidate))
        //                {
        //                    return candidate;
        //                }
        //            }
        //        }
        //    }
        //    if (!sameLine.Item2)
        //    {
        //        var ctrline = FindAllCubesInLineY(cubes[i], cubes[j]);
        //        foreach (var cubeInLine in ctrline)
        //        {
        //            //daca are vecini pe x si pe y adauga la posibilul cub intern ce e intre vecini
        //            //verifica daca e cub intern
        //            var nx = FindNeighx(cubes, cubeInLine);
        //            var nz = FindNeighz(cubes, cubeInLine);
        //            if (nx.Any() && nz.Any())
        //            {
        //                var candidate = FindCand(nx[0], nx[1], cubes[i], cubes[j], nz[0], nz[1]);
        //                if (InternCube(cubes, candidate))
        //                {
        //                    return candidate;
        //                }
        //            }
        //        }
        //    }
        //    if (!sameLine.Item1)
        //    {
        //        var ctrline = FindAllCubesInLineX(cubes[i], cubes[j]);
        //        foreach (var cubeInLine in ctrline)
        //        {
        //            //daca are vecini pe x si pe y adauga la posibilul cub intern ce e intre vecini
        //            //verifica daca e cub intern
        //            var nx = FindNeighy(cubes, cubeInLine);
        //            var nz = FindNeighz(cubes, cubeInLine);
        //            if (nx.Any() && nx.Any())
        //            {
        //                var candidate = FindCand(cubes[i], cubes[j],nx[0], nx[1], nz[0], nz[1]);
        //                if (InternCube(cubes, candidate))
        //                {
        //                    return candidate;
        //                }
        //            }
        //        }
        //    }
        //    return new List<Cubelet>();
        //}

        //private static List<Cubelet> FindNeighz(List<Cubelet> cubes, Cubelet cubeInLine)
        //{
        //    throw new NotImplementedException();
        //}

        //private static IEnumerable FindAllCubesInLineZ(Cubelet cube, Cubelet cubelet)
        //{
        //    throw new NotImplementedException();
        //}

        //private static bool InternCube(List<Cubelet> cubes, List<Cubelet> candidate)
        //{
        //    var totlist = new List<Cubelet>(cubes);
        //    totlist.AddRange(candidate);
        //    foreach (var cubelet in candidate)
        //    {
        //        if (!IsTrappedEx(totlist, cubelet))
        //            return false;
        //    }

        //    return true;
        //}

        //private static bool IsTrappedEx(List<Cubelet> totlist, Cubelet cubelet)
        //{
        //    var nz = totlist.Any(c => c.x == cubelet.x && c.y == cubelet.y && Math.Abs(c.z - cubelet.z) == 1);
        //    if (nz == false)
        //        return false;
        //    var ny = totlist.Any(c => c.x == cubelet.x && c.z == cubelet.z && Math.Abs(c.y - cubelet.y) == 1);
        //    if(ny == false) 
        //        return false;
        //    var nx = totlist.Any(c => c.y == cubelet.y && c.z == cubelet.z && Math.Abs(c.x - cubelet.x) == 1);
        //    return nx;

        //}

        //private static List<Cubelet> FindCand(Cubelet cubeletx1, Cubelet cubeletx2, Cubelet cubelety1, Cubelet cubelety2, Cubelet cubeletz1, Cubelet cubeletz2)
        //{
        //    var res = new List<Cubelet>();
        //    int maxx = Math.Max(cubeletx1.x, cubeletx2.x);
        //    int minx = Math.Min(cubeletx1.x, cubeletx2.x);

        //    int maxy = Math.Max(cubelety1.y, cubelety2.y);
        //    int miny = Math.Min(cubelety1.y, cubelety2.y);

        //    int maxz = Math.Max(cubeletz1.z, cubeletz2.z);
        //    int minz = Math.Min(cubeletz1.z, cubeletz2.z);

        //    for (int i = minx+1; i < maxx; i++)
        //    {
        //        for (int j = miny+1; j < maxy; j++)
        //        {
        //            for (int k = minz+1; k < maxz; k++)
        //            {
        //                res.Add(new Cubelet(i,j,k));
        //            }
        //        }
        //    }

        //    return res;
        //}

        //private static List<Cubelet> FindAllCubesInLineY(Cubelet cubelet, Cubelet cubelet1)
        //{
        //    throw new NotImplementedException();
        //}

        //private static List<Cubelet> FindAllCubesInLineX(Cubelet cubelet, Cubelet cubelet1)
        //{
        //    throw new NotImplementedException();
        //}

        //private static List<Cubelet> FindNeighy(List<Cubelet> cubes, object cubeInLine)
        //{
        //    throw new NotImplementedException();
        //}

        //private static List<Cubelet> FindNeighx(List<Cubelet> cubes, object cubeInLine)
        //{
        //    throw new NotImplementedException();
        //}

        private static int SumCubes(List<Cubelet> cubes)
        {
            for (int i = 0; i < cubes.Count; i++)
            {
                for (int j = i + 1; j < cubes.Count; j++)
                {
                    if (IsNeigh(cubes[i], cubes[j]))
                    {
                        cubes[i].SE--;
                        cubes[j].SE--;
                    }
                }
            }
            return cubes.Sum(x => x.SE);
        }

        private static Tuple<bool,bool,bool,bool> SameLine(Cubelet cube1, Cubelet cube2)
        {
            if (cube1.x == cube2.x && cube1.y == cube2.y && cube1.z != cube2.z)
                return new Tuple<bool, bool, bool, bool>(true, true,true,false);
            if (cube1.x == cube2.x && cube1.z == cube2.z && cube1.y != cube2.y)
                return new Tuple<bool, bool, bool, bool>(true, true,false,true);
            if (cube1.y == cube2.y && cube1.z == cube2.z && cube1.x != cube2.x)
                return new Tuple<bool, bool, bool, bool>(true,false,true,true);
            return new Tuple<bool, bool, bool, bool>(false,false,false,false);
        }

        private static bool IsNeigh(Cubelet cube1, Cubelet cube2)
        {
            if(cube1.x == cube2.x && cube1.y == cube2.y && Math.Abs(cube1.z-cube2.z)==1)
                return true;
            if(cube1.x == cube2.x && cube1.z == cube2.z && Math.Abs(cube1.y - cube2.y) == 1)
                return true;
            if (cube1.y == cube2.y && cube1.z == cube2.z && Math.Abs(cube1.x - cube2.x) == 1)
                return true;
            return false;
        }
    }
}