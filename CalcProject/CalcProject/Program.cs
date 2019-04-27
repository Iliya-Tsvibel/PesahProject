using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcProject calc = new CalcProject();
         
            
                Console.Write("Insert the number:");
                int x = Convert.ToInt16(Console.ReadLine());
                Console.Write("Insert another number:");
                int y = Convert.ToInt16(Console.ReadLine());
            if (x > 0 && y > 0)
            {
                calc.AddNumbers(x, y);
                Console.WriteLine("Here All Options Of Your Calculator:");
                calc.PrintTheCalculator();
            }


            else
            {
                
                Console.WriteLine("The numbers must have positive values");
            }

           
        }
    }
}
