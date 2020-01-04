using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication2
{
   
    class Program
    {
        static byte[] sum(string num1, string num2)
        {

            if (num2.Count() > num1.Count()) { return sum(num2, num1); }

            List<byte> n1 = new List<byte>{};
            List<byte> n2 = new List<byte>{};

                                  
            int i = 0;
            for (i = 0; i <= num1.Count()-1; i++) { n1.Add(Convert.ToByte(num1[i].ToString())); }
            for (i = 0; i <= num2.Count() - 1; i++) { n2.Add(Convert.ToByte(num2[i].ToString())); }

            n1.Reverse(); n2.Reverse();
            //int nc = num1.Count();
            List<byte> n = new List<byte>{};
            for (i = 0; i <= num2.Count()-1; i++) 
            {
                try { n[i] = Convert.ToByte(n1[i] + n2[i] + n[i]); } catch { n.Add(Convert.ToByte(n1[i] + n2[i])); } finally {}
                if (n[i] > 9) { n.Add(1); n[i] = Convert.ToByte(n[i] - 10); }
            }

            n.Reverse();
            return n.ToArray();

        }
       
        static void CorrectNumbersInCells(List<byte> list, int StartingPosition=0)
        {
            int sp = StartingPosition; int i; int size = list.Count(); Boolean repeat = false; 
            if (sp>size) {return;}
            for (i = 0; i <= size - sp - 1; i++) 
            {
                if (list[i + sp] > 9) { 
                    repeat = true; byte a = Convert.ToByte(list[i + sp] / 10); byte b = Convert.ToByte(list[i + sp] % 10);
                    try { list[i + sp + 1] = Convert.ToByte(list[i + sp + 1] + a); list[i + sp] = b; }
                    catch { list.Add(a); list[i + sp] = b; }
                }
            } 
            if (repeat) {CorrectNumbersInCells(list, sp+1);}
        }

        static void AddTo(List<byte> num1, List<byte> num2, int StartingPosition)
        {

            int sp = StartingPosition;
            //string strnum = Convert.ToString(num2);
            UInt16 size = Convert.ToUInt16(num2.Count());

            int i;
            for (i = 0; i <= size-1; i++) 
            {
                try { num1[sp + i] = Convert.ToByte(num1[sp + i] + num2[i]); }
                catch {num1.Add(num2[i]);}
            }
            CorrectNumbersInCells(num1, sp);
        }

        static List<byte> ProductWithOneDigit(List<byte> list, byte number)
        {
            int i; int size = list.Count(); List<byte> result = new List<byte>(list);
            for (i = 0; i <= size - 1; i++) { result[i] = Convert.ToByte(result[i] * number); }
            CorrectNumbersInCells(result);
            return result;
        }

        static List<byte> multiplication(List<byte> n1, List<byte>n2)
        {
            if (n2.Count() > n1.Count()) { return multiplication(n2, n1); }

            int i;
            
            List<byte> n = new List<byte> { };
            
            for (i = 0; i <= n2.Count()-1; i++) 
            {
                AddTo(n, ProductWithOneDigit(n1, n2[i]), i);
            }
            return n;
        }

        static List<byte> ToPower(List<byte> n, UInt16 power)
        {
            if (power == 1) { return n; }
            else { return multiplication(n, ToPower(n, Convert.ToUInt16(power - 1))); }

        }
        static void Main(string[] args)
        {
            string num1; UInt16 num2;
            if (args.Count() < 2)
            { Console.WriteLine("Enter number and power to raise to: ");
                num1 = Console.ReadLine(); num2 = Convert.ToUInt16(Console.ReadLine()); }
            else
            { num1 = args[0]; num2 = Convert.ToUInt16(args[1]); Console.WriteLine(num1); Console.WriteLine(num2); }

            System.Diagnostics.Stopwatch p = new System.Diagnostics.Stopwatch();

            Console.WriteLine("Length of the number given is " + Convert.ToString(num1.Count()));

            string str = ""; int i;

            List<byte> n1 = new List<byte> { };
            //List<byte> n2 = new List<byte> { };
            
            for (i = 0; i <= num1.Count() - 1; i++) { n1.Add(Convert.ToByte(num1[i].ToString())); }
            //for (i = 0; i <= num2.Count() - 1; i++) { n2.Add(Convert.ToByte(num2[i].ToString())); }

            n1.Reverse(); //n2.Reverse();


           // List<byte> answer = multiplication(n1, n2).ToList();
            p.Start();

            List<byte> answer = ToPower(n1, num2);

            answer.Reverse();

            p.Stop();

            Console.WriteLine("Calculation took " + Convert.ToString(p.ElapsedMilliseconds) + "ms.");
            

            for (i = 0; i <= answer.Count()-1; i++) { str = str + Convert.ToString(answer[i]); }

            Console.WriteLine("Length of the resultant number is " + Convert.ToString(str.Count()));
            Console.WriteLine("The resultant number is: " + str);
            Console.ReadKey();

                
            
            
        }
        
    }
}
