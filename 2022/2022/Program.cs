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
            rez = (ind1+1)*(ind2+1);

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
                if(n1<n2)
                    return true;
                if(n1>n2)
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
                    if(x.HasValue && x.Value)
                        return true;
                    if(x.HasValue && !x.Value)
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
                while (s1[i] != ',' && s1[i] !=']' && i<s1.Length-1)
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
            s1= s1.TrimEnd(',');
            int.TryParse(s1, out int n);
            return n;
        }

        private static bool isInteger(string s1)
        {
            if(s1.Length == 0)
                return false;
            return (s1[0] >= '0' && s1[0] <= '9');
        }

        private static bool Compare(string v1, string v2)
        {
            string cp1 = "";


            if (v1[0] == '[')
            {
                cp1 = TakeList(v1);
                //v1.TakeWhile(x => x != ']').ToArray();
                // cp1 = new string(cp[1..]);
            }
            else
            {
                cp1 = TakeElement(v1);
                //cp1= new string(cp[1..]);
            }

            string cp2 = "";
            if (v2[0] == '[')
            {

                cp2 = TakeList(v2);
                // cp2 = new string(cp[1..]);
            }
            else
            {
                cp2 = TakeElement(v2);
                //cp2 = new string(cp[1..]);
            }

            if (cp1.Length == 0)
                return true;
            if (cp2.Length == 0)
                return false;
            if (cp1[0] == '[' && cp2[0] == '[')
            {

               // if (isListArray(v1))
              //  {
                    int ind1 = 0;
                    int ind2 = 0;
                    while (true)
                    {
                        var x1 = takeNextEl(v1, ind1);
                        var x2 = takeNextEl(v2, ind2);
                        if (!Compare(x1, x2))
                            return false;

                        ind1 += x1.Length + 1;
                        ind2 += x2.Length + 1;
                        if (v1.Length <= ind1 + 1 && v2.Length > ind2 + 1)
                            return true;
                        if (v1.Length > ind1 + 1 && v2.Length <= ind2 + 1)
                            return false;
                        if (v1.Length == ind1 + 1 && v2.Length == ind2 + 1)
                            break;
                    }


               // }
               // else
               // {
                    
               // }
            }
            else
            {
                if (cp1[0] == '[')
                {
                    cp1 = TakeList(cp1);
                    return CompareLists(cp1, cp2);
                }
                if (cp2[0] == '[')
                {
                    cp2 = TakeList(cp2);
                    return CompareLists(cp1, cp2);
                }



            }




            return CompareLists(cp1, cp2);





        }

        private static string TakeElement(string v1)
        {
            int i = 0;

            while (v1[i] <= '9' && v1[i] >= '0' && i < v1.Length - 1)
                i++;

            i++;
            return new string(v1[0..i]);

        }

        private static string takeNextEl(string v1, int ind)
        {
            int pd = 0;
            var cv = v1.ToCharArray();
            int i = ind + 1;
            if (cv[ind + 1] != '[')
            {
                while (cv[i] <= '9' && cv[i] >= '0')
                    i++;

                var ind1 = ind + 1;
                return new string(cv[ind1..i]);
            }


            while (true)
            {
                if (cv[i] == '[')
                    pd++;
                if (cv[i] == ']')
                {
                    if (pd == 1)
                        break;
                    pd--;
                }

                i++;
                if (i == v1.Length)
                    break;
            }

            i++;
            var ind2 = ind + 1;
            return new string(cv[ind2..i]);
        }



        private static bool isListArray(string v1)
        {
            int pd = 0;
            var cv = v1.ToCharArray();
            int i = 1;
            while (true)
            {
                if (cv[i] == '[')
                    pd++;
                if (cv[i] == ']')
                {
                    if (pd == 0)
                        break;
                    pd--;
                }

                if (cv[i] == ',' && pd == 0)
                    return true;

                i++;
                if (i == v1.Length)
                    break;
            }

            return false;
        }

        private static string TakeList(string v1)
        {
            int pd = 0;
            var cv = v1.ToCharArray();
            int i = 1;
            while (true)
            {
                if (cv[i] == '[')
                    pd++;
                if (cv[i] == ']')
                {
                    if (pd == 0)
                        break;
                    pd--;
                }

                i++;
                if (i == v1.Length)
                    break;
            }

            return new string(cv[1..i]);


        }

        private static bool CompareLists(string cp1, string cp2)
        {
            var x1 = cp1.Split(",");
            var x2 = cp2.Split(",");
            var i = 0;
            //var j = 0;
            while (i < x1.Length && i < x2.Length)
            {
                var x = int.TryParse(x1[i], out int n1); 
                var y = int.TryParse(x2[i], out int n2);
                if (x && y)
                {
                    if (n1 > n2)
                        return false;
                    if (n1 < n2)
                        return true;
                    i++;
                }
                //else
                //{
                //    if (x && !y)
                //    {
                //        MakeList()
                //    }
                //}
            }

            if (i == x2.Length && i != x1.Length)
                return false;
            return true;
        }
    }
}