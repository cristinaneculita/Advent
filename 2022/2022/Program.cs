// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;


long rez = 0;
string[] lines = System.IO.File.ReadAllLines("input1.txt");
int L = 2000;
int sx = 1000;
int sy = 1000;
//int L = 6;
//int sx = 0;
//int sy = 4;
var mat = new int[L, L];
var tail = new Point[10];
var H = new Point(sx, sy);
var T = new Point(sx, sy);
for (int i = 0; i < 9; i++)
    tail[i] = new Point(sx, sy);

var vis = new bool[L, L];
vis[sx,sy] = true;
foreach (var line in lines)
{
    var x = line.Split(" ");
    int.TryParse(x[1], out int pasi);

    for (int i = 0; i < pasi; i++)
    {
        switch (x[0])
        {
            case "R":
                MoveHRightT(mat, H, tail, vis);
                break;
            case "U":
                MoveHUpT(mat, H, tail, vis);
                break;
            case "L":
                MoveHLeftT(mat, H, tail, vis);
                break;
            case "D":
                MoveHDOwnT(mat, H, tail, vis);
                break;
        }
    }
}

long sum =0;

for (int i = 0; i < L; i++)
{
    for (int j = 0; j < L; j++)
    {
        if (vis[i, j])
            sum++;
    }
}
Console.WriteLine("sum "+sum);
void MoveHRight(int[,] mat, Point h, Point t, bool[,] vis)
{
    h.x++;
     CheckPOints(h,t,vis);
    vis[t.x, t.y] = true;
}
void MoveHRightT(int[,] mat, Point h, Point[] t, bool[,] vis)
{
    h.x++;
    
    CheckPOints(h, t[0], vis);
    for (int i = 1; i < 9; i++)
    {
        CheckPOints(t[i - 1], t[i],vis);
    }
    vis[t[8].x, t[8].y] = true;
}

void MoveHUp(int[,] mat, Point h, Point t, bool[,] vis)
{
    h.y--;
    CheckPOints(h, t, vis);
    vis[t.x, t.y] = true;

}
void MoveHUpT(int[,] mat, Point h, Point[] t, bool[,] vis)
{
    h.y--;
    CheckPOints(h, t[0], vis);
    for (int i = 1; i < 9; i++)
    {
        CheckPOints(t[i - 1], t[i], vis);
    }
    vis[t[8].x, t[8].y] = true;

}
void MoveHLeft(int[,] mat, Point h, Point t, bool[,] vis)
{
    h.x--;
    CheckPOints(h, t, vis);
    vis[t.x, t.y] = true;

}
void MoveHLeftT(int[,] mat, Point h, Point[] t, bool[,] vis)
{
    h.x--;
    CheckPOints(h, t[0], vis);
    for (int i = 1; i < 9; i++)
    {
        CheckPOints(t[i - 1], t[i], vis);
    }
    vis[t[8].x, t[8].y] = true;

}
void MoveHDOwn(int[,] mat, Point h, Point t, bool[,] vis)
{
    h.y++;
    CheckPOints(h, t, vis);

    vis[t.x, t.y] = true;
}
void MoveHDOwnT(int[,] mat, Point h, Point[] t, bool[,] vis)
{
    h.y++;
    CheckPOints(h, t[0], vis);
    for (int i = 1; i < 9; i++)
    {
        CheckPOints(t[i - 1], t[i], vis);
    }
    vis[t[8].x, t[8].y] = true;
}
void MoveCloser(Point h, Point t, bool[,] vis)
{
    if(Math.Abs(h.y-t.y)>1)
        if (h.y - t.y > 1)
        {
            if (h.x - t.x > 0)
            {
                t.x++;
                t.y++;
            }
            else
            {
                t.x--;
                t.y++;
            }
        }
        else
        {
            if (h.x - t.x > 0)
            {
                t.x++;
                t.y--;
            }
            else
            {
                t.x--;
                t.y--;
            }
        }
    if (Math.Abs(h.x - t.x) > 1)
        if (h.x - t.x > 1)
        {
            if (h.y - t.y > 0)
            {
                t.y++;
                t.x++;
            }
            else
            {
                t.y--;
                t.x++;
            }
        }
        else
        {
            if (h.y - t.y > 0)
            {
                t.y++;
                t.x--;
            }
            else
            {
                t.y--;
                t.x--;
            }
        }
}

void CheckPOints(Point h, Point t, bool[,] vis)
{

    if (distHoriz(h, t) == 0 && distVert(h, t) == 0)
        ;
    else if (distHoriz(h, t) == 1 && distVert(h, t) == 1)
        ;
    else if (distHoriz(h, t) == 0 && distVert(h, t) == 1)
        ;
    else if (distHoriz(h, t) == 1 && distVert(h, t) == 1)
        ;
    else if (distHoriz(h, t) == 1 && distVert(h, t) == 0)
        ;
    else if (distHoriz(h, t) == 2 && distVert(h, t) == 0)
    {
        if(h.x-t.x==2)
            t.x++;
        else
        {
            t.x--;
        }
    }
    else if (distVert(h,t)==2 && distHoriz(h,t)==0)
    {
        if (h.y - t.y == 2)
            t.y++;
        else
        {
            t.y--;
        }
    }
    else
    {
        MoveCloser(h, t, vis);
    }
}

int distHoriz(Point h, Point t)
{
    return Math.Abs(h.x - t.x);
}
int distVert(Point h, Point t)
{
    return Math.Abs(h.y - t.y);
}
Console.WriteLine("result "+rez);
class Point
{
    public int x { get; set; }
    public int y { get; set; }

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
