using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class ImpactLocation : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CReportLocationNextFrame_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImpactLocation _003C_003E4__this;

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
		public _003CReportLocationNextFrame_003Ed__12(int _003C_003E1__state)
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

	[Header("Runtime Only")]
	public ShellDefinition shell;

	public bool TriggerNormalEvents;

	private static RectTransform _cachedRootCanvas;

	public GameObject[] ScoutingStrips;

	public bool DisableIfAnyEntityMatches;

	public FilterEntitySet EntityFilter;

	public bool ScoutingStripsBlocked { get; private set; }

	public void Init(ShellDefinition shell, bool triggerNormalEvents = true)
	{
	}

	private void SetScoutingStripsActive(bool active)
	{
	}

	[IteratorStateMachine(typeof(_003CReportLocationNextFrame_003Ed__12))]
	private IEnumerator ReportLocationNextFrame()
	{
		return null;
	}

	private void EvaluateAndReport()
	{
	}

	private RectTransform ResolveRootCanvasRect()
	{
		return null;
	}
}
