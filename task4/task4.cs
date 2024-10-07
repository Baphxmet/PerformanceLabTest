namespace task4
{
    internal class task4
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: project <input_file>");
                return;
            }

            string filePath = args[0];

            try
            {
                // Чтение чисел из файла
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length == 0)
                {
                    throw new InvalidDataException("The file is empty.");
                }

                int[] nums = lines.Select(line =>
                {
                    if (!int.TryParse(line, out int num))
                    {
                        throw new InvalidDataException($"Invalid data in the file: {line} is not a valid number.");
                    }
                    return num;
                }).ToArray();

                // Нахождение медианы
                Array.Sort(nums);
                int median = nums[nums.Length / 2]; // В случае четного количества элементов медиана берется как средний элемент

                // Подсчет минимального количества ходов
                int moves = nums.Sum(num => Math.Abs(num - median));

                // Вывод результата
                Console.WriteLine(moves);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File {filePath} not found.");
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}