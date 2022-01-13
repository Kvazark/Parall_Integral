using System;
using System.Runtime.Intrinsics.Arm;

namespace Integral
{
    public class Integral
    {
        private readonly Func<double, double> _func;
        private readonly double _left;
        private readonly double _right;
        private readonly double _e; //погрешность
        private readonly double _n; 

        private double _result;

        public Integral(Func<double, double> function, double left, double right, double e, int n)
        {
            _func = function;
            _left = left;
            _right = right;
            _e = e;
            _n = n;
        }

        //Метод средних прямоугольников с точностью е
        public double CalculationNoParall()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            double result = 0;

            for (double i = _left; i < _right; i += _e)
            {
                double h = ((i + _e) - i) / _n;
                double x = i + h;
                result += _func(x) * _e;
            }

            watch.Stop();

            Console.WriteLine("Время вычисления в однопоточном режиме: " + watch.ElapsedMilliseconds);
            Console.WriteLine($"Результат: {Math.Round(result, 7)}.");
            return result;
        }


        public void CalculateIntegral(double left, double right, out double result)
        {
            var h = right - left / _n; //шаг разбиения отрезка
            var center = left + h;
            
            CalculateIntegral(left, center, out var s1);
            CalculateIntegral(center, right, out var s2);

            result = s1 + s2;
        }

        public void Parall()
        {
        }
    }
}