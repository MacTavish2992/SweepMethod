using System;


public class Program
{
    public static void Main()
    {


        //double[,] Matrix = new double[5, 5] {{ 8, -2, 0, 0, 0 }, //для проверки, результаты должны быть такими -0.80392, 0.28433, 1.25815, -1.47783, -0.29864
        //                                    { -1, 5, 3, 0, 0 },
        //                                    { 0, 7, -5, -9, 0 },
        //                                    { 0, 0, 4, 7, 9 },
        //                                    { 0, 0, 0, -5, 8 }};
        //double[] arrayFreeOdds = new double[5] { -7, 6, 9, -8, 5 };

        double[,] Matrix = new double[5, 5];
        double[] arrayFreeOdds = new double[5];
        MainDiagonal(Matrix);
        AboveDiagonal(Matrix);
        BelowDiagonal(Matrix);
        RestElements(Matrix);
        FreeOdds(arrayFreeOdds);
        ArrPrint(Matrix, arrayFreeOdds);
        PrintResult(Matrix, arrayFreeOdds);
        ChekResult(Matrix, arrayFreeOdds);
        Console.ReadLine();

    }
    public static void MainDiagonal(double[,] Matrix) // ввод главной диагонали 
    {
        Console.WriteLine("Введите элементы главной диагонали матрицы:");
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"Matrix[{i},{i}] = ");
            if ((!double.TryParse(Console.ReadLine(), out Matrix[i, i]) || Matrix[i, i] == 0))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                i--;
            }
        }

        //Random random = new Random();
        //for (int i = 0; i < 5; i++)
        //{
        //    Matrix[i, i] = random.Next(1, 10);
        //}
    }
    public static void AboveDiagonal(double[,] Matrix) // вводы выше главной диагонали
    {
        Console.WriteLine("Введите элементы, выше главной диагонали матрицы:");
        for (int i = 0; i < 4; i++)
        {
            Console.Write($"Matrix[{i},{i + 1}] = ");
            if ((!double.TryParse(Console.ReadLine(), out Matrix[i, i + 1]) || Matrix[i, i + 1] == 0))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                i--;
            }
        }
        //Random random = new Random();
        //for (int i = 0; i < 4; i++)
        //{
        //    Matrix[i, i + 1] = random.Next(1, 10);
        //}
    }
    public static void BelowDiagonal(double[,] Matrix) // вводы ниже главной диагонали
    {
        Console.WriteLine("Введите элементы, ниже главной диагонали матрицы:");
        for (int i = 1; i < 5; i++)
        {
            Console.Write($"Matrix[{i},{i - 1}] = ");
            if ((!double.TryParse(Console.ReadLine(), out Matrix[i, i - 1]) || Matrix[i, i - 1] == 0))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                i--;
            }
        }
        //Random random = new Random();
        //for (int i = 1; i < 5; i++)
        //{
        //    Matrix[i, i - 1] = random.Next(1, 10);
        //}
    }
    public static void RestElements(double[,] Matrix) // заполнение остальных элементов нулями
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Matrix[i, j] == 0)
                {
                    Matrix[i, j] = 0;
                }
            }
        }
    }
    public static void FreeOdds(double[] arrayFreeOdds) // ввод свободных коэффициентов
    {
        Console.WriteLine("Введите свободные коэффициенты:");
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"arrayFreeOdds[{i + 1}] = ");
            if ((!double.TryParse(Console.ReadLine(), out arrayFreeOdds[i])) || arrayFreeOdds[i] == 0)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                i--;
            }
        }
        //Random random = new Random();
        //for (int i = 0; i < 5; i++)
        //{
        //    arrayFreeOdds[i] = random.Next(1, 10);
        //}
    }
    public static void ArrPrint(double[,] Matrix, double[] arrayFreeOdds) // вывод матрицы со свободными коэффициентами
    {

        Console.WriteLine("Изначальный массив со свободными коэффициентами:");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(Matrix[i, j] + " ");
            }
            Console.Write("|");
            Console.Write(" " + arrayFreeOdds[i]);
            Console.WriteLine();
        }
    }

    public static (double[], double[], int) SolutionMatrixStaightStroke(double[,] Matrix, double[] arrayFreeOdds) // метод прямого хода
    {
        int FreeOddsLength = arrayFreeOdds.Length;
        double[] alpha = new double[FreeOddsLength - 1];
        double[] beta = new double[FreeOddsLength];
        alpha[0] = Matrix[0, 1] / Matrix[0, 0];
        beta[0] = arrayFreeOdds[0] / Matrix[0, 0];
        for (int i = 1; i < FreeOddsLength - 1; i++)
        {
            double m = 1.0 / (Matrix[i, i] - Matrix[i, i - 1] * alpha[i - 1]);
            alpha[i] = Matrix[i, i + 1] * m;
            beta[i] = (arrayFreeOdds[i] - Matrix[i, i - 1] * beta[i - 1]) * m;
        }
        beta[FreeOddsLength - 1] = (arrayFreeOdds[FreeOddsLength - 1] - Matrix[FreeOddsLength - 1, FreeOddsLength - 2] * beta[FreeOddsLength - 2]) / (Matrix[FreeOddsLength - 1, FreeOddsLength - 1] - Matrix[FreeOddsLength - 1, FreeOddsLength - 2] * alpha[FreeOddsLength - 2]);
        return (alpha, beta, FreeOddsLength);


    }
    public static double[] SolutionMatrixReverseStroke(double[,] Matrix, double[] arrayFreeOdds) // метод обратного хода
    {
        (double[] alpha, double[] beta, int FreeOddsLength) = SolutionMatrixStaightStroke(Matrix, arrayFreeOdds);
        double[] resultOdds = new double[FreeOddsLength];
        resultOdds[FreeOddsLength - 1] = beta[FreeOddsLength - 1];
        for (int i = FreeOddsLength - 2; i >= 0; i--)
        {
            resultOdds[i] = beta[i] - alpha[i] * resultOdds[i + 1];
        }
        return resultOdds;
    }


    public static void PrintResult(double[,] Matrix, double[] arrayFreeOdds) // вывод результата 
    {
        Console.WriteLine();
        double[] solution = SolutionMatrixReverseStroke(Matrix, arrayFreeOdds);
        Console.WriteLine("Ответ:");
        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine("x" + (i + 1) + " = " + solution[i]);
        }

    }
    public static void ChekResult(double[,] Matrix, double[] arrayFreeOdds) // проврека на верность решения
    {
        double[] resultOdds = SolutionMatrixReverseStroke(Matrix, arrayFreeOdds);
        for (int i = 0; i < arrayFreeOdds.Length; i++)
        {
            double sum = 0;
            for (int j = 0; j < resultOdds.Length; j++)
            {
                sum += Matrix[i, j] * resultOdds[j];
            }
            if (Math.Abs(sum - arrayFreeOdds[i]) > 0.00001)
            {
                Console.WriteLine("Ошибка: ответ не верен.");
                return;
            }
        }
        Console.WriteLine("Ответ верен.");
    }





}
