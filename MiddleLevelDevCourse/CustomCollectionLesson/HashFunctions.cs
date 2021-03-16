namespace MiddleLevelDevCourse.CustomCollectionLesson
{
    public static class HashFunctions
    {
        public static int NoHash(int key) => key;

        public static int OldHash(int key) => ((key >> 24) ^ key) * 0x45d9f3b;

        public static int StrongHash(int key) => 101 * ((key >> 24) + 101 * ((key >> 16) + 101 * (key >> 8))) + key;
    }
}
