using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Cinemachine/MMCinemachinePriorityBrainListener")]
	[RequireComponent(typeof(CinemachineBrain))]
	public class MMCinemachinePriorityBrainListener : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CResetBlendDefinition_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMCinemachinePriorityBrainListener _003C_003E4__this;

			public float delay;

			private float _003Ctimer_003E5__2;

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
			public _003CResetBlendDefinition_003Ed__8(int _003C_003E1__state)
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

		[HideInInspector]
		public TimescaleModes TimescaleMode;

		protected CinemachineBrain _brain;

		protected CinemachineBlendDefinition _initialDefinition;

		protected Coroutine _coroutine;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void Awake()
		{
		}

		public virtual void OnMMCinemachinePriorityEvent(MMChannelData channelData, bool forceMaxPriority, int newPriority, bool forceTransition, CinemachineBlendDefinition blendDefinition, bool resetValuesAfterTransition, TimescaleModes timescaleMode, bool restore = false)
		{
		}

		[IteratorStateMachine(typeof(_003CResetBlendDefinition_003Ed__8))]
		protected virtual IEnumerator ResetBlendDefinition(float delay)
		{
			return null;
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
