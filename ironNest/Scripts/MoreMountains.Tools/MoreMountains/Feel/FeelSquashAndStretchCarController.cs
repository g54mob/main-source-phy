using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSquashAndStretchCarController : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CTeleportSequence_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public FeelSquashAndStretchCarController _003C_003E4__this;

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
			public _003CTeleportSequence_003Ed__18(int _003C_003E1__state)
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

		[Header("Car Settings")]
		public float Speed;

		public float RotationSpeed;

		[Header("Bindings")]
		public Collider BoundaryCollider;

		public List<TrailRenderer> Trails;

		public MMFeedbacks TeleportFeedbacks;

		protected Vector2 _input;

		protected Vector3 _rotationAxis;

		protected const string _horizontalAxis = "Horizontal";

		protected const string _verticalAxis = "Vertical";

		protected Bounds _bounds;

		protected Vector3 _thisPosition;

		protected Vector3 _newPosition;

		protected float _trailTime;

		protected virtual void Start()
		{
		}

		protected virtual void HandleInput()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void MoveCar()
		{
		}

		protected virtual void HandleBounds()
		{
		}

		[IteratorStateMachine(typeof(_003CTeleportSequence_003Ed__18))]
		protected virtual IEnumerator TeleportSequence()
		{
			return null;
		}

		protected virtual void SetTrails(bool status)
		{
		}
	}
}
