using System;

namespace MiddleLevelDevCourse.MaxBlockToAllocateLesson
{
    public class MaxBlockToAllocateLesson : ILesson
    {
        /* 
         * Result (MacBook Air 2020 i5 8GB):
         * 
         * Amount of available memory to allocate: 67,336 bytes
         * Max size of single block available in heap to allocate: 2,147,483,591 bytes
         * Amount of available memory to allocate after allocating the block: 2,147,712,064 bytes
         * Compare with physically available memory: ~8,589,934,592 bytes
         */

        public void Run()
        {
            Console.WriteLine($"Amount of available memory to allocate: {GetAmountOfAvailableMemory():n0} bytes");

            Console.WriteLine($"Max size of single block available in heap to allocate: {GetMaxSizeOfSingleBlockInHeap():n0} bytes");

            Console.WriteLine($"Amount of available memory to allocate after allocating the block: {GetAmountOfAvailableMemory():n0} bytes");

            Console.WriteLine($"Compare with physically available memory: ~{GetAmountOfRAM():n0} bytes");
        }

        private static long GetAmountOfAvailableMemory()
        {
            // false means don't wait for garbage collection.
            return GC.GetTotalMemory(false);
        }

        private static long GetMaxSizeOfSingleBlockInHeap()
        {
            var maxBlockSize = int.MaxValue;
            var isMaxBlockFound = false;
            while (!isMaxBlockFound)
            {
                try
                {
                    GC.AllocateArray<byte>(maxBlockSize);
                    isMaxBlockFound = true;
                }
                catch
                {
                    maxBlockSize--;
                }
            }

            return maxBlockSize;
        }

        private static long GetAmountOfRAM() => 8589934592;
    }
}
