using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class Barbarian : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CAttackCoroutine_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Barbarian _003C_003E4__this;

			private float _003CintervalDuration_003E5__2;

			private int _003CenemyCounter_003E5__3;

			private List<Vector3>.Enumerator _003C_003E7__wrap3;

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
			public _003CAttackCoroutine_003Ed__20(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[Header("Cooldown")]
		[Tooltip("a duration, in seconds, between two attacks, during which attacks are prevented")]
		public float CooldownDuration;

		[Header("Feedbacks")]
		[Tooltip("a feedback to call when the attack starts")]
		public MMFeedbacks AttackFeedback;

		[Tooltip("a feedback to call when each individual attack phase starts")]
		public MMFeedbacks IndividualAttackFeedback;

		[Tooltip("a feedback to call when trying to attack while in cooldown")]
		public MMFeedbacks DeniedFeedback;

		[Header("Attack settings")]
		public MMTween.MMTweenCurve AttackCurve;

		public float AttackDuration;

		public float AttackPositionOffset;

		public float IntervalDecrement;

		protected List<Vector3> _targets;

		protected float _lastAttackStartedAt;

		protected Vector3 _initialPosition;

		protected Vector3 _initialLookAtTarget;

		protected Vector3 _lookAtTarget;

		protected BarbarianEnemy _enemy;

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LookAtTarget()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void Attack()
		{
		}

		protected virtual void AcquireTargets()
		{
		}

		[IteratorStateMachine(typeof(_003CAttackCoroutine_003Ed__20))]
		protected virtual IEnumerator AttackCoroutine()
		{
			return null;
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
		}
	}
}
