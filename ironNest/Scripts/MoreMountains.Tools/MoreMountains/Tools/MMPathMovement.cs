using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMPathMovement")]
	public class MMPathMovement : MonoBehaviour
	{
		public enum PossibleAccelerationType
		{
			ConstantSpeed = 0,
			EaseOut = 1,
			AnimationCurve = 2
		}

		public enum CycleOptions
		{
			BackAndForth = 0,
			Loop = 1,
			OnlyOnce = 2,
			StopAtBounds = 3,
			Random = 4
		}

		public enum MovementDirection
		{
			Ascending = 0,
			Descending = 1
		}

		public enum UpdateModes
		{
			Update = 0,
			FixedUpdate = 1,
			LateUpdate = 2
		}

		public enum AlignmentModes
		{
			None = 0,
			ThisRotation = 1,
			ParentRotation = 2
		}

		[CompilerGenerated]
		private sealed class _003CGetPathEnumerator_003Ed__48 : IEnumerator<Vector3>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Vector3 _003C_003E2__current;

			public MMPathMovement _003C_003E4__this;

			private int _003Cindex_003E5__2;

			Vector3 IEnumerator<Vector3>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(Vector3);
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
			public _003CGetPathEnumerator_003Ed__48(int _003C_003E1__state)
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

		[Header("Path")]
		[MMInformation("Here you can select the '<b>Cycle Option</b>'. Back and Forth will have your object follow the path until its end, and go back to the original point. If you select Loop, the path will be closed and the object will move along it until told otherwise. If you select Only Once, the object will move along the path from the first to the last point, and remain there forever.", MMInformationAttribute.InformationType.Info, false)]
		public CycleOptions CycleOption;

		[MMInformation("Add points to the <b>Path</b> (set the size of the path first), then position the points using either the inspector or by moving the handles directly in scene view. For each path element you can specify a delay (in seconds). The order of the points will be the order the object follows.\nFor looping paths, you can then decide if the object will go through the points in the Path in Ascending (1, 2, 3...) or Descending (Last, Last-1, Last-2...) order.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the initial movement direction : ascending > will go from the points 0 to 1, 2, etc ; descending > will go from the last point to last-1, last-2, etc")]
		public MovementDirection LoopInitialMovementDirection;

		[Tooltip("the points that make up the path the object will follow")]
		public List<MMPathMovementElement> PathElements;

		[Header("Path Alignment")]
		[Tooltip("whether to align the path on nothing, this object's rotation, or this object's parent's rotation")]
		public AlignmentModes AlignmentMode;

		[Header("Movement")]
		[MMInformation("Set the <b>speed</b> at which the path will be crawled, and if the movement should be constant or eased.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the movement speed")]
		public float MovementSpeed;

		[Tooltip("the movement type of the object")]
		public PossibleAccelerationType AccelerationType;

		[Tooltip("the acceleration to apply to an object traveling between two points of the path.")]
		public AnimationCurve Acceleration;

		[Tooltip("the chosen update mode (update, fixed update, late update)")]
		public UpdateModes UpdateMode;

		[Header("Settings")]
		[MMInformation("The <b>MinDistanceToGoal</b> is used to check if we've (almost) reached a point in the Path. The 2 other settings here are for debug only, don't change them.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the minimum distance to a point at which we'll arbitrarily decide the point's been reached")]
		public float MinDistanceToGoal;

		[Tooltip("the original position of the transform, hidden and shouldn't be accessed")]
		protected Vector3 _originalTransformPosition;

		protected bool _originalTransformPositionStatus;

		protected bool _active;

		protected IEnumerator<Vector3> _currentPoint;

		protected int _direction;

		protected Vector3 _initialPosition;

		protected Vector3 _finalPosition;

		protected Vector3 _previousPoint;

		protected float _waiting;

		protected int _currentIndex;

		protected float _distanceToNextPoint;

		protected bool _endReached;

		protected Vector3 _positionLastFrame;

		protected Vector3 _vector3Zero;

		public virtual Vector3 CurrentSpeed { get; protected set; }

		public virtual bool CanMove { get; set; }

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void ResetPath()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void FixedUpdate()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void PointReached()
		{
		}

		protected virtual void EndReached()
		{
		}

		protected virtual void ExecuteUpdate()
		{
		}

		protected virtual void Move()
		{
		}

		public virtual void MoveAlongThePath()
		{
		}

		[IteratorStateMachine(typeof(_003CGetPathEnumerator_003Ed__48))]
		public virtual IEnumerator<Vector3> GetPathEnumerator()
		{
			return null;
		}

		public virtual void ChangeDirection()
		{
		}

		protected virtual void OnDrawGizmos()
		{
		}

		public virtual Vector3 PointPosition(int index)
		{
			return default(Vector3);
		}

		public virtual Vector3 PointPosition(Vector3 relativePointPosition)
		{
			return default(Vector3);
		}

		public virtual void UpdateOriginalTransformPosition(Vector3 newOriginalTransformPosition)
		{
		}

		public virtual Vector3 GetOriginalTransformPosition()
		{
			return default(Vector3);
		}

		public virtual void SetOriginalTransformPositionStatus(bool status)
		{
		}

		public virtual bool GetOriginalTransformPositionStatus()
		{
			return false;
		}
	}
}
