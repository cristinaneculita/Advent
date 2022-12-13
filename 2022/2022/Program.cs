// See https://aka.ms/new-console-template for more information


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Advent2022
{



    class Program
    {
        class Str : IComparable<Str>
        {
            public string It { get; set; }

            public int CompareTo(Str? other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                var c = CompareNou(this.It, other.It);
                if (!c.HasValue)
                    return 0;
                if (c.Value)
                    return -1;
                return 1;
            }

            public Str(string it)
            {
                It = it;
            }
        }

        static void Main(string[] args)
        {
            double rez = 0;

            string[] lines = System.IO.File.ReadAllLines("input1.txt");
            var listS = new List<Str>();
            int i = 1;
            for (int j = 0; j < lines.Length; j += 3)
            {
                var s1 = lines[j];
                var s2 = lines[j + 1];
                listS.Add(new Str(s1));
                listS.Add(new Str(s2));

                //if (CompareNou(lines[j], lines[j + 1]).Value)
                //    rez += i;
                // i++;
            }

            var s11 = new Str("[[2]]");
            var s12 = new Str("[[6]]");
            listS.Add(s11);
            listS.Add(s12);
            var ind1 = 0;
            var ind2 = 0;
            //listS.Sort();

            foreach (var s in listS)
            {
                if (s.CompareTo(s11) < 0)
                    ind1++;
            }

            foreach (var s in listS)
            {
                if (s.CompareTo(s12) < 0)
                    ind2++;
            }

            rez = (ind1 + 1) * (ind2 + 1);

            Console.WriteLine("sum " + rez);
        }

        static bool? CompareNou(string s1, string s2)
        {
            if (s1 == "" && s2 != "")
                return true;
            if (s1 != "" && s2 == "")
                return false;
            if (isInteger(s1) && isInteger(s2))
            {
                var n1 = GetInteger(s1);
                var n2 = GetInteger(s2);
                if (n1 < n2)
                    return true;
                if (n1 > n2)
                    return false;
                if (n1 == n2)
                    return null;
            }

            if (isListNou(s1) && isListNou(s2))
            {
                var index1 = 1;
                var index2 = 1;
                while (true)
                {
                    string e1 = takeNextElNou(s1, index1);
                    string e2 = takeNextElNou(s2, index2);
                    var x = CompareNou(e1, e2);
                    if (x.HasValue && x.Value)
                        return true;
                    if (x.HasValue && !x.Value)
                        return false;

                    index1 += e1.Length;
                    index2 += e2.Length;

                    if (index1 == s1.Length - 1 && index2 == s2.Length - 1)
                        return null;
                    if (index1 == s1.Length - 1)
                        return true;
                    if (index2 == s2.Length - 1)
                        return false;

                }
            }
            else
            {
                if (isListNou(s1) && isInteger(s2))
                {
                    var c2 = ConvertToList(s2);
                    return CompareNou(s1, c2);
                }

                if (isListNou(s2) && isInteger(s1))
                {
                    var c1 = ConvertToList(s1);
                    return CompareNou(c1, s2);
                }
            }

            return null;
        }

        private static string takeNextElNou(string s1, int index1)
        {

            var pd = 0;
            if (s1[index1] == ',')
                index1++;
            var i = index1;
            if (s1[index1] == '[')
            {
                while (true)
                {
                    if (s1[i] == '[')
                        pd++;
                    if (s1[i] == ']')
                    {
                        if (pd == 1)
                            break;
                        pd--;
                    }

                    i++;
                    if (i == s1.Length)
                        break;
                }

                i++;
                var ind2 = index1;
                return new string(s1[ind2..i]);
            }

            if (isInteger(s1[index1].ToString()))
            {
                while (s1[i] != ',' && s1[i] != ']' && i < s1.Length - 1)
                    i++;
                return s1[index1..i];
            }

            return "";
        }

        private static string ConvertToList(string s2)
        {
            return "[" + s2 + "]";
        }

        private static bool isListNou(string s1)
        {
            if (s1.Length == 0)
                return false;
            return (s1[0] == '[');
        }

        private static int GetInteger(string s1)
        {
            s1 = s1.TrimEnd(',');
            int.TryParse(s1, out int n);
            return n;
        }

        private static bool isInteger(string s1)
        {
            if (s1.Length == 0)
                return false;
            return (s1[0] >= '0' && s1[0] <= '9');
        }

    }

}