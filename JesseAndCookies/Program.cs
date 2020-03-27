using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution
{

    static int cookies(int k, int[] A)
    {
        var minHeap = ToHeap(A);
        return CombineCookies(minHeap, k);
    }

    private static int CombineCookies(int[] minHeap, int k)
    {
        int operations = 0;
        var amount = minHeap.Length;
        while (minHeap[0] < k && amount > 1)
        {
            operations++;

            var min = minHeap[0];
            amount = RemoveMin(minHeap, amount);
            var min2 = minHeap[0];
            amount = RemoveMin(minHeap, amount);
            amount = Insert(minHeap, min + 2 * min2, amount);
        }

        if (minHeap[0] < k) return -1;
        return operations;
    }

    private static int[] ToHeap(int[] A)
    {
        var minHeap = new int[A.Length];
        var heapSize = 0;
        foreach (var value in A)
        {
            heapSize = Insert(minHeap, value, heapSize);
        }

        return minHeap;
    }

    private static int Insert(int[] A, int value, int count)
    {
        A[count] = value;
        var result = count + 1;
        HeapifyUp(A, count);

        return result;
    }

    private static void HeapifyUp(int[] A, int index)
    {
        if (index == 0) return;
        var parent = (int)Math.Floor((index - 1) / 2.0d);

        if (A[index] < A[parent])
        {
            Swap(A, index, parent);
            HeapifyUp(A, parent);
        }
    }

    private static int RemoveMin(int[] A, int count)
    {
        var result = count - 1;
        A[0] = A[result];

        Heapify(A, 0, result);

        return result;
    }

    private static void Heapify(int[] A, int index, int count)
    {

        var left = 2 * index + 1;
        var right = 2 * index + 2;
        var smallest = index;

        if (left < count && A[left] < A[index])
            smallest = left;
        if (right < count && A[right] < A[smallest])
            smallest = right;

        if (smallest != index)
        {
            Swap(A, index, smallest);
            Heapify(A, smallest, count);
        }
    }

    private static void Swap(int[] A, int first, int second)
    {
        var tmp = A[first];
        A[first] = A[second];
        A[second] = tmp;
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(Console.OpenStandardOutput());//@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0]);

        int k = Convert.ToInt32(nk[1]);

        int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), ATemp => Convert.ToInt32(ATemp))
        ;
        int result = cookies(k, A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
