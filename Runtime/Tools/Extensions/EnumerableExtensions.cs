using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace GameSoft.Tools.Extensions
{
	public static class IEnumerableExtensions
	{
		private static Random _random;
		private static int IntRandom(int min, int max)
		{
			if (_random == null) _random = new Random();
			return _random.Next(min, max);
		}
	
		public static T GetShuffled<T>(this T array) where T : IList, new() {
			T newArray = new T();
			int count = array.Count;
			bool[] flags = new bool[count];
			for (int i = 0; i < count; i++) {
				int index = IntRandom(0, count);
				while (flags[index]) {
					index = IntRandom(0, count);
				}

				flags[index] = true;
				newArray.Add(array[index]);
			}
			return newArray;
		}
	
		public static List<T> GetShuffled<T>(this IEnumerable<T> array) {
			return array.ToList().GetShuffled();
		}
	
		public static T GetRandom<T>(this ICollection<T> array) {
			int count = array.Count;
			int index = IntRandom(0, count);
			using var link = array.GetEnumerator();
			int i = 0;
			while (link.MoveNext() && i < index)
			{
				i++;
			}

			return link.Current;
		}
	}
}