using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading;

namespace Integral
{
    public class Integral
    {
        private readonly Func<double, double> _func;
        private readonly double _left;
        private readonly double _right;
        private readonly double _e; //погрешность
        private readonly int _n; //количество отрезков

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
        public double NoParallel()
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


        public void CalculateIntegral(object index)
        {
            var i = (double[])index;
            
            var left = i[0];
            var right = i[1];
            
            double h = (right - left) / _n;
           
            var center = left + h;
            if (right-left <= _e) _result += _func(center)*(right-left);
            else
            {
                CalculateIntegral(new double[] {left, center});
                CalculateIntegral(new double[] {center, right});
            }
        }

        public double Parallel()
        {
            //List<Thread> threads = new List<Thread>();
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int count = Environment.ProcessorCount;
            
            Thread[] threads = new Thread[count];
            double[] results = new double[count];
            
            var weight = _right - _left;
            var distanceThreads = weight / count;
            
            for (int i = 0; i < threads.Length; i++)
            {
                var left = _left + i * distanceThreads;
                var right = _left + (i + 1) * distanceThreads;
                threads[i] = new Thread(CalculateIntegral);
                threads[i].Start(new double[] {left,right});
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            
            watch.Stop();

            Console.WriteLine("Время вычисления в многопоточном режиме: " + watch.ElapsedMilliseconds);
            return _result;
        }
    }
}