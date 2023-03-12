using System;

namespace Algorithms
{
    class Program
    {
        static public void BubbleSort(int[] array)
        {
            int length = array.Length;
            int temp = array[0];

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
        static public void SelectSort(int[] arr)
        {
            int n = arr.Length;

            // One by one move boundary of unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in unsorted array
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[min_idx])
                        min_idx = j;

                // Swap the found minimum element with the first
                // element
                int temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;
            }
        }
        static private int Partition(int[] arr, int left, int right)
        {
            int pivot;
            pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    int temp = arr[right];
                    arr[right] = arr[left];
                    arr[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        static public void QuickSort(int[] arr, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(arr, left, right);
                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
                }
            }
        }
        static private void MainMerge(int[] numbers, int left, int mid, int right)
        {
            int[] temp = new int[25];
            int i, eol, num, pos;
            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);

            while ((left <= eol) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid])
                    temp[pos++] = numbers[left++];
                else
                    temp[pos++] = numbers[mid++];
            }
            while (left <= eol)
                temp[pos++] = numbers[left++];
            while (mid <= right)
                temp[pos++] = numbers[mid++];
            for (i = 0; i < num; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }
        static public void SortMerge(int[] numbers, int left, int right)
        {
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                SortMerge(numbers, left, mid);
                SortMerge(numbers, (mid + 1), right);
                MainMerge(numbers, left, (mid + 1), right);
            }
        }
        static public void HeapSort(int[] arr)
        {
            int N = arr.Length;

            // Build heap (rearrange array)
            for (int i = N / 2 - 1; i >= 0; i--)
                Heapify(arr, N, i);

            // One by one extract an element from heap
            for (int i = N - 1; i > 0; i--)
            {
                // Move current root to end
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                // call max heapify on the reduced heap
                Heapify(arr, i, 0);
            }
        }
        static private void Heapify(int[] arr, int N, int i)
        {
            int largest = i; // Initialize largest as root
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (l < N && arr[l] > arr[largest])
                largest = l;

            // If right child is larger than largest so far
            if (r < N && arr[r] > arr[largest])
                largest = r;

            // If largest is not root
            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                // Recursively heapify the affected sub-tree
                Heapify(arr, N, largest);
            }
        }
        static public void CountSort(int[] arr)
        {
            int n = arr.Length;

            // The output character array that
            // will have sorted arr
            int[] output = new int[n];

            // Create a count array to store
            // count of individual characters
            // and initialize count array as 0
            int[] count = new int[256];

            for (int i = 0; i < 256; ++i)
                count[i] = 0;

            // store count of each character
            for (int i = 0; i < n; ++i)
                ++count[arr[i]];

            // Change count[i] so that count[i]
            // now contains actual position of
            // this character in output array
            for (int i = 1; i <= 255; ++i)
                count[i] += count[i - 1];

            // Build the output character array
            // To make it stable we are operating in reverse
            // order.
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            // Copy the output array to arr, so
            // that arr now contains sorted
            // characters
            for (int i = 0; i < n; ++i)
                arr[i] = output[i];
        }
        static public void PrintArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] arr = { 64, 25, 12, 22, 11 };
            BubbleSort(arr);
            PrintArray(arr);

            int maxIndex = 4;

            arr[0] = 64; arr[1] = 25; arr[2] = 12; arr[3] = 22; arr[4] = 11;
            SelectSort(arr);
            PrintArray(arr);

            arr[0] = 64; arr[1] = 25; arr[2] = 12; arr[3] = 22; arr[4] = 11;
            QuickSort(arr, 0 , maxIndex);
            PrintArray(arr);

            arr[0] = 64; arr[1] = 25; arr[2] = 12; arr[3] = 22; arr[4] = 11;
            SortMerge(arr, 0, maxIndex);
            PrintArray(arr);

            arr[0] = 64; arr[1] = 25; arr[2] = 12; arr[3] = 22; arr[4] = 11;
            HeapSort(arr);
            PrintArray(arr);

            arr[0] = 64; arr[1] = 25; arr[2] = 12; arr[3] = 22; arr[4] = 11;
            CountSort(arr);
            PrintArray(arr);
        }
    }
}
