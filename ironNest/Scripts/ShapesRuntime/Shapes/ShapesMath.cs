using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Shapes
{
	public static class ShapesMath
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003C_003Ec__DisplayClass35_0
		{
			public PolylinePoint a;

			public PolylinePoint b;

			public Vector3 center;

			public float radius;
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003C_003Ec__DisplayClass36_0
		{
			public Vector3 center;

			public float radius;
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003C_003Ec__DisplayClass37_0
		{
			public Vector2 center;

			public float radius;
		}

		[CompilerGenerated]
		private sealed class _003CCubicBezierPointsSkipFirst_003Ed__38 : IEnumerable<PolylinePoint>, IEnumerable, IEnumerator<PolylinePoint>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private PolylinePoint _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private int count;

			public int _003C_003E3__count;

			private PolylinePoint a;

			public PolylinePoint _003C_003E3__a;

			private PolylinePoint b;

			public PolylinePoint _003C_003E3__b;

			private PolylinePoint c;

			public PolylinePoint _003C_003E3__c;

			private PolylinePoint d;

			public PolylinePoint _003C_003E3__d;

			private int _003Ci_003E5__2;

			PolylinePoint IEnumerator<PolylinePoint>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(PolylinePoint);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CCubicBezierPointsSkipFirst_003Ed__38(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<PolylinePoint> IEnumerable<PolylinePoint>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CCubicBezierPointsSkipFirst_003Ed__40 : IEnumerable<Vector3>, IEnumerable, IEnumerator<Vector3>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Vector3 _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private int count;

			public int _003C_003E3__count;

			private Vector3 a;

			public Vector3 _003C_003E3__a;

			private Vector3 b;

			public Vector3 _003C_003E3__b;

			private Vector3 c;

			public Vector3 _003C_003E3__c;

			private Vector3 d;

			public Vector3 _003C_003E3__d;

			private int _003Ci_003E5__2;

			Vector3 IEnumerator<Vector3>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(Vector3);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CCubicBezierPointsSkipFirst_003Ed__40(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<Vector3> IEnumerable<Vector3>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CCubicBezierPointsSkipFirst_003Ed__41 : IEnumerable<Vector2>, IEnumerable, IEnumerator<Vector2>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Vector2 _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private int count;

			public int _003C_003E3__count;

			private Vector2 a;

			public Vector2 _003C_003E3__a;

			private Vector2 b;

			public Vector2 _003C_003E3__b;

			private Vector2 c;

			public Vector2 _003C_003E3__c;

			private Vector2 d;

			public Vector2 _003C_003E3__d;

			private int _003Ci_003E5__2;

			Vector2 IEnumerator<Vector2>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(Vector2);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CCubicBezierPointsSkipFirst_003Ed__41(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<Vector2> IEnumerable<Vector2>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39 : IEnumerable<PolylinePoint>, IEnumerable, IEnumerator<PolylinePoint>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private PolylinePoint _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private int count;

			public int _003C_003E3__count;

			private PolylinePoint style;

			public PolylinePoint _003C_003E3__style;

			private Vector3 a;

			public Vector3 _003C_003E3__a;

			private Vector3 b;

			public Vector3 _003C_003E3__b;

			private Vector3 c;

			public Vector3 _003C_003E3__c;

			private Vector3 d;

			public Vector3 _003C_003E3__d;

			private int _003Ci_003E5__2;

			PolylinePoint IEnumerator<PolylinePoint>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(PolylinePoint);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<PolylinePoint> IEnumerable<PolylinePoint>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CGetArcPoints_003Ed__35 : IEnumerable<PolylinePoint>, IEnumerable, IEnumerator<PolylinePoint>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private PolylinePoint _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private PolylinePoint a;

			public PolylinePoint _003C_003E3__a;

			private PolylinePoint b;

			public PolylinePoint _003C_003E3__b;

			private Vector3 center;

			public Vector3 _003C_003E3__center;

			private float radius;

			public float _003C_003E3__radius;

			private int count;

			public int _003C_003E3__count;

			private Vector3 normA;

			public Vector3 _003C_003E3__normA;

			private Vector3 normB;

			public Vector3 _003C_003E3__normB;

			private _003C_003Ec__DisplayClass35_0 _003C_003E8__1;

			private int _003Ci_003E5__2;

			PolylinePoint IEnumerator<PolylinePoint>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(PolylinePoint);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CGetArcPoints_003Ed__35(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<PolylinePoint> IEnumerable<PolylinePoint>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CGetArcPoints_003Ed__36 : IEnumerable<Vector3>, IEnumerable, IEnumerator<Vector3>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Vector3 _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private Vector3 center;

			public Vector3 _003C_003E3__center;

			private float radius;

			public float _003C_003E3__radius;

			private int count;

			public int _003C_003E3__count;

			private Vector3 normA;

			public Vector3 _003C_003E3__normA;

			private Vector3 normB;

			public Vector3 _003C_003E3__normB;

			private _003C_003Ec__DisplayClass36_0 _003C_003E8__1;

			private int _003Ci_003E5__2;

			Vector3 IEnumerator<Vector3>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(Vector3);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CGetArcPoints_003Ed__36(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<Vector3> IEnumerable<Vector3>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CGetArcPoints_003Ed__37 : IEnumerable<Vector2>, IEnumerable, IEnumerator<Vector2>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Vector2 _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private Vector2 center;

			public Vector2 _003C_003E3__center;

			private float radius;

			public float _003C_003E3__radius;

			private int count;

			public int _003C_003E3__count;

			private Vector2 normA;

			public Vector2 _003C_003E3__normA;

			private Vector2 normB;

			public Vector2 _003C_003E3__normB;

			private _003C_003Ec__DisplayClass37_0 _003C_003E8__1;

			private int _003Ci_003E5__2;

			Vector2 IEnumerator<Vector2>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(Vector2);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CGetArcPoints_003Ed__37(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<Vector2> IEnumerable<Vector2>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

		public const float TAU = (float)Math.PI * 2f;

		public const double DEG_TO_RAD = Math.PI / 180.0;

		private const double SINC_W = 0.01;

		private const double SINC_P_C2 = -1.0 / 6.0;

		private const double SINC_P_C4 = 1.0 / 120.0;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Frac(float x)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Eerp(float a, float b, float t)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SmoothCos01(float x)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 AngToDir(float angRad)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DirToAng(Vector2 dir)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Rotate90CW(Vector2 v)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Rotate90CCW(Vector2 v)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 AtLeast0(Vector4 v)
		{
			return default(Vector4);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float MaxComp(Vector4 v)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasNegativeValues(Vector4 v)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Determinant(Vector2 a, Vector2 b)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Luminance(Color c)
		{
			return 0f;
		}

		public static float GetLineSegmentProjectionT(Vector3 a, Vector3 b, Vector3 p)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PolylinePoint WeightedSum(Vector4 w, PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d)
		{
			return default(PolylinePoint);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 WeightedSum(Vector4 w, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			return default(Vector3);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 WeightedSum(Vector4 w, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color WeightedSum(Vector4 w, Color a, Color b, Color c, Color d)
		{
			return default(Color);
		}

		public static bool PointInsideTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 point, float aMargin = 0f, float bMargin = 0f, float cMargin = 0f)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 Dir(Vector2 a, Vector2 b)
		{
			return default(Vector2);
		}

		public static float PolygonSignedArea(List<Vector2> pts)
		{
			return 0f;
		}

		public static Vector2 Rotate(Vector2 v, float angRad)
		{
			return default(Vector2);
		}

		private static float DeltaAngleRad(float a, float b)
		{
			return 0f;
		}

		public static float InverseLerpAngleRad(float a, float b, float v)
		{
			return 0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 t)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Rect r, Vector2 t)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 v)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 InverseLerp(Rect r, Vector2 pt)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector2 Remap(Vector2 iMin, Vector2 iMax, Vector2 oMin, Vector2 oMax, Vector2 value)
		{
			return default(Vector2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Remap(Rect iRect, Rect oRect, Vector2 iPos)
		{
			return default(Vector2);
		}

		public static Vector3 Abs(Vector3 v)
		{
			return default(Vector3);
		}

		public static float RandomGaussian(float min = 0f, float max = 1f)
		{
			return 0f;
		}

		public static Vector3 GetRandomPerpendicularVector(Vector3 a)
		{
			return default(Vector3);
		}

		[IteratorStateMachine(typeof(_003CGetArcPoints_003Ed__35))]
		public static IEnumerable<PolylinePoint> GetArcPoints(PolylinePoint a, PolylinePoint b, Vector3 normA, Vector3 normB, Vector3 center, float radius, int count)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CGetArcPoints_003Ed__36))]
		public static IEnumerable<Vector3> GetArcPoints(Vector3 normA, Vector3 normB, Vector3 center, float radius, int count)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CGetArcPoints_003Ed__37))]
		public static IEnumerable<Vector2> GetArcPoints(Vector2 normA, Vector2 normB, Vector2 center, float radius, int count)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CCubicBezierPointsSkipFirst_003Ed__38))]
		public static IEnumerable<PolylinePoint> CubicBezierPointsSkipFirst(PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d, int count)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CCubicBezierPointsSkipFirstMatchStyle_003Ed__39))]
		public static IEnumerable<PolylinePoint> CubicBezierPointsSkipFirstMatchStyle(PolylinePoint style, Vector3 a, Vector3 b, Vector3 c, Vector3 d, int count)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CCubicBezierPointsSkipFirst_003Ed__40))]
		public static IEnumerable<Vector3> CubicBezierPointsSkipFirst(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int count)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CCubicBezierPointsSkipFirst_003Ed__41))]
		public static IEnumerable<Vector2> CubicBezierPointsSkipFirst(Vector2 a, Vector2 b, Vector2 c, Vector2 d, int count)
		{
			return null;
		}

		public static Vector4 GetCubicBezierWeights(float t)
		{
			return default(Vector4);
		}

		public static PolylinePoint CubicBezier(PolylinePoint a, PolylinePoint b, PolylinePoint c, PolylinePoint d, float t)
		{
			return default(PolylinePoint);
		}

		public static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
		{
			return default(Vector3);
		}

		public static Vector2 CubicBezier(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
		{
			return default(Vector2);
		}

		private static Vector3 CubicBezierDirectionIsh(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
		{
			return default(Vector3);
		}

		public static float GetApproximateAngularCurveSumDegrees(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int vertCount)
		{
			return 0f;
		}

		public static Matrix4x4 AffineMtxMul(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return default(Matrix4x4);
		}

		public static float Cosinc(float x)
		{
			return 0f;
		}

		public static double Cosinc(double x)
		{
			return 0.0;
		}

		public static float Sinc(float x)
		{
			return 0f;
		}

		public static double Sinc(double x)
		{
			return 0.0;
		}
	}
}
