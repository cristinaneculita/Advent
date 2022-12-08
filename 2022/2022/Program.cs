// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

long rez = 0;
int L = 99;
string[] lines = System.IO.File.ReadAllLines("input1.txt");

var mat = new int[L, L];
for (int i = 0; i < L; i++)
{
    for (int j = 0; j < L; j++)
    {
        int.TryParse(lines[i][j].ToString(), out int x);
        mat[i, j] = x;
    }
}

var max = 0;

for (int i = 0; i < L; i++)
{
    for (int j = 0; j < L; j++)
    {
        if (Highest(mat,i,j))
            rez++;
        var x = scenic(mat, i, j);
        if (x > max)
            max = x;
    }
}

int scenic(int[,] mat, int i, int j)
{
    var s = 1;
    //look left
    if (i == 0)
        return 0;
    var tot = 0;
    for (int k = i-1; k >=0; k--)
    {
        tot++;
        if (mat[k,j]>= mat[i, j])
           break;
    }
    s*=tot;

    //look up
    if (j == 0)
        return 0;
    tot = 0;
    for (int k = j-1; k >=0; k--)
    {
        tot++;
        if (mat[i, k] >= mat[i, j])
            break;
    }
    s *= tot;

    //look right
    if (i == L - 1)
        return 0;
    tot = 0;
    for (int k = i + 1; k < L; k++)
    {
        tot++;
        if (mat[k, j] >= mat[i, j])
            break;
    }
    s *= tot;

    //look down
    tot = 0;
    if (j == L - 1)
        return 0;
    for (int k = j + 1; k < L; k++)
    {
        tot++;
        if (mat[i, k] >= mat[i, j])
             break;
    }
    s *= tot;
    return s;
}

bool Highest(int[,] mat, int i, int j)
{
    //look left
    if (i == 0)
        return true;
    var tot = 0;
    for (int k = 0; k < i; k++)
    {
        if (mat[k, j] < mat[i, j])
            tot++;
    }
    if(tot==i) return true;

    //look up
    if(j==0) 
        return true;
    tot = 0;
    for (int k = 0; k < j; k++)
    {
        if (mat[i, k] < mat[i, j])
            tot++;
    }
    if(tot==j) return true;

    //look right
    if (i == L - 1)
        return true;
    tot = 0;
    for (int k = i+1; k < L; k++)
    {
        if (mat[k, j] < mat[i, j])
            tot++;
    }
    if(tot==L-i-1)
        return true;

    //look down
    tot = 0;
    if(j==L-1)
        return true;
    for (int k = j+1; k < L; k++)
    {
        if (mat[i, k] < mat[i, j])
            tot++;
    }
    if(tot == L - j - 1)
        return true;
    return false;
}

Console.WriteLine("result "+max);
