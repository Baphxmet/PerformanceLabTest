using Newtonsoft.Json;

namespace task3
{
    internal class task3
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: project <values.json> <tests.json> <report.json>");
                return;
            }

            string valuesFilePath = args[0];
            string testsFilePath = args[1];
            string reportFilePath = args[2];

            try
            {
                // Считываем и парсим values.json
                var valuesData = JsonConvert.DeserializeObject<ValuesData>(File.ReadAllText(valuesFilePath));
                Dictionary<int, string> valuesMap = new Dictionary<int, string>();
                foreach (var entry in valuesData.values)
                {
                    valuesMap[entry.id] = entry.value;
                }

                // Считываем и парсим tests.json
                var testsData = JsonConvert.DeserializeObject<TestsData>(File.ReadAllText(testsFilePath));

                // Заполняем значения в структуре tests.json
                FillTestValues(testsData.tests, valuesMap);

                // Записываем результат в report.json
                string outputJson = JsonConvert.SerializeObject(testsData, Formatting.Indented);
                File.WriteAllText(reportFilePath, outputJson);

                Console.WriteLine("Report successfully generated at: " + reportFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        static void FillTestValues(List<Test> tests, Dictionary<int, string> valuesMap)
        {
            foreach (var test in tests)
            {
                if (valuesMap.ContainsKey(test.id))
                {
                    test.value = valuesMap[test.id];
                }

                if (test.values != null && test.values.Count > 0)
                {
                    FillTestValues(test.values, valuesMap);
                }
            }
        }
    }

    class ValuesData
    {
        public List<ValueEntry> values { get; set; }
    }

    class ValueEntry
    {
        public int id { get; set; }
        public string value { get; set; }
    }

    class TestsData
    {
        public List<Test> tests { get; set; }
    }

    class Test
    {
        public int id { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public List<Test> values { get; set; }
    }
}