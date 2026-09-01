using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Utilities/Tag Switcher")]
public class TagSwitcher : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDoChangeTagAfterDelay_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public TagSwitcher _003C_003E4__this;

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
		public _003CDoChangeTagAfterDelay_003Ed__13(int _003C_003E1__state)
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

	[Tooltip("The GameObject whose tag will be changed.\n- If left empty, this component's own GameObject is used.\nPrefab/Scene Safe: If the reference is missing at runtime, 'self' is used.")]
	[SerializeField]
	private GameObject target;

	[Tooltip("The exact Unity Tag to apply.\n- Must already exist in Project Settings > Tags and Layers, or be \"Untagged\".\nTokens/Codes: None supported (provide a literal tag only).\nRules:\n  • Case-sensitive.\n  • Undefined tags will cause a fallback or a warning (see 'Fallback To Untagged On Invalid Tag').\nExamples:\n  • Player\n  • Untagged")]
	[SerializeField]
	private string newTag;

	[Tooltip("Time in seconds to wait before changing the tag.\n- Must be >= 0.0\nExamples:\n  • 0   (immediate)\n  • 1.5 (wait one and a half seconds)")]
	[Min(0f)]
	[SerializeField]
	private float delaySeconds;

	[Tooltip("If enabled, the countdown starts automatically in Awake().\n- When true, the component behaves as if Trigger() was called during Awake().")]
	[SerializeField]
	private bool triggerOnAwake;

	[Tooltip("If enabled and 'New Tag' is not defined in the project's Tag Manager, the tag will be set to 'Untagged' instead.\n- If disabled, an invalid tag will be skipped and a warning will be logged.")]
	[SerializeField]
	private bool fallbackToUntaggedOnInvalidTag;

	private Coroutine _pending;

	private void Awake()
	{
	}

	public void Trigger()
	{
	}

	public void TriggerWithDelay(float delay)
	{
	}

	public void Cancel()
	{
	}

	[ContextMenu("Trigger Now (use configured delay)")]
	private void ContextTrigger()
	{
	}

	[ContextMenu("Cancel Pending")]
	private void ContextCancel()
	{
	}

	private void StartOrRestartCountdown(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDoChangeTagAfterDelay_003Ed__13))]
	private IEnumerator DoChangeTagAfterDelay(float delay)
	{
		return null;
	}

	private void ApplyTagSafely()
	{
	}

	private bool TrySetTag(GameObject go, string tagValue)
	{
		return false;
	}
}
