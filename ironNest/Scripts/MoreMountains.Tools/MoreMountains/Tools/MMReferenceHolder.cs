using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MoreMountains.Tools
{
	public struct MMReferenceHolder<T> : IDisposable where T : class
	{
		[CompilerGenerated]
		private sealed class _003Cget_All_003Ed__9 : IEnumerator<T>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private T _003C_003E2__current;

			private List<WeakReference<T>>.Enumerator _003C_003E7__wrap1;

			T IEnumerator<T>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
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
			public _003Cget_All_003Ed__9(int _003C_003E1__state)
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

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		private static List<WeakReference<T>> _instances;

		private WeakReference<T> _instance;

		public static T Any => null;

		public static IEnumerator<T> All
		{
			[IteratorStateMachine(typeof(MMReferenceHolder<>._003Cget_All_003Ed__9))]
			get
			{
				return null;
			}
		}

		public void Reference(T instance, bool cleanUp = false)
		{
		}

		public void Dispose()
		{
		}

		public static void CleanUp()
		{
		}

		private static void RepackNonNullReferences()
		{
		}

		public static T First(Func<T, bool> selector)
		{
			return null;
		}
	}
}
