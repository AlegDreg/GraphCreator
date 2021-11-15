using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Actions
    {
        public int GetMax(int[] array)
        {
            int max = array[0];

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                    max = array[i];
            }

            return max;
        }

        public int GetLenght(int[] array)
        {
            int res = GetMax(array);

            if (res > 512)
                return res;
            else return 512;
        }

        public int GetIndexMinEl(int[]array)
        {
            int min = array[0];
            int minIn = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    minIn = i;
                }
            }

            return minIn;
        }

        public int GetIndexMaxEl(int[] array)
        {
            int max = array[0];
            int maxIn = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                    maxIn = i;
                }
            }

            return maxIn;
        }

        public int GetMin(int[] array)
        {
            int min = array[0];

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min)
                    min = array[i];
            }

            return min;
        }
    }
}
