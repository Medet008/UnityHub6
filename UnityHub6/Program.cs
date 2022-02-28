using System;
using System.IO;

namespace UnityHub6
{
    //1. Изменить программу вывода таблицы функции так, чтобы можно было передавать 
    //    функции типа double (double, double). Продемонстрировать работу на функции 
    //    с функцией a* x^2 и функцией a* sin(x).

    public delegate double Fun(double x, double a);
   
    class MyClass
    {
        public static void TableFunc(Fun F, double x, double b)
        {
            Console.WriteLine("--x-----------y--");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", x, F(x, b));
                x++;    
            }
            Console.WriteLine("------------------");
        }
        

        public static double MyFuncSin(double x, double a)
        {
            return a * Math.Sin(x);
        }

        public static double MyFuncCos(double x, double a)
        {
            return (a * Math.Cos(x));   
        }

        public static double MyFuncPow(double x, double a) 
        {    return a * Math.Pow(x, 2); }



        public static void Task()
        {
            Console.WriteLine("MyFunctSin");
            TableFunc(MyFuncSin, -2, 2);
            Console.WriteLine("MyFunctCos");
            TableFunc(MyFuncCos, -2, 2);
            Console.WriteLine("MyFunctPow");
            TableFunc(MyFuncPow, 2, 2);
        }

    }



    class MyClass2
    {
        public delegate double function(double x);
        public static double F1(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double F2(double x)
        {
            return x * x - 10 * x + 50;
        }
        public static void SaveFunc(string fileName, double a, double b, double h, function F)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h; 
            }
            bw.Close();
            fs.Close();
        }
        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }

        public static void Task2()
        {
            function[] F = { F1, F2 };
            Console.WriteLine("Сделайте выбор: 1 - функция F1, 2 - функция F2");
            int index = int.Parse(Console.ReadLine());
            SaveFunc("data.bin", -100, 100, 0.5, F[index - 1]);
            Console.WriteLine(Load("data.bin"));
            Console.ReadKey();
        }
    }



    class Program
        {
            static void Main(string[] args)
            {
              MyClass.Task();
              MyClass2.Task2();
         }
        }
    
}
