// See https://aka.ms/new-console-template for more information


using System.Runtime.CompilerServices;

var rez = 0;

string[] lines = System.IO.File.ReadAllLines("input1.txt");

foreach (var line in lines)
{
    switch (line)
    {
        case "A X": rez += 3;
            break;
        case "A Y":
            rez += 1+3;
            break;

        case "A Z":
            rez += 2+6;
            break;

        case "B X":
            rez += 1;
            break;

        case "B Y":
            rez += 2 +3;
            break;

        case "B Z":
            rez += 3+6;
            break;

        case "C X":
            rez += 2;
            break;

        case "C Y":
            rez += 3+3;
            break;

        case "C Z":
            rez += 1+6;
            break;

    }
}
Console.WriteLine("Hello, World!  {0}",rez);
