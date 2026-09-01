using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AgentMover : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CMoveOnOffMeshLink_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AgentMover _003C_003E4__this;

		public bool reverseDirection;

		public Spline spline;

		private float _003CcurrentTime_003E5__2;

		private Vector3 _003CagentStartPosition_003E5__3;

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
		public _003CMoveOnOffMeshLink_003Ed__29(int _003C_003E1__state)
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
	private sealed class _003CPerformJumpRoutine_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AgentMover _003C_003E4__this;

		public NavMeshLink link;

		public Spline spline;

		private bool _003CreverseDirection_003E5__2;

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
		public _003CPerformJumpRoutine_003Ed__27(int _003C_003E1__state)
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
	private NavMeshAgent _Agent;

	private bool _onNavMeshLink;

	[SerializeField]
	private float _jumpDuration;

	[SerializeField]
	private float _jumpPreparationDelay;

	[SerializeField]
	private float _jumpFinishedDelay;

	public UnityEvent OnLand;

	public UnityEvent<string> OnStartJump;

	private Coroutine _jumpRoutine;

	private Coroutine _linkRoutine;

	private NavMeshPath _reusablePath;

	public bool IsOnNavMeshLink => false;

	public event Action<float> OnSpeedChanged
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Start()
	{
	}

	public void PauseMovement()
	{
	}

	public void ResumeMovement()
	{
	}

	public void EnableAgent(bool enable)
	{
	}

	public bool IsPathReachable(Vector3 target)
	{
		return false;
	}

	public void SetDestination(Vector3 destination)
	{
	}

	public void Teleport(Vector3 destination)
	{
	}

	public bool HasReachedDestination()
	{
		return false;
	}

	private void Update()
	{
	}

	private void StartNavMeshLinkMovement()
	{
	}

	public void StopNavMeshLinkMovement()
	{
	}

	private void PerformJump(NavMeshLink link, Spline spline)
	{
	}

	[IteratorStateMachine(typeof(_003CPerformJumpRoutine_003Ed__27))]
	private IEnumerator PerformJumpRoutine(NavMeshLink link, Spline spline)
	{
		return null;
	}

	private bool CheckIfJumpingFromEndToStart(NavMeshLink link)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CMoveOnOffMeshLink_003Ed__29))]
	private IEnumerator MoveOnOffMeshLink(Spline spline, bool reverseDirection)
	{
		return null;
	}

	private void FaceTarget(Vector3 target)
	{
	}
}
