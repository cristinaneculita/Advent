// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

long rez = 0;

string[] lines = System.IO.File.ReadAllLines("input1.txt");

var dir = new Dir("/");
int i = 1;

dir = ReadDir(lines, ref i, dir);

Dir ReadDir(string[] lines, ref int i, Dir dir )
{
    while (true)
    {
        
        if (lines[i] == "$ ls")
        {
            i++;
           
            while (i<lines.Length && lines[i][0] != '$' )
            {
                if (lines[i][0] == 'd')
                {
                    var x = lines[i].Split(" ");
                    var r = new Dir(x[1]);
                    dir.Dirs.Add(r);
                }
                else
                {
                    var x = lines[i].Split(" ");
                    long.TryParse(x[0], out long x0);
                    dir.Files.Add(new Fil(x[1], x0));
                }

                i++;
            }
        }
        if (i == lines.Length)
        {
            break;

        }
        if (lines[i] == "$ cd ..")
        {
            i++;
            return dir;
        }
        
        while (i < lines.Length && lines[i] != "$ cd ..")
        {
            if (lines[i].StartsWith("$ cd") && !lines[i].EndsWith("..") && lines[i] != "$ cd /")
            {
                var x = lines[i].Split(" ");
                var r = dir.Dirs.FirstOrDefault(d => d.Name == x[2]);
                i++;
                ReadDir(lines, ref i, r);
            }
        }
        if (i == lines.Length)
        {
            break;

        }
        if (i < lines.Length && lines[i] == "$ cd /")
        {
            i++;
            ReadDir(lines, ref i, dir);
        }

        if (dir.Name != "/" && i==lines.Length-1)
            break;
    }

    return dir;
}

List<long> l = new List<long>();
CalcL(dir);

void CalcL(Dir dir)
{
    l.Add(Calcm(dir));
    foreach (var dir1 in dir.Dirs)
    {
        CalcL(dir1);
    }
}

long Calcm(Dir dir)
{
    long l = 0;
    foreach (var dirFile in dir.Files)
    {
        l += dirFile.Size;
    }

    foreach (var dirDir in dir.Dirs)
    {
        l += Calcm(dirDir);
    }
    return l;
}




//ist<long> lm = new List<long>();
long s = 0;
foreach (var l1 in l)
{
    if(l1<100000)
        s += l1;
}

Console.WriteLine(s);


long systemm = 70000000;
long need = 30000000;
var used = l.Max();
var unused = systemm - used;
var needed = need - unused;
var candidates = new List<long>();
foreach (var l1 in l)
{
    if (l1 > needed)
        candidates.Add(l1);
}

Console.WriteLine(candidates.Min());
long MaxSumSubset(List<long> values, int limit)
{
    long max = 0;
    var n = values.Count;
    for (int j = 1; j <= n; j++)
    {
        for (int stIndex = 0; stIndex < n; stIndex++)
        {
            if (stIndex + j > n)
                break;
            long sum = 0;
            for (int k = stIndex; k < stIndex+j; k++)
            {
                sum += values[k];
            }
           
            if (sum > max)
            {
                max = sum;
               
            }
        }
    }

    return max;
}
bool isSubsetSum(List<long> set, int n, long sum)
{

    // Base Cases
    if (sum == 0)
        return true;
    if (n == 0)
        return false;

    // If last element is greater than sum,
    // then ignore it
    if (set[n - 1] > sum)
        return isSubsetSum(set, n - 1, sum);

    /* else, check if sum can be obtained by any 
of the following:
      (a) including the last element
      (b) excluding the last element   */
    return isSubsetSum(set, n - 1, sum)
           || isSubsetSum(set, n - 1, sum - set[n - 1]);
}
class Dir
{
    public List<Dir> Dirs { get; set; }
    public List<Fil> Files { get; set; }
    public string Name { get; set; }

    public Dir(List<Dir> dirs, List<Fil> files)
    {
        Dirs = dirs;
        Files = files;
    }

    public Dir(string name)
    {
        Dirs =new List<Dir>();
        Files = new List<Fil>();
        Name = name;
    }
}
internal class Fil
{
    public string Name { get; set; }
    public long Size { get; set; }

    public Fil(string name, long size)
    {
        Name = name;
        Size = size;
    }
}

   
        
         