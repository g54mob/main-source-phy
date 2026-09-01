using System;
using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier.Internal
{
	internal struct Triangle : IEquatable<Triangle>
	{
		public int index;

		public int v0;

		public int v1;

		public int v2;

		public int subMeshIndex;

		public int va0;

		public int va1;

		public int va2;

		public double err0;

		public double err1;

		public double err2;

		public double err3;

		public bool deleted;

		public bool dirty;

		public Vector3d n;

		public int this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Triangle(int index, int v0, int v1, int v2, int subMeshIndex)
		{
			this.index = 0;
			this.v0 = 0;
			this.v1 = 0;
			this.v2 = 0;
			this.subMeshIndex = 0;
			va0 = 0;
			va1 = 0;
			va2 = 0;
			err0 = 0.0;
			err1 = 0.0;
			err2 = 0.0;
			err3 = 0.0;
			deleted = false;
			dirty = false;
			n = default(Vector3d);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetAttributeIndices(int[] attributeIndices)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAttributeIndex(int index, int value)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetErrors(double[] err)
		{
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public bool Equals(Triangle other)
		{
			return false;
		}
	}
}
