using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace EfektywnoscAlgorytmow
{
    public class SortingAlgorithms
    {
        
        [Params(10, 1000, 100000)]
        public int Size { get; set; }

        public enum DataGenerationType
        {
            Random,
            Sorted,
            Reversed,
            AlmostSorted,
            FewUnique
        }
        
        [Params(DataGenerationType.Random, DataGenerationType.Sorted, DataGenerationType.Reversed, DataGenerationType.AlmostSorted, DataGenerationType.FewUnique)]
        public DataGenerationType DataType { get; set; }
        
        private int[] GenerateArray(DataGenerationType type)
        {
            switch (type)
            {
                case DataGenerationType.Random:
                    return Generators.GenerateRandom(Size, 0, 2147483647);
                case DataGenerationType.Sorted:
                    return Generators.GenerateSorted(Size, 0, 2147483647);
                case DataGenerationType.Reversed:
                    return Generators.GenerateReversed(Size, 0, 2147483647);
                case DataGenerationType.AlmostSorted:
                    return Generators.GenerateAlmostSorted(Size, 0, 2147483647, 0.05);
                case DataGenerationType.FewUnique:
                    return Generators.GenerateFewUnique(Size, 0, 2147483647);
                default:
                    throw new ArgumentException("Invalid data generation type");
            }
        }

        [Benchmark]
        public void InsertionSort()
        {
            int[] arr = GenerateArray(DataType);
            InsertionSort(arr);
        }
        
        public void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }

                arr[j + 1] = key;
            }
        }


        [Benchmark]
        public void MergeSort()
        {
            int[] arr = GenerateArray(DataType);
            MergeSort(arr);
        }
        
        public void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }
        
        private void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                // find center of array
                int middle = (left + right) / 2;

                // sort left half
                MergeSort(arr, left, middle);
                // sort right half
                MergeSort(arr, middle + 1, right);

                // scale both
                Merge(arr, left, middle, right);
            }
        }


        // Merges two subarrays of []arr.
        // First subarray is arr[l..m]
        // Second subarray is arr[m+1..r]
        private void Merge(int[] arr, int left, int middle, int right)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = middle - left + 1;
            int n2 = right - middle;

            // Create temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[left + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[middle + 1 + j];

            // Merge the temp arrays

            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;

            // Initial index of merged
            // subarray array
            int k = left;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }

                k++;
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        [Benchmark]
        public void QuickSortClassic()
        {
            int[] arr = GenerateArray(DataType);
            QuickSortClassic(arr);
        }
        
        public void QuickSortClassic(int[] arr)
        {
            QuickSortClassic(arr, 0, arr.Length - 1);
        }

        private void QuickSortClassic(int[] arr, int low, int high)
        {
            if (low < high)
            {
                // partitionIndex is index, of separating element
                int partitionIndex = Partition(arr, low, high);

                QuickSortClassic(arr, low, partitionIndex - 1);
                QuickSortClassic(arr, partitionIndex + 1, high);
            }
        }

        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }

            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);

            return i + 1;
        }


        [Benchmark]
        public void QuickSort()
        {
            int[] arr = GenerateArray(DataType);
            QuickSort(arr);
        }
        
        public void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        private void QuickSort(int[] arr, int left, int right)
        {
            Array.Sort(arr, left, right);
        }
    }
}

