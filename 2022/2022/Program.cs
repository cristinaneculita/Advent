// See https://aka.ms/new-console-template for more information


using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

long rez = 0;

string[] lines = System.IO.File.ReadAllLines("input1.txt");

foreach (var line in lines)
{

    var x = line.Split(",");
    var y1 = x[0].Split("-");
    var y2 = x[1].Split("-");
    int.TryParse(y1[0], out int z1);
    int.TryParse(y1[1], out int z2);
    int.TryParse(y2[0], out int z3);
    int.TryParse(y2[1], out int z4);
    if (z1 <= z3 && z2 >= z4 || z3 <= z1 && z4 >= z2)
        rez++;
   
    //var l1 = new List<int>();
    //for (int i = z1; i <=z2; i++)
    //{
    //    l1.Add(i);
    //}
    //var l2 = new List<int>();
    //for (int i = z3; i <= z4; i++)
    //{
    //    l2.Add(i);
    //}

    //if (l1.Intersect(l2).Any())
    //    rez++;
}

Console.WriteLine("Hello, World!  {0}",rez);
