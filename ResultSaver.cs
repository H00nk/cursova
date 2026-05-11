using System.IO;

namespace SearchCourseWorkNew;

public static class ResultSaver
{
    public static void SaveResult(
        int method,
        int result,
        long time,
        int comparisons)
    {
        string methodName = "";

        switch (method)
        {
            case 0:
                methodName =
                    "Послідовний пошук";
                break;

            case 1:
                methodName =
                    "Пошук Фібоначчі";
                break;

            case 2:
                methodName =
                    "Інтерполяційний пошук";
                break;

            case 3:
                methodName =
                    "Хеш-пошук";
                break;
        }

        using StreamWriter writer =
            new StreamWriter(
                "results.txt",
                true
            );

        writer.WriteLine(
            "===================="
        );

        writer.WriteLine(
            $"Метод: {methodName}"
        );

        if (result != -1)
        {
            writer.WriteLine(
                $"Елемент знайдено. Індекс: {result}"
            );
        }
        else
        {
            writer.WriteLine(
                "Елемент не знайдено"
            );
        }

        writer.WriteLine(
            $"Час виконання: {time} ms"
        );

        writer.WriteLine(
            $"Кількість порівнянь: {comparisons}"
        );

        writer.WriteLine();
    }
}