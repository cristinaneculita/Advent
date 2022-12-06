// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

long rez = 0;

string[] lines = System.IO.File.ReadAllLines("input1.txt");
var x = 13;
var line = lines[0];
while(true)
{  
    //if (line[x] != line[x - 1]
    //   && line[x - 1] != line[x - 2] && line[x] != line[x-2]
    //   && line[x - 3] != line[x-2] && line[x - 3] != line[x-1] && line[x - 3] != line[x])
    var isok = true;
    for(var i = x-13; i <= x;i++)
    for (int j = x-13; j <= x; j++)
    {
        if(i==j)
            continue;
        if (line[i] == line[j])
        {
            isok = false;
            break;
        }

    }

    if (isok)
    {
        Console.WriteLine("Hello, World!  {0}", x+1);
        break;
    }
    x++;

}
   

