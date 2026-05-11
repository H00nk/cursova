using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchCourseWorkNew;

public static class SearchAlgorithms
{
    public static int Comparisons = 0;

    public static int[] GenerateArray(int size)
    {
        int[] array = new int[size];

        for (int i = 0; i < size; i++)
        {
            array[i] = i + 1;
        }

        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            int j = random.Next(size);

            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return array;
    }

    public static async Task HighlightStep(
        ListBox listBox,
        int index)
    {
        listBox.SelectedIndex = index;

        listBox.ScrollIntoView(index);

        await Task.Delay(50);
    }

    public static async Task<int> LinearSearch(
        int[] array,
        int target,
        ListBox listBox)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Comparisons++;

            await HighlightStep(listBox, i);

            if (array[i] == target)
            {
                return i;
            }
        }

        return -1;
    }

    public static async Task<int> FibonacciSearch(
    int[] array,
    int target,
    ListBox listBox)
{
    Array.Sort(array);

    listBox.Items.Clear();

    foreach (int number in array)
    {
        listBox.Items.Add(number);
    }

    int fibM2 = 0;
    int fibM1 = 1;
    int fibM = fibM1 + fibM2;

    while (fibM < array.Length)
    {
        fibM2 = fibM1;
        fibM1 = fibM;
        fibM = fibM1 + fibM2;
    }

    int offset = -1;

    while (fibM > 1)
    {
        int i = Math.Min(
            offset + fibM2,
            array.Length - 1
        );

        Comparisons++;

        await HighlightStep(listBox, i);

        if (array[i] < target)
        {
            fibM = fibM1;
            fibM1 = fibM2;
            fibM2 = fibM - fibM1;

            offset = i;
        }
        else if (array[i] > target)
        {
            fibM = fibM2;
            fibM1 = fibM1 - fibM2;
            fibM2 = fibM - fibM1;
        }
        else
        {
            return i;
        }
    }

    if (fibM1 == 1 &&
        offset + 1 < array.Length &&
        array[offset + 1] == target)
    {
        await HighlightStep(
            listBox,
            offset + 1
        );

        return offset + 1;
    }

    return -1;
}

    public static async Task<int> InterpolationSearch(
    int[] array,
    int target,
    ListBox listBox)
    
{
    Array.Sort(array);

    listBox.Items.Clear();

    foreach (int number in array)

    {

        listBox.Items.Add(number);

    }
    int low = 0;
    int high = array.Length - 1;

    while (low <= high &&
           target >= array[low] &&
           target <= array[high])
    {
        Comparisons++;

        if (low == high)
        {
            await HighlightStep(listBox, low);

            if (array[low] == target)
            {
                return low;
            }

            return -1;
        }

        int denominator =
            array[high] - array[low];

        if (denominator == 0)
        {
            return -1;
        }

        int pos =
            low +
            (target - array[low]) *
            (high - low) /
            denominator;

        if (pos < low || pos > high)
        {
            return -1;
        }

        await HighlightStep(listBox, pos);

        if (array[pos] == target)
        {
            return pos;
        }

        if (array[pos] < target)
        {
            low = pos + 1;
        }
        else
        {
            high = pos - 1;
        }
    }

    return -1;
}

    public static async Task<int> HashSearch(
        int[] array,
        int target,
        ListBox listBox)
    {
        Dictionary<int, int> table =
            new Dictionary<int, int>();

        for (int i = 0; i < array.Length; i++)
        {
            table[array[i]] = i;
        }

        await Task.Delay(200);

        Comparisons++;

        if (table.ContainsKey(target))
        {
            int index = table[target];

            listBox.SelectedIndex = index;

            listBox.ScrollIntoView(index);

            return index;
        }

        return -1;
    }
}