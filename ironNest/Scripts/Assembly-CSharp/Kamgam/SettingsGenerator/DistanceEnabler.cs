using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class DistanceEnabler : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CStateCoroutine_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public DistanceEnabler _003C_003E4__this;

			public bool enable;

			private float _003Cstep_003E5__2;

			private int _003Ci_003E5__3;

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
			public _003CStateCoroutine_003Ed__13(int _003C_003E1__state)
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
		}

		[SerializeField]
		[Tooltip("If enabled, this light is treated as low priority and its cull distance is reduced to 20% of the standard value.\n\nLow-priority cull distances:\n  Shadow res 512  → 3\n  Shadow res 1024 → 5\n  Shadow res 2048 → 8\n  Shadow res 4096 → 12\n  Default         → 2\n\nUse this for decorative or fill lights that are less important to render at distance.\n\nDefault: OFF (standard cull distance applies).")]
		private bool lowPriority;

		private readonly WaitForEndOfFrame waitForEndOfFrame;

		private Light light;

		private Transform player;

		private Transform lightTransform;

		private Coroutine stateCoroutine;

		private float defaultIntensity;

		private bool initialised;

		private bool? stateCoroutineTargetState;

		private void OnEnable()
		{
		}

		private void Update()
		{
		}

		private int CalculateCullDistance()
		{
			return 0;
		}

		private bool ShouldBeOn(int cullDistance)
		{
			return false;
		}

		[IteratorStateMachine(typeof(_003CStateCoroutine_003Ed__13))]
		private IEnumerator StateCoroutine(bool enable)
		{
			return null;
		}
	}
}
