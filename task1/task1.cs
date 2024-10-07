using System.Text;

namespace task1
{
    internal class task1
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: project <n> <m>");
                return;
            }
            int n, m;
            if (!int.TryParse(args[0], out n) || n < 2)
            {
                Console.Write("n should be a natural number with a value greater than 1.");
            }
            if (!int.TryParse(args[1], out m) || m < 1)
            {
                Console.Write("m should be a natural number with a value greater than 0\nEnter m: ");
            }
            StringBuilder result = new StringBuilder();
            int cur = 1;
            do
            {
                result.Append(cur == 0 ? n.ToString() : cur.ToString());
                cur = (cur + m - 1) % n;
            } while (cur != 1);
            Console.Write("The result is: " + result.ToString());
        }
    }
}