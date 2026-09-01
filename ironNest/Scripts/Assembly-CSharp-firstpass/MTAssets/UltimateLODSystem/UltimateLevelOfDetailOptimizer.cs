using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MTAssets.UltimateLODSystem
{
	[AddComponentMenu("MT Assets/Ultimate LOD System/Ultimate Level Of Detail Optimizer")]
	public class UltimateLevelOfDetailOptimizer : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CUlodOptimizationLoop_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UltimateLevelOfDetailOptimizer _003C_003E4__this;

			private int _003Ci_003E5__2;

			private List<UltimateLevelOfDetail>.Enumerator _003C_003E7__wrap2;

			object IEnumerator<object>.Current
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
			public _003CUlodOptimizationLoop_003Ed__9(int _003C_003E1__state)
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

		private WaitForSecondsRealtime DELAY_BETWEEN_OPTIMIZATION_UPDATES;

		private WaitForSecondsRealtime DELAY_BETWEEN_GAMEOBJECTS_STATE_CHANGE;

		private float ADITIONAL_CULLING_DISTANCE_OFFSET;

		private RuntimeInstancesDetector runtimeInstancesDetector;

		private int[] instructionsToMakeOnUlods;

		[HideInInspector]
		public bool enableOptimizationTasks;

		[HideInInspector]
		public List<UltimateLevelOfDetail> ulodsToBeIgnored;

		public void Awake()
		{
		}

		private bool isThisUlodPresentOnUlodsToBeIgnored(UltimateLevelOfDetail ulod)
		{
			return false;
		}

		[IteratorStateMachine(typeof(_003CUlodOptimizationLoop_003Ed__9))]
		private IEnumerator UlodOptimizationLoop()
		{
			return null;
		}
	}
}
