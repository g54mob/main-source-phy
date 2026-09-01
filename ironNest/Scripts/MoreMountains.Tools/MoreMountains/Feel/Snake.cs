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
	public class Snake : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CEatCo_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Snake _003C_003E4__this;

			private int _003Ctotal_003E5__2;

			private float _003Cpart_003E5__3;

			private int _003Ci_003E5__4;

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
			public _003CEatCo_003Ed__32(int _003C_003E1__state)
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
		private sealed class _003CTeleportCo_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Snake _003C_003E4__this;

			private int _003Ctotal_003E5__2;

			private float _003Cpart_003E5__3;

			private int _003Ci_003E5__4;

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
			public _003CTeleportCo_003Ed__30(int _003C_003E1__state)
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

		[Header("Movement")]
		public float Speed;

		public float NormalSpeedMultiplier;

		public float SpeedChangeRate;

		public Vector3 Direction;

		[Header("Boost")]
		public float BoostMultiplier;

		public float BoostDuration;

		[Header("BodyParts")]
		public SnakeBodyPart BodyPartPrefab;

		public int BodyPartsOffset;

		public int MaxAmountOfBodyParts;

		public float MinTimeBetweenLostParts;

		[Header("Bindings")]
		public Text PointsCounter;

		[Header("Feedbacks")]
		public MMFeedbacks TurnFeedback;

		public MMFeedbacks TeleportFeedback;

		public MMFeedbacks TeleportOnceFeedback;

		public MMFeedbacks EatFeedback;

		public MMFeedbacks LoseFeedback;

		[Header("Debug")]
		[MMReadOnly]
		public int SnakePoints;

		[MMReadOnly]
		public float _speed;

		[MMReadOnly]
		public float _speedMultiplier;

		[MMReadOnly]
		public float _lastFoodEatenAt;

		protected Vector3 _newPosition;

		protected MMPositionRecorder _recorder;

		public List<SnakeBodyPart> _snakeBodyParts;

		protected float _lastLostPart;

		protected void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void HandleMovement()
		{
		}

		public virtual void Turn()
		{
		}

		public virtual void Teleport()
		{
		}

		[IteratorStateMachine(typeof(_003CTeleportCo_003Ed__30))]
		protected virtual IEnumerator TeleportCo()
		{
			return null;
		}

		public virtual void Eat()
		{
		}

		[IteratorStateMachine(typeof(_003CEatCo_003Ed__32))]
		protected virtual IEnumerator EatCo()
		{
			return null;
		}

		public virtual void EatEffect()
		{
		}

		public virtual void Lose(SnakeBodyPart part)
		{
		}
	}
}
