using Avalonia.Controls;
using System;
using System.Diagnostics;
using System.Linq;

namespace SearchCourseWorkNew;

public partial class MainWindow : Window
{
    private int[]? array;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void GenerateButton_Click(
        object? sender,
        Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!int.TryParse(SizeTextBox.Text, out int size))
        {
            ResultTextBlock.Text =
                "Некоректний розмір масиву";

            return;
        }

        if (size < 100)
        {
            ResultTextBlock.Text =
                "Розмір масиву повинен бути не менше 100";

            return;
        }

        array = SearchAlgorithms.GenerateArray(size);

        ArrayListBox.Items.Clear();

        foreach (int number in array)
        {
            ArrayListBox.Items.Add(number);
        }

        ResultTextBlock.Text =
            "Масив успішно згенеровано";

        ComparisonsTextBlock.Text = "";
        TimeTextBlock.Text = "";
    }

    private async void SearchButton_Click(
        object? sender,
        Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (array == null)
        {
            ResultTextBlock.Text =
                "Спочатку згенеруйте масив";

            return;
        }

        if (!int.TryParse(TargetTextBox.Text, out int target))
        {
            ResultTextBlock.Text =
                "Некоректне значення";

            return;
        }

        SearchAlgorithms.Comparisons = 0;

        int result = -1;

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        switch (MethodComboBox.SelectedIndex)
        {
            case 0:

                result =
                    await SearchAlgorithms.LinearSearch(
                        array,
                        target,
                        ArrayListBox
                    );

                break;

            case 1:

                result =
                    await SearchAlgorithms.FibonacciSearch(
                        array,
                        target,
                        ArrayListBox
                    );

                break;

            case 2:

                result =
                    await SearchAlgorithms.InterpolationSearch(
                        array,
                        target,
                        ArrayListBox
                    );

                break;

            case 3:

                result =
                    await SearchAlgorithms.HashSearch(
                        array,
                        target,
                        ArrayListBox
                    );

                break;
        }

        stopwatch.Stop();

        if (result != -1)
        {
            ResultTextBlock.Text =
                $"Елемент знайдено. Індекс: {result}";

            ArrayListBox.SelectedIndex = result;

            ArrayListBox.ScrollIntoView(result);
        }
        else
        {
            ResultTextBlock.Text =
                "Елемент не знайдено";
        }

        ComparisonsTextBlock.Text =
            $"Кількість порівнянь: {SearchAlgorithms.Comparisons}";

        TimeTextBlock.Text =
            $"Час виконання: {stopwatch.ElapsedMilliseconds} ms";

        ResultSaver.SaveResult(
            MethodComboBox.SelectedIndex,
            result,
            stopwatch.ElapsedMilliseconds,
            SearchAlgorithms.Comparisons
        );
    }
}