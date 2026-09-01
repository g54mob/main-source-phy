using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class Strike : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CResetCountdown_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Strike _003C_003E4__this;

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
			public _003CResetCountdown_003Ed__36(int _003C_003E1__state)
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
		private sealed class _003CResetSceneCo_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Strike _003C_003E4__this;

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
			public _003CResetSceneCo_003Ed__37(int _003C_003E1__state)
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

		[Header("Input")]
		[Tooltip("a key to use to throw the ball")]
		public KeyCode ActionKey;

		[Tooltip("a secondary key to use to throw the ball")]
		public KeyCode ActionKeyAlt;

		[Header("Bindings")]
		[Tooltip("the rigidbody of the bowling ball")]
		public Rigidbody BowlingBallRb;

		[Tooltip("a collider used to count points (still standing pins will overlap with it)")]
		public Collider PointsCollider;

		[Tooltip("the rigidbody of the pins")]
		public List<Rigidbody> Pins;

		[Tooltip("the wiggler that makes the launcher rotate")]
		public MMWiggle BowlingBallLauncherWiggler;

		[Tooltip("the text component used to display the current last score")]
		public Text LastScoreText;

		[Tooltip("the text component used to display the total score")]
		public Text TotalScoreText;

		[Tooltip("the text component used to display the number of consecutive strikes")]
		public Text ConsecutiveStrikesText;

		[Tooltip("a list of elements to turn on/off in case of strike")]
		public List<GameObject> StrikeElements;

		[Header("Settings")]
		[Tooltip("the force to apply when throwing the ball")]
		public Vector3 ThrowingForce;

		[Tooltip("the gravity to apply")]
		public Vector3 Gravity;

		[Tooltip("the max duration before a reset")]
		public float MaxDurationBeforeReset;

		[Tooltip("the delay to wait for (in seconds) before resetting the scene")]
		public float DelayBeforeReset;

		[Tooltip("the delay to wait for (in seconds) while counting/displaying points")]
		public float DelayForPoints;

		[Header("Feedbacks")]
		[Tooltip("a feedback to call when throwing the ball")]
		public MMFeedbacks ThrowBallFeedback;

		[Tooltip("a feedback to call when resetting the scene")]
		public MMFeedbacks ResetFeedback;

		[Tooltip("a feedback played when hitting a strike")]
		public MMFeedbacks StrikeFeedback;

		[Tooltip("a feedback played when missing a strike")]
		public MMFeedbacks NoStrikeFeedback;

		[Header("Scores")]
		[Tooltip("the last score you hit")]
		[MMReadOnly]
		public int LastScore;

		[Tooltip("The total amount of points since the start")]
		[MMReadOnly]
		public int TotalPoints;

		[Tooltip("the amount of consecutive strikes")]
		[MMReadOnly]
		public int ConsecutiveStrikes;

		protected bool _ballThrown;

		protected Vector3 _initialBallPosition;

		protected Quaternion _initialBallRotation;

		protected List<StrikePin> _strikePins;

		protected List<Collider> _pinColliders;

		protected Coroutine _resetCoroutine;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void SetStrikeElements(bool status)
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void StartBall()
		{
		}

		public virtual void ThrowBall()
		{
		}

		protected void OnTriggerEnter(Collider other)
		{
		}

		[IteratorStateMachine(typeof(_003CResetCountdown_003Ed__36))]
		protected virtual IEnumerator ResetCountdown()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CResetSceneCo_003Ed__37))]
		protected virtual IEnumerator ResetSceneCo()
		{
			return null;
		}

		protected virtual void CountPoints()
		{
		}
	}
}
