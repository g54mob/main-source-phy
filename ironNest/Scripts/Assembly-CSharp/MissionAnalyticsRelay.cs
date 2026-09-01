using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SleepyNodes;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(0)]
public class MissionAnalyticsRelay : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCoRetrySubscribe_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MissionAnalyticsRelay _003C_003E4__this;

		private float _003Cdeadline_003E5__2;

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
		public _003CCoRetrySubscribe_003Ed__10(int _003C_003E1__state)
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

	[Header("Debug")]
	[Tooltip("Enable console logs during integration. Safe to disable in production.")]
	[SerializeField]
	private bool enableDebugLogs;

	private MissionManager _mm;

	private bool _subscribed;

	private string _activeMissionID;

	private float _missionStartTime;

	private bool _completionPending;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	private void OnDisable()
	{
	}

	[IteratorStateMachine(typeof(_003CCoRetrySubscribe_003Ed__10))]
	private IEnumerator CoRetrySubscribe()
	{
		return null;
	}

	private void TrySubscribe()
	{
	}

	private void Unsubscribe()
	{
	}

	private void OnMissionChanging(MissionGraph oldMission, MissionGraph newMission)
	{
	}

	private void OnMissionChanged(MissionGraph oldMission, MissionGraph newMission)
	{
	}

	private void OnSceneUnloaded(Scene scene)
	{
	}

	private void SendEvent(string eventName, string missionId)
	{
	}

	private void ClearActiveSession()
	{
	}

	private string SanitizeName(string raw)
	{
		return null;
	}

	private void TrySendAnalyticsEvent(string eventName, IDictionary<string, object> parameters)
	{
	}
}
