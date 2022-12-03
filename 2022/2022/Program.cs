// See https://aka.ms/new-console-template for more information


using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

long rez = 0;

string[] lines = System.IO.File.ReadAllLines("input1.txt");

//foreach (var line in lines)
//{
//    int y;
//    //var m = line.Length / 2;
//    //var x1 = line.Substring(0, m);
//    //var x2 = line.Substring(m);
//    //var x = x1.Intersect(x2).ToList();

//    if (char.IsLower(x[0]))
//        y = x[0] - 'a'+1;
//    else y = x[0] - 'A' + 27;
//    rez += y;

//}

for (int i = 0; i < lines.Length-2; i+=3)
{
    var x = lines[i].Intersect(lines[i + 1]).Intersect(lines[i+2]).ToList();
    int y;
    //var m = line.Length / 2;
    //var x1 = line.Substring(0, m);
    //var x2 = line.Substring(m);
    //var x = x1.Intersect(x2).ToList();

    if (char.IsLower(x[0]))
        y = x[0] - 'a' + 1;
    else y = x[0] - 'A' + 27;
    rez += y;
}
Console.WriteLine("Hello, World!  {0}",rez);
