using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class WeightedItem<T>
    {
        public T Item { get; set; }
        public int Weight { get; set; }
    }
}