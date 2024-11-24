using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "INPUT.TXT";
        string outputFile = "OUTPUT.TXT";

        try
        {
            var lines = File.ReadAllLines(inputFile);
            var firstLine = lines[0].Split();
            int n = int.Parse(firstLine[0]);
            int r = int.Parse(firstLine[1]);

            var clothes = new List<(int wi, int di, int index)>();

            for (int i = 1; i <= n; i++)
            {
                var data = lines[i].Split();
                int wi = int.Parse(data[0]);
                int di = int.Parse(data[1]);
                clothes.Add((wi, di, i));
            }

            clothes = clothes.OrderBy(c => c.di).ToList();
            var plan = new List<(int time, int index)>();
            int currentTime = 0;

            foreach (var item in clothes)
            {
                int wi = item.wi;
                int di = item.di;
                int index = item.index;

                if (wi <= di) continue;

                int requiredBatteryTime = (wi - di + r - 1) / r;
                currentTime += requiredBatteryTime;

                if (currentTime > di)
                {
                    File.WriteAllText(outputFile, "Impossible");
                    return;
                }

                plan.Add((currentTime, index));
            }

            using (var writer = new StreamWriter(outputFile))
            {
                foreach (var entry in plan)
                {
                    writer.WriteLine($"{entry.time} {entry.index}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
