using System;
using System.Numerics;
using System.Diagnostics;
using System.Threading;

namespace MatrixSolutions
{
    internal class Program
    {
        public static double[,] InverseMatrix(double[,] matrix)
        {
            double det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            if (det == 0) 
                throw new InvalidOperationException("Matrix is singular");

            double[,] inverse = new double[2, 2];
            inverse[0, 0] = matrix[1, 1] / det;
            inverse[0, 1] = -matrix[0, 1] / det;
            inverse[1, 0] = -matrix[1, 0] / det;
            inverse[1, 1] = matrix[0, 0] / det;

            return inverse;
        }

        public static double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
        {
            double[] result = new double[2];

            result[0] = matrix[0, 0] * vector[0] + matrix[0, 1] * vector[1];
            result[1] = matrix[1, 0] * vector[0] + matrix[1, 1] * vector[1];

            return result;
        }

        public static double[] Solve(double[,] A, double[] b)
        {
            double[,] A_inv = InverseMatrix(A); 
            return MultiplyMatrixVector(A_inv, b); 
        }

        public static void Main()
        {

            double[,] A = { { 2, 2 }, { 4, 1 } };
            double[] b = { 5, 6 };

            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine("Початкове використання пам'яті: " + currentProcess.WorkingSet64 / 1024 + " КБ");
            var s = currentProcess.WorkingSet64;

            try
            {

                double[] solution = Solve(A, b);

                currentProcess.Refresh();
                Console.WriteLine($"Solution: x = {solution[0]}, y = {solution[1]}");


                Console.WriteLine("Використання пам'яті після обчислень: " +(currentProcess.WorkingSet64) / 1024 + " КБ");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}