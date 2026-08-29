using System.Collections.Generic;

namespace MagicaCloth2
{
	public class MinimumData<T1, T2> where T1 : unmanaged where T2 : unmanaged
	{
		private T1 minDist;

		private T2 minData;

		private bool isValid;

		public bool IsValid => isValid;

		public T1 MinDistance => minDist;

		public T2 MinData => minData;

		public void Add(T1 distance, T2 data)
		{
			if (isValid)
			{
				if (Comparer<T1>.Default.Compare(distance, minDist) < 0)
				{
					minDist = distance;
					minData = data;
				}
			}
			else
			{
				minDist = distance;
				minData = data;
				isValid = true;
			}
		}

		public void Clear()
		{
			isValid = false;
		}

		public override string ToString()
		{
			return $"MinimumData. IsValid:{isValid}, minDist:{minDist}, minData:{minData}";
		}
	}
}
