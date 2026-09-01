using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	public static class TransformExtensions
	{
		[CompilerGenerated]
		private sealed class _003CMMEnumerateAllParents_003Ed__5 : IEnumerable<Transform>, IEnumerable, IEnumerator<Transform>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Transform _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private bool includeSelf;

			public bool _003C_003E3__includeSelf;

			private Transform targetTransform;

			public Transform _003C_003E3__targetTransform;

			Transform IEnumerator<Transform>.Current
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
			public _003CMMEnumerateAllParents_003Ed__5(int _003C_003E1__state)
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
			IEnumerator<Transform> IEnumerable<Transform>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		public static void MMDestroyAllChildren(this Transform transform)
		{
		}

		public static Transform MMFindDeepChildBreadthFirst(this Transform parent, string transformName)
		{
			return null;
		}

		public static Transform MMFindDeepChildDepthFirst(this Transform parent, string transformName)
		{
			return null;
		}

		public static void ChangeLayersRecursively(this Transform transform, string layerName)
		{
		}

		public static void ChangeLayersRecursively(this Transform transform, int layerIndex)
		{
		}

		[IteratorStateMachine(typeof(_003CMMEnumerateAllParents_003Ed__5))]
		public static IEnumerable<Transform> MMEnumerateAllParents(this Transform targetTransform, bool includeSelf = false)
		{
			return null;
		}
	}
}
