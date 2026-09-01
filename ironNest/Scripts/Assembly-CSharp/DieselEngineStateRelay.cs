using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Gameplay/Diesel Engine State Relay")]
public class DieselEngineStateRelay : MonoBehaviour
{
	public enum OnEnableTrigger
	{
		None = 0,
		ForceOn = 1,
		ForceOff = 2
	}

	[CompilerGenerated]
	private sealed class _003CClearForceFlagsNextFrame_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DieselEngineStateRelay _003C_003E4__this;

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
		public _003CClearForceFlagsNextFrame_003Ed__21(int _003C_003E1__state)
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

	[Header("Target")]
	[Tooltip("Unity tag used to locate the GameObject that has a DieselEngineController.\nThe tag must exist in Edit → Project Settings → Tags & Layers.\nDefault: DieselEngine")]
	[SerializeField]
	private string engineTag;

	[Tooltip("If the engine GameObject is not found on Enable, how many seconds to wait\nbefore retrying. Set to 0 to disable retries.\nUseful when the relay scene is loaded before the engine scene has finished loading.")]
	[SerializeField]
	[Min(0f)]
	private float retrySearchInterval;

	[Header("On Enable Behaviour")]
	[Tooltip("Command to execute automatically when this component is enabled.\n\nNone     — Do nothing on enable; rely entirely on UnityEvents / direct calls.\nForceOn  — Immediately call ForceEngineOn() when enabled.\nForceOff — Immediately call ForceEngineOff() when enabled.")]
	[SerializeField]
	private OnEnableTrigger triggerOnEnable;

	[Header("Relay Events")]
	[Tooltip("Fired after ForceEngineOn() is dispatched to the controller.\nWire up audio cues, UI changes, or other scene reactions here.")]
	[SerializeField]
	private UnityEvent OnRelayEngineOn;

	[Tooltip("Fired after ForceEngineOff() is dispatched to the controller.\nWire up audio cues, UI changes, or other scene reactions here.")]
	[SerializeField]
	private UnityEvent OnRelayEngineOff;

	[Tooltip("Fired when the engine controller cannot be found (tag search failed).\nWire up an error indicator or retry logic here.")]
	[SerializeField]
	private UnityEvent OnEngineNotFound;

	[Header("Debug")]
	[Tooltip("Log relay activity to the Console.")]
	[SerializeField]
	private bool debugLog;

	private DieselEngineController _engine;

	private FieldInfo _fieldForceOn;

	private FieldInfo _fieldForceOff;

	private float _retryTimer;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	public void ForceEngineOn()
	{
	}

	public void ForceEngineOff()
	{
	}

	public void ToggleEngine()
	{
	}

	public void RefreshEngineReference()
	{
	}

	private void TryFindEngine()
	{
	}

	private bool EnsureEngine()
	{
		return false;
	}

	private void SetForceFields(bool forceOn, bool forceOff)
	{
	}

	[IteratorStateMachine(typeof(_003CClearForceFlagsNextFrame_003Ed__21))]
	private IEnumerator ClearForceFlagsNextFrame()
	{
		return null;
	}
}
