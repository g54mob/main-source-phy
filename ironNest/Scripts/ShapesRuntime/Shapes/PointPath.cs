using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Shapes
{
	public class PointPath<T> : DisposableMesh
	{
		protected List<T> path;

		public int Count => 0;

		public T LastPoint => default(T);

		public T this[int i]
		{
			get
			{
				return default(T);
			}
			set
			{
			}
		}

		protected void OnSetFirstDataPoint()
		{
		}

		public void ClearAllPoints()
		{
		}

		public void SetPoint(int index, T point)
		{
		}

		public void RemovePointAt(int index)
		{
		}

		public void AddPoint(T p)
		{
		}

		public void AddPoints(params T[] pts)
		{
		}

		public void AddPoints(IEnumerable<T> ptsToAdd)
		{
		}

		protected bool CheckCanAddContinuePoint([CallerMemberName] string callerName = null)
		{
			return false;
		}
	}
}
