using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Зчитуємо вхідні дані з файлу
        string input = File.ReadAllText("INPUT.TXT").Trim();

        // Обчислюємо кількість способів декодування
        int result = CountDecodings(input);

        // Записуємо результат у файл
        File.WriteAllText("OUTPUT.TXT", result.ToString());
    }

    static int CountDecodings(string sequence)
    {
        int n = sequence.Length;
        if (n == 0 || sequence[0] == '0')
            return 0;

        int[] dp = new int[n + 1];
        dp[0] = 1; // Порожній рядок має один спосіб декодування
        dp[1] = sequence[0] != '0' ? 1 : 0;

        for (int i = 2; i <= n; i++)
        {
            // Одна цифра
            int oneDigit = int.Parse(sequence.Substring(i - 1, 1));
            if (oneDigit >= 1 && oneDigit <= 33)
                dp[i] += dp[i - 1];

            // Дві цифри
            int twoDigits = int.Parse(sequence.Substring(i - 2, 2));
            if (twoDigits >= 10 && twoDigits <= 33)
                dp[i] += dp[i - 2];
        }

        return dp[n];
    }
}
