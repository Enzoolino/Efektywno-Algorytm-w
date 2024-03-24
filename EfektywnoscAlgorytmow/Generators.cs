
    public class Generators
    {
        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            Random random = new Random();
            
            int[] a = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = random.Next(minVal, maxVal);
            }

            return a;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }

        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal, double disturbancePercentage)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Random random = new Random();

            int disturbanceCount = (int)(size * disturbancePercentage);

            for (int i = 0; i < disturbanceCount; i++)
            {
                int index1 = random.Next(0, size);
                int index2 = random.Next(0, size);

                (a[index1], a[index2]) = (a[index2], a[index1]);
            }

            return a;
        }

        public static int[] GenerateFewUnique(int size, int minVal, int maxVal, int uniqueCount)
        {
            Random random = new Random();
            int[] a = new int[size];
            
            for (int i = 0; i < size; i++)
            {
                a[i] = random.Next(minVal, maxVal + 1);
            }
            
            HashSet<int> uniqueValues = new HashSet<int>();
            while (uniqueValues.Count < uniqueCount)
            {
                uniqueValues.Add(random.Next(minVal, maxVal + 1));
            }
            
            foreach (int uniqueValue in uniqueValues)
            {
                int index;
                do
                {
                    index = random.Next(0, size);
                } while (a[index] != 0);
                a[index] = uniqueValue;
            }
            
            for (int i = 0; i < size; i++)
            {
                if (a[i] == 0)
                {
                    a[i] = random.Next(minVal, maxVal + 1);
                }
            }

            return a;
        }
        
    
    }


