using System;

namespace Integral
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Интеграл: 2/sqrt(x^3)");
            Console.Write("Введите левую границу интегрирования: ");
            double _left = double.Parse(Console.ReadLine());
            Console.Write("Введите правую границу интегрирования: ");
            double _right = double.Parse(Console.ReadLine());;

            var integral = new Integral(
                function: x => 2 / Math.Sqrt(Math.Pow(x, 3)),
                left: _left,
                right: _right,
                e: 0.001,
                n: 2
            );
            integral.NoParallel();
            var result = integral.Parallel();
            Console.WriteLine($"Результат: {Math.Round(result, 7)}.");

        }
        
    }
}