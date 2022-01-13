using System;

namespace Integral
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Интеграл: 18*x/sqrt(x^2+9)");
            Console.Write("Введите левую границу интегрирования: ");
            double _left = double.Parse(Console.ReadLine());
            Console.Write("Введите правую границу интегрирования: ");
            double _right = double.Parse(Console.ReadLine());;

            var integral = new Integral(
                function: x => 18 * x / Math.Sqrt(Math.Pow(x, 2)+9),
                left: _left,
                right: _right,
                e: 0.001,
                n: 2
            );
            integral.CalculationNoParall();

        }
        
    }
}