using System.Linq;

namespace Poly.Game
{
	public class UnionFind
	{
		private int[] parent;

		public UnionFind(int numIndices)
		{
			parent = Enumerable.Range(0, numIndices).ToArray();
		}

		public int FindRoot(int i)
		{
			if (parent[i] != i)
			{
				parent[i] = FindRoot(parent[i]);
			}
			return parent[i];
		}

		public void Union(int a, int b)
		{
			a = FindRoot(a);
			b = FindRoot(b);
			if (a != b)
			{
				parent[a] = b;
			}
		}
	}
}
