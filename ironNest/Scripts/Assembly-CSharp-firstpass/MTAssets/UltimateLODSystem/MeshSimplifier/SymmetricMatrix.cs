using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	public struct SymmetricMatrix
	{
		public double m0;

		public double m1;

		public double m2;

		public double m3;

		public double m4;

		public double m5;

		public double m6;

		public double m7;

		public double m8;

		public double m9;

		public double this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SymmetricMatrix(double c)
		{
			m0 = 0.0;
			m1 = 0.0;
			m2 = 0.0;
			m3 = 0.0;
			m4 = 0.0;
			m5 = 0.0;
			m6 = 0.0;
			m7 = 0.0;
			m8 = 0.0;
			m9 = 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SymmetricMatrix(double m0, double m1, double m2, double m3, double m4, double m5, double m6, double m7, double m8, double m9)
		{
			this.m0 = 0.0;
			this.m1 = 0.0;
			this.m2 = 0.0;
			this.m3 = 0.0;
			this.m4 = 0.0;
			this.m5 = 0.0;
			this.m6 = 0.0;
			this.m7 = 0.0;
			this.m8 = 0.0;
			this.m9 = 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SymmetricMatrix(double a, double b, double c, double d)
		{
			m0 = 0.0;
			m1 = 0.0;
			m2 = 0.0;
			m3 = 0.0;
			m4 = 0.0;
			m5 = 0.0;
			m6 = 0.0;
			m7 = 0.0;
			m8 = 0.0;
			m9 = 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SymmetricMatrix operator +(SymmetricMatrix a, SymmetricMatrix b)
		{
			return default(SymmetricMatrix);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal double Determinant1()
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal double Determinant2()
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal double Determinant3()
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal double Determinant4()
		{
			return 0.0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double Determinant(int a11, int a12, int a13, int a21, int a22, int a23, int a31, int a32, int a33)
		{
			return 0.0;
		}
	}
}
