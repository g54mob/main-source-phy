using System;
using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal struct Vertex : IEquatable<Vertex>
	{
		public int index;

		public Vector3d p;

		public int tstart;

		public int tcount;

		public SymmetricMatrix q;

		public bool borderEdge;

		public bool uvSeamEdge;

		public bool uvFoldoverEdge;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vertex(int index, Vector3d p)
		{
			this.index = 0;
			this.p = default(Vector3d);
			tstart = 0;
			tcount = 0;
			q = default(SymmetricMatrix);
			borderEdge = false;
			uvSeamEdge = false;
			uvFoldoverEdge = false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public bool Equals(Vertex other)
		{
			return false;
		}
	}
}
