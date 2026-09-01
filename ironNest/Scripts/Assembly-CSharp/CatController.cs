using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CatController : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayedNavmeshPositionSet_003Ed__39 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CatController _003C_003E4__this;

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
		public _003CDelayedNavmeshPositionSet_003Ed__39(int _003C_003E1__state)
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
	private sealed class _003CResumeActivities_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CatController _003C_003E4__this;

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
		public _003CResumeActivities_003Ed__38(int _003C_003E1__state)
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
	private AgentMover _movement;

	[SerializeField]
	private AgentAnimation _agentAnimation;

	[SerializeField]
	private Rigidbody _rigidbody;

	private CatMovementManager _manager;

	[SerializeField]
	private string _pauseAnimationTrigger;

	[SerializeField]
	private float _dropDistance;

	[SerializeField]
	private float _pauseTimeAfterDrop;

	[SerializeField]
	private float activityTimeInPlace;

	[SerializeField]
	private string loopEndTrigger;

	public UnityEvent onPickedUp;

	public UnityEvent onReleased;

	public UnityEvent onShooed;

	public UnityEvent onPetted;

	private CatState _currentState;

	private CatState _previousState;

	private float _activityTimer;

	private float _activityLocationTimer;

	private float _currentActivityDuration;

	private float _afterLoopActivityDuration;

	private bool _isLoopingActivity;

	private Coroutine _resumeRoutine;

	private bool selectClosestPoint;

	private bool selectFurtherPoint;

	public bool RecoveryState;

	private GameObject _cachedPlayer;

	public CatState CurrentState => default(CatState);

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void HandleIdleState()
	{
	}

	private void HandleWalkingState()
	{
	}

	private void PickRandomActivity()
	{
	}

	private void HandleActivityState()
	{
	}

	public void PauseBehavior(bool pause)
	{
	}

	public void StartCustomization()
	{
	}

	public void StopCustomization()
	{
	}

	public void StartCarrying()
	{
	}

	public void StopCarrying()
	{
	}

	[IteratorStateMachine(typeof(_003CResumeActivities_003Ed__38))]
	private IEnumerator ResumeActivities()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayedNavmeshPositionSet_003Ed__39))]
	private IEnumerator DelayedNavmeshPositionSet()
	{
		return null;
	}

	public void ShooCat(bool initiatedByPlayer)
	{
	}

	public void PetTheCat()
	{
	}

	public void InterruptCat()
	{
	}
}
