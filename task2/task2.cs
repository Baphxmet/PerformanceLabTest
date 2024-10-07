using System.IO;
using System;

namespace task2
{
    internal class task2
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: project <circle_file> <points_file>");
                return;
            }

            string circleFilePath = args[0];
            string pointsFilePath = args[1];

            try
            {
                // Чтение данных окружности
                double[] circleData = ReadCircleData(circleFilePath);
                double x_c = circleData[0];
                double y_c = circleData[1];
                double r = circleData[2];

                // Чтение точек и проверка каждой точки
                double[][] points = ReadPointsData(pointsFilePath);
                foreach (var point in points)
                {
                    double x_p = point[0];
                    double y_p = point[1];
                    int position = CheckPointPosition(x_c, y_c, r, x_p, y_p);
                    Console.WriteLine(position);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static double[] ReadCircleData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not found.");
            }

            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
            {
                throw new InvalidDataException("Circle file must contain at least 2 lines.");
            }

            string[] center = lines[0].Split(' ');
            if (center.Length != 2)
            {
                throw new InvalidDataException("The first line must contain two coordinates for the circle's center.");
            }

            if (!double.TryParse(center[0], out double x_c) || !double.TryParse(center[1], out double y_c))
            {
                throw new InvalidDataException("The center coordinates must be numbers.");
            }

            if (!double.TryParse(lines[1], out double r))
            {
                throw new InvalidDataException("The radius of the circle must be a number.");
            }

            return new double[] { x_c, y_c, r };
        }

        static double[][] ReadPointsData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not found.");
            }

            string[] lines = File.ReadAllLines(filePath);
            double[][] points = new double[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] pointCoords = lines[i].Split(' ');
                if (pointCoords.Length != 2)
                {
                    throw new InvalidDataException($"Line {i + 1} in the points file must contain two coordinates.");
                }

                if (!double.TryParse(pointCoords[0], out double x_p) || !double.TryParse(pointCoords[1], out double y_p))
                {
                    throw new InvalidDataException($"The coordinates in line {i + 1} must be numbers.");
                }

                points[i] = new double[] { x_p, y_p };
            }
            return points;
        }

        static int CheckPointPosition(double x_c, double y_c, double r, double x_p, double y_p)
        {
            double distance = Math.Sqrt(Math.Pow(x_p - x_c, 2) + Math.Pow(y_p - y_c, 2));

            if (Math.Abs(distance - r) < 1e-9)
                return 0; // На окружности
            else if (distance < r)
                return 1; // Внутри окружности
            else
                return 2; // Вне окружности

        }
    }
}