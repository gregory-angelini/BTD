using System;
using System.Collections;
using System.Collections.Generic;


namespace Common
{
    public class WeightUtils
    {
        public static List<WeightedItem<T>> ConvertChancesToWeights<T>(List<ItemChance<T>> items, Func<double, bool> isValidChance, int precision = 100)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Item list is null or empty.");

            if (precision <= 0)
                throw new ArgumentException($"Precision ({precision}) must be greater than zero.");

            List<WeightedItem<T>> weightedItems = new();

            foreach (var item in items)
            {
                var chance = item.Chance;

                if (!isValidChance(chance))
                    throw new ArgumentException($"Invalid chance ({chance}) value for item: {item}");

                int weight = (int)Math.Round(chance * precision);
                
                if (weight <= 0)
                    throw new ArgumentException($"Weight must be greater than zero for item: {item}");

                weightedItems.Add(
                    new WeightedItem<T>
                    {
                        Item = item.Item,
                        Weight = weight
                    });
            }

            return weightedItems;
        }
    }
}