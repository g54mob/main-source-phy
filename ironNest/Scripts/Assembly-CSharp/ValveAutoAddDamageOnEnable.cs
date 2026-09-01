using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class ValveAutoAddDamageOnEnable : MonoBehaviour
{
	public enum TargetScope
	{
		SpecificSystem = 0,
		AnySystem = 1
	}

	[CompilerGenerated]
	private sealed class _003CBurstRoutine_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ValveAutoAddDamageOnEnable _003C_003E4__this;

		public bool ignoreProbability;

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
		public _003CBurstRoutine_003Ed__19(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CWaitForAnySystemPool_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ValveAutoAddDamageOnEnable _003C_003E4__this;

		public float waitStart;

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
		public _003CWaitForAnySystemPool_003Ed__21(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CWaitForSpecificSystemPool_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ValveAutoAddDamageOnEnable _003C_003E4__this;

		public float waitStart;

		private HighPressureSystemManager _003Cmgr_003E5__2;

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
		public _003CWaitForSpecificSystemPool_003Ed__22(int _003C_003E1__state)
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

	[Header("Targeting")]
	[Tooltip("Optional explicit reference to the HighPressureSystemManager to target. If set, System Id is ignored.\nIf left empty the component will search for a manager using the System Id field\n(only applies when Target Scope = SpecificSystem).")]
	[SerializeField]
	private HighPressureSystemManager explicitManager;

	[Tooltip("System ID to target when Explicit Manager is not set and Target Scope = SpecificSystem. Case-sensitive.\nMust match HighPressureSystemManager.SystemId exactly.\nExamples: \"Default\", \"ReactorA\".\nIf left empty and Target Scope = SpecificSystem the attempt will fail unless Explicit Manager is assigned.")]
	[SerializeField]
	private string systemId;

	[Header("Scope")]
	[Tooltip("Which scope to search when picking random valves:\n- SpecificSystem: pick valves only from the specified system (Explicit Manager OR System Id).\n- AnySystem: pick valves from across ALL registered systems (globally blocked systems are skipped).")]
	[SerializeField]
	private TargetScope targetScope;

	[Header("Burst Count")]
	[Tooltip("Minimum number of distinct valves to affect in a single burst (inclusive).\nMust be >= 1. Set both Min and Max to 1 to reproduce the original single-valve behaviour.\nIf the available valve pool is smaller than the sampled count it is automatically clamped\nto the pool size — no burst will be aborted due to an insufficient pool.\nExample: Min=1, Max=5 will affect between one and five valves at random.")]
	[SerializeField]
	[Min(1f)]
	private int burstCountMin;

	[Tooltip("Maximum number of distinct valves to affect in a single burst (inclusive).\nMust be >= Burst Count Min. If set equal to Burst Count Min the count is fixed.\nClamped to the available pool size at runtime so large values are safe to use.\nExample: Min=3, Max=3 will always affect exactly three distinct valves (or all valves\nin the pool if fewer than three are available).")]
	[SerializeField]
	[Min(1f)]
	private int burstCountMax;

	[Header("Probability")]
	[Tooltip("Chance [0..1] that the burst will fire when this component is enabled.\n0 = never, 1 = always. Applies once per trigger attempt — not per valve.\nExample: 0.25 for a 25% chance.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float probability;

	[Header("Timing")]
	[Tooltip("Delay in seconds after OnEnable before performing the probability check.\nUseful to allow other systems to initialise. Set to 0 for immediate attempt.")]
	[SerializeField]
	[Min(0f)]
	private float delaySeconds;

	[Tooltip("How long (seconds) the component will wait for target manager(s) or valves to appear before giving up.\nUseful for cross-scene triggers. Set to 0 to only try once immediately.")]
	[SerializeField]
	[Min(0f)]
	private float waitForManagerSeconds;

	[Tooltip("When waiting for managers/valves, how often (seconds) to poll for them.\nLower values find targets sooner but cost more checks. Default: 0.25 seconds.")]
	[SerializeField]
	[Min(0.01f)]
	private float managerPollInterval;

	[Header("Add Damage")]
	[Tooltip("Minimum damage amount to ADD to each chosen valve (0..1). This is the per-valve delta minimum.\nEach valve in the burst receives its own independent roll in [Add Damage Min, Add Damage Max].\nExample: 0.1 adds at least 10% damage.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float addDamageMin;

	[Tooltip("Maximum damage amount to ADD to each chosen valve (0..1). The actual per-valve delta is sampled\nuniformly and independently from [Add Damage Min, Add Damage Max].\nExample: Min=0.1, Max=0.5 -> each valve in the burst gains between 10% and 50% damage independently.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float addDamageMax;

	[Header("Behaviour")]
	[Tooltip("If true, the component will only attempt once across its lifetime (first trigger). If false it will attempt on every OnEnable / TriggerNow call.")]
	[SerializeField]
	private bool onlyOnce;

	[Tooltip("If true, logs attempts and outcomes to the Console. Useful for designers. Disable in production.")]
	[SerializeField]
	private bool logAttempts;

	[Header("Blocker Handling")]
	[Tooltip("If true the component will abort if ValveBreakBlocker.IsBlocked is true, and will also skip/abort\nfor systems blocked via ValveBreakBlocker.IsSystemBlocked(systemId).\nIf false the component will proceed even when a global or per-system block is active (use with caution).")]
	[SerializeField]
	private bool respectGlobalBlocker;

	private bool alreadyTriggered;

	private List<ValveController> _pendingPool;

	private void OnEnable()
	{
	}

	public void TriggerNow()
	{
	}

	public void TriggerNowIgnoreProbability()
	{
	}

	[IteratorStateMachine(typeof(_003CBurstRoutine_003Ed__19))]
	private IEnumerator BurstRoutine(bool ignoreProbability)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CWaitForAnySystemPool_003Ed__21))]
	private IEnumerator WaitForAnySystemPool(float waitStart)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CWaitForSpecificSystemPool_003Ed__22))]
	private IEnumerator WaitForSpecificSystemPool(float waitStart)
	{
		return null;
	}

	private static void ShufflePartial(List<ValveController> list, int count)
	{
	}

	private static string TryGetValveSystemId(ValveController valve)
	{
		return null;
	}

	private void MarkTriggeredIfOnce()
	{
	}
}
