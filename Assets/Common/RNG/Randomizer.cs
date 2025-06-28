using System;

namespace Common
{
    public class Randomizer
    {
        Random random;

        public Randomizer(int seed = -1)
        {
            if (seed == -1)
                random = new Random();
            else
                random = new Random(seed);
        }

        public int Next()
        {
            return random.Next();
        }

        public int RandomRange(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}