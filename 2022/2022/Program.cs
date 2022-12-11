// See https://aka.ms/new-console-template for more information


using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Advent2022
{
    class Monkey
    {
        public List<double> Articles { get; set; }
        public  operation op { get; set; }
        public string OP2 { get; set; }
        public int Divisible { get; set; }
        public int IfYes { get; set; }
        public int IfNo { get; set; }
        public double Processedtime { get; set; }

        public Monkey(List<double> articles, operation op, string op2, int divisible, int ifYes, int ifNo)
        {
            Articles = articles;
            this.op = op;
            OP2 = op2;
            Divisible = divisible;
            IfYes = ifYes;
            IfNo = ifNo;
        }

        public Monkey()
        {
            Articles = new List<double>();
        }
    }


    public enum operation
    {
        mul,
        add
    }

    class Program
    {

        static void Main(string[] args)
        {
            double rez = 0;
            string[] lines = System.IO.File.ReadAllLines("input1.txt");
            var mon = new List<Monkey>();

            var m1 = new Monkey(new List<double>() { 76, 88, 96, 97, 58, 61, 67 }, operation.mul, "19", 3, 2, 3);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 93, 71, 79, 83, 69, 70, 94, 98 }, operation.add, "8", 11, 5, 6);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 50, 74, 67, 92, 61, 76 }, operation.mul, "13", 19, 3, 1);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 76, 92 }, operation.add, "6", 5, 1, 6);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 74, 94, 55, 87, 62 }, operation.add, "5", 2, 2, 0);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 59, 62, 53, 62 }, operation.mul, "old", 7, 4, 7);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 62 }, operation.add, "2", 17, 5, 7);
            mon.Add(m1);
            m1 = new Monkey(new List<double>() { 85, 54, 53 }, operation.add, "3", 13, 4, 0);
            mon.Add(m1);


            //var m1 = new Monkey(new List<double>() { 79, 98 }, operation.mul, "19", 23, 2, 3);
            //mon.Add(m1);
            //m1 = new Monkey(new List<double>() { 54, 65, 75, 74 }, operation.add, "6", 19, 2, 0);
            //mon.Add(m1);
            //m1 = new Monkey(new List<double>() { 79, 60, 97 }, operation.mul, "old", 13, 1, 3);
            //mon.Add(m1);
            //m1 = new Monkey(new List<double>() { 74 }, operation.add, "3", 17, 0, 1);
            //mon.Add(m1);
            double worryl = 1;

            
            for (int i = 0; i < 10000; i++)
            {

                foreach (var monkey in mon)
                {
                    foreach (var monkeyArticle in monkey.Articles)
                    {
                        switch (monkey.op)
                        {
                            case operation.add:
                                int.TryParse(monkey.OP2, out int op2);
                                worryl = monkeyArticle + op2;
                                break;
                            case operation.mul:
                                var t=int.TryParse(monkey.OP2, out int op);
                                if (t) 
                                    worryl = monkeyArticle * op;
                                else 
                                    worryl = monkeyArticle * monkeyArticle;
                                break;

                        }

                        worryl = worryl % 9699690;
                        if (worryl % monkey.Divisible == 0)
                        {
                            mon[monkey.IfYes].Articles.Add(worryl);
                        }
                        else
                        {
                            mon[monkey.IfNo].Articles.Add(worryl);
                        }

                        monkey.Processedtime++;
                    }

                    monkey.Articles = new List<double>();
                }
            }

            mon = mon.OrderByDescending(x => x.Processedtime).ToList();
            double d = mon[0].Processedtime * mon[1].Processedtime;
            Console.WriteLine("sum " + d);
        }
    }
}