using System.Collections;

namespace Poly.Game
{
	internal class FloatHistory : IEnumerable
	{
		private float[] values;

		private int nextWriteIdx;

		public ref float this[int idx] => ref values[(nextWriteIdx + idx) % values.Length];

		public ref float Current => ref values[nextWriteIdx];

		public FloatHistory(int length)
		{
			values = new float[length];
			nextWriteIdx = 0;
		}

		public void Add(float value)
		{
			values[nextWriteIdx++] = value;
			nextWriteIdx %= values.Length;
		}

		public IEnumerator GetEnumerator()
		{
			return values.GetEnumerator();
		}

		public void MoveNext()
		{
			nextWriteIdx = (nextWriteIdx + 1) % values.Length;
		}
	}
}
