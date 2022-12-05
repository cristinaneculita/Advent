// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

long rez = 0;

string[] lines = System.IO.File.ReadAllLines("input1.txt");
List<Stack> list = new List<Stack>();


Stack s = new Stack();
s.Push('L');
s.Push('N');
s.Push('W');
s.Push('T');
s.Push('D');
list.Add(s);
s = new Stack();
s.Push('C');
s.Push('P');
s.Push('H');
list.Add(s);

s=new Stack();
s.Push('W');
s.Push('P');
s.Push('H');
s.Push('N');
s.Push('D');
s.Push('G');
s.Push('M');
s.Push('J');
list.Add(s);

s = new Stack();
s.Push('C');
s.Push('W');
s.Push('S');
s.Push('N');
s.Push('T');
s.Push('Q');
s.Push('L');
list.Add(s);

s = new Stack();
s.Push('P');
s.Push('H');
s.Push('C');
s.Push('N');
list.Add(s);

s = new Stack();
s.Push('T');
s.Push('H');
s.Push('N');
s.Push('D');
s.Push('M');
s.Push('W');
s.Push('Q');
s.Push('B');
list.Add(s);

s = new Stack();
s.Push('M');
s.Push('B');
s.Push('R');
s.Push('J');
s.Push('G');
s.Push('S');
s.Push('L');
list.Add(s);

s = new Stack();
s.Push('Z');
s.Push('N');
s.Push('W');
s.Push('G');
s.Push('V');
s.Push('B');
s.Push('R');
s.Push('T');
list.Add(s);

s = new Stack();
s.Push('W');
s.Push('G');
s.Push('D');
s.Push('N');
s.Push('P');
s.Push('L');
list.Add(s);

foreach (var line in lines)
{
    var x = line.Split(" ");
    int.TryParse(x[0], out int x0);
    int.TryParse(x[1], out int x1);
    int.TryParse(x[2], out int x2);
    var d = new Stack();
    for (int i = 0; i < x0; i++)
    {
        var y = list[x1-1].Pop();
      d.Push(y);
    }
    for (int i = 0; i < x0; i++)
    {
        var y = d.Pop();
        list[x2 - 1].Push(y);
    }
    
}

Console.WriteLine(list[0].Pop().ToString()+
                  list[1].Pop().ToString()+
                  list[2].Pop().ToString()+
                  list[3].Pop().ToString()+ 
                  list[4].Pop().ToString()+ 
                  list[5].Pop().ToString()+ 
                  list[6].Pop().ToString()+ 
                  list[7].Pop().ToString()+
                  list[8].Pop().ToString());
Console.WriteLine("Hello, World!  {0}",rez);
