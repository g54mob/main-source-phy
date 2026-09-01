using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	internal static class ShapesExtensions
	{
		[CompilerGenerated]
		private sealed class _003CZip_003Ed__14<T1, T2, T3, TResult> : IEnumerable<TResult>, IEnumerable, IEnumerator<TResult>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private TResult _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private IEnumerable<T1> source;

			public IEnumerable<T1> _003C_003E3__source;

			private IEnumerable<T2> second;

			public IEnumerable<T2> _003C_003E3__second;

			private IEnumerable<T3> third;

			public IEnumerable<T3> _003C_003E3__third;

			private Func<T1, T2, T3, TResult> func;

			public Func<T1, T2, T3, TResult> _003C_003E3__func;

			private IEnumerator<T1> _003Ce1_003E5__2;

			private IEnumerator<T2> _003Ce2_003E5__3;

			private IEnumerator<T3> _003Ce3_003E5__4;

			TResult IEnumerator<TResult>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(TResult);
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
			public _003CZip_003Ed__14(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			private void _003C_003Em__Finally2()
			{
			}

			private void _003C_003Em__Finally3()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		public static void ForEach<T>(this IEnumerable<T> elems, Action<T> action)
		{
		}

		public static Vector3 Rot90CCW(this Vector3 v)
		{
			return default(Vector3);
		}

		public static int AsInt(this bool b)
		{
			return 0;
		}

		public static Vector4 ToVector4(this Rect r)
		{
			return default(Vector4);
		}

		public static float TaxicabMagnitude(this Vector3 v)
		{
			return 0f;
		}

		public static float AvgComponentMagnitude(this Vector3 v)
		{
			return 0f;
		}

		internal static Color ColorSpaceAdjusted(this Color c)
		{
			return default(Color);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInt_Shapes(this Material m, int id, int value)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInt_Shapes(this MaterialPropertyBlock mpb, int id, int value)
		{
		}

		public static void DestroyBranched(this UnityEngine.Object obj)
		{
		}

		public static void DestroyEndOfFrameEmulated(this UnityEngine.Object obj)
		{
		}

		public static void TryDestroyInOnDestroy(this UnityEngine.Object caller, UnityEngine.Object obj)
		{
		}

		public static int Product<T>(this IEnumerable<T> arr, Func<T, int> mulVal)
		{
			return 0;
		}

		public static float Product<T>(this IEnumerable<T> arr, Func<T, float> mulVal)
		{
			return 0f;
		}

		[IteratorStateMachine(typeof(_003CZip_003Ed__14<, , , >))]
		public static IEnumerable<TResult> Zip<T1, T2, T3, TResult>(this IEnumerable<T1> source, IEnumerable<T2> second, IEnumerable<T3> third, Func<T1, T2, T3, TResult> func)
		{
			return null;
		}

		public static int PopCount(this uint i)
		{
			return 0;
		}
	}
}
