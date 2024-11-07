using System.Diagnostics;

namespace LaplasMethod
{
    internal class Program
    {
        static int CalculateDeterminant(int[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new ArgumentException("Matrix must be 3x3 size");
            }

            int a11 = matrix[0, 0], a12 = matrix[0, 1], a13 = matrix[0, 2];

            int M11 = matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1];
            int M12 = matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0];
            int M13 = matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0];

            int determinant = a11 * M11 - a12 * M12 + a13 * M13;

            return determinant;
        }

        static void Main()
        {
            int[,] matrix = {
            { 2, 3, 1 },
            { 4, 5, 6 },
            { 7, 8, 9 }
            };

            Process currentProcess = Process.GetCurrentProcess();
            Console.WriteLine("Початкове використання пам'яті: " + currentProcess.WorkingSet64 / 1024 + " КБ");
            var s = currentProcess.WorkingSet64;
            try
            {

                int determinant = CalculateDeterminant(matrix);
                currentProcess.Refresh();
                Console.WriteLine("Matrix determinant: " + determinant);


                Console.WriteLine("Використання пам'яті після обчислень: " + (currentProcess.WorkingSet64) / 1024 + " КБ");

            }
            catch (Exception ex) { }
        }

    }
}
