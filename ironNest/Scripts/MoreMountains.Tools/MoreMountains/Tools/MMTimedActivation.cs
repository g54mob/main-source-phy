using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMTimedActivation")]
	public class MMTimedActivation : MonoBehaviour
	{
		public enum TimedStatusChange
		{
			Enable = 0,
			Disable = 1,
			Destroy = 2
		}

		public enum ActivationModes
		{
			Awake = 0,
			Start = 1,
			OnEnable = 2,
			OnTriggerEnter = 3,
			OnTriggerExit = 4,
			OnTriggerEnter2D = 5,
			OnTriggerExit2D = 6,
			Script = 7
		}

		public enum TriggerModes
		{
			None = 0,
			Tag = 1,
			Layer = 2
		}

		public enum DelayModes
		{
			Time = 0,
			Frames = 1
		}

		[CompilerGenerated]
		private sealed class _003CTimedActivationSequence_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMTimedActivation _003C_003E4__this;

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
			public _003CTimedActivationSequence_003Ed__25(int _003C_003E1__state)
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

		[Header("Trigger Mode")]
		public ActivationModes ActivationMode;

		[MMEnumCondition("ActivationMode", new int[] { 3, 4 })]
		public TriggerModes TriggerMode;

		[MMEnumCondition("TriggerMode", new int[] { 2 })]
		public LayerMask TargetTriggerLayer;

		[MMEnumCondition("TriggerMode", new int[] { 1 })]
		public string TargetTriggerTag;

		[Header("Delay")]
		public DelayModes DelayMode;

		[MMEnumCondition("DelayMode", new int[] { 0 })]
		public float TimeBeforeStateChange;

		[MMEnumCondition("DelayMode", new int[] { 1 })]
		public int FrameCount;

		[Header("Timed Activation")]
		public List<GameObject> TargetGameObjects;

		public List<MonoBehaviour> TargetBehaviours;

		public TimedStatusChange TimeDestructionMode;

		[Header("Actions")]
		public UnityEvent TimedActions;

		protected virtual void Awake()
		{
		}

		public virtual void TriggerSequence()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnTriggerEnter(Collider collider)
		{
		}

		protected virtual void OnTriggerExit(Collider collider)
		{
		}

		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
		}

		protected virtual bool CorrectTagOrLayer(GameObject target)
		{
			return false;
		}

		protected virtual void StartChangeState()
		{
		}

		[IteratorStateMachine(typeof(_003CTimedActivationSequence_003Ed__25))]
		protected virtual IEnumerator TimedActivationSequence()
		{
			return null;
		}

		protected virtual void Activate()
		{
		}

		protected virtual void StateChange()
		{
		}
	}
}
