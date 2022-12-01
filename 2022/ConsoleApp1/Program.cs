// See https://aka.ms/new-console-template for more information


using System.Runtime.CompilerServices;

var rez = 0;
var listr = new List<long>();
string[] lines = System.IO.File.ReadAllLines("input.txt");
long s = 0;
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {

        listr.Add(s);
        s = 0;
    }
    else
    {
        int.TryParse(line, out int r);
        s += r;
    }
}

 s = 0;
var m = listr.Max();
s += (long)m;

listr.Remove(m);
m = listr.Max();
s += (long)m;

listr.Remove(m);
m = listr.Max();
s += (long)m;

listr.Remove(m);

Console.WriteLine("Hello, World!  {0}",s);
