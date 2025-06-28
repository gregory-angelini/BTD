using System;
using System.Collections;
using System.Collections.Generic;


namespace Common
{
    public class WeightedRandomSelector
    {
        Randomizer randomizer = new Randomizer();

        public WeightedRandomSelector(int seed = -1)
        {
            randomizer = new Randomizer(seed);
        }

        public WeightedItem<T> PickRandom<T>(List<WeightedItem<T>> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Item list is null or empty.");

            int totalWeight = 0;

            foreach (var item in items)
            {
                if (item.Weight <= 0)
                    throw new ArgumentException($"Invalid weight ({item.Weight}) for item {item}");

                totalWeight += item.Weight;
            }

            int randomValue = randomizer.RandomRange(0, totalWeight);
            int cumulative = 0;

            foreach (var item in items)
            {
                cumulative += item.Weight;

                if (randomValue < cumulative)
                    return item;
            }

            throw new InvalidOperationException("No item selected.");
        }
    }
}