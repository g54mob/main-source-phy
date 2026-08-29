using UnityEngine;

namespace ThisOtherThing.Utils
{
	public class MinimumAttribute : PropertyAttribute
	{
		public readonly float minFloat;

		public readonly int minInt;

		public MinimumAttribute(float min)
		{
			minFloat = min;
		}

		public MinimumAttribute(int min)
		{
			minInt = min;
		}
	}
}
