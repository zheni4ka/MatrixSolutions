using System.Diagnostics;

namespace KramerMethod
{
    internal class Program
    {
        public static void Main()
        {
            double[,] matrixA = 
            { 
                { 2, 3 }, 
                { 5, -1 } 
            };

            double[] matrixB = { 8, 3 };


            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine("Початкове використання пам'яті: " + currentProcess.WorkingSet64 / 1024 + " КБ");

            var result = SolveKramer(matrixA, matrixB);

            currentProcess.Refresh();

            if (result != null)
            {
                Console.WriteLine($"x = {result[0]}, y = {result[1]}");
                Console.WriteLine("Використання пам'яті після обчислень: " + (currentProcess.WorkingSet64) / 1024 + " КБ");
            }
            else
            {
                Console.WriteLine("This system has an infinite number of solutions or nothing");
            }
        }

        public static double[] SolveKramer(double[,] matrixA, double[] matrixB)
        {
            double D = Determinant2x2(matrixA);

            if (D == 0)
            {
                return null;
            }

            double[,] matrixDx = 
            { 
                { matrixB[0], matrixA[0, 1] }, 
                { matrixB[1], matrixA[1, 1] } 
            };
            double[,] matrixDy =
            { 
                { matrixA[0, 0], matrixB[0] },
                { matrixA[1, 0], matrixB[1] } 
            };

            double Dx = Determinant2x2(matrixDx);
            double Dy = Determinant2x2(matrixDy);

            double x = Dx / D;
            double y = Dy / D;

            return new double[] { x, y };
        }

        public static double Determinant2x2(double[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
    }
}
