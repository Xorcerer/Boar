using System;
using System.Collections.Generic;
using LoganZhou.Boar;
using System.Diagnostics;

namespace LoganZhou.Boar
{
	public class RandomByWeight<T>
    {
		class RangeNode
		{
			public static RangeNode CreateTarget(int number)
			{
				return new RangeNode { LowerBoundary = number, UpperBoundary = number };
			}

			public int LowerBoundary { get; set; }
			public int UpperBoundary { get; set; }

			public T Value { get; set; }

			/// <summary>
			/// Target Node, whose lower and upper boundary have the same value.
			/// </summary>
			/// <value><c>true</c> if this instance is target; otherwise, <c>false</c>.</value>
			public bool IsTarget { get { return LowerBoundary == UpperBoundary; } }
		}

		class Comparer : IComparer<RangeNode>
        {
			public int Compare(RangeNode x, RangeNode y)
            {
				if (x.IsTarget)
					return Compare(x.LowerBoundary, y.LowerBoundary, y.UpperBoundary);

				if (y.IsTarget)
					return  -1 * Compare(y.LowerBoundary, x.LowerBoundary, x.UpperBoundary);

                Debug.Assert(false);
                throw new InvalidOperationException("{0} failed to run binary search.".Format(typeof(RandomByWeight<T>)));
            }

            int Compare(int x, int lowerBound, int upperBound)
            {
                if (x < lowerBound)
                    return -1;

                return x > upperBound ? 1 : 0;
            }
        }

        Random _random = new Random();

        // TODO: Better name.
		readonly List<RangeNode> _weights = new List<RangeNode>();

        public bool Empty { get { return _weights.Count == 0; } }

		public int WeightSum { get { return _weights.Count == 0 ? 0 : _weights[_weights.Count - 1].UpperBoundary; } }

        public void Add(T item, int weight)
        {
            if (weight <= 0)
                throw new ArgumentOutOfRangeException("weight");

            int prevSum = WeightSum;
			var rangeNode = new RangeNode
			{
				LowerBoundary = prevSum,
				UpperBoundary = prevSum + weight,
				Value = item,
			};
			_weights.Add(rangeNode);
        }

        public T Next()
        {
            int weight = _random.Next(1, WeightSum);
            return GetByGivenWeight(weight);
        }

        T GetByGivenWeight(int weight)
        {
			var target = RangeNode.CreateTarget(weight);
			int index = _weights.BinarySearch(target, new Comparer());
			return _weights[index].Value;
        }
    }
}

