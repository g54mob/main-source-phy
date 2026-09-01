using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Movement/MMPath")]
	public class MMPath : MonoBehaviour
	{
		public enum CycleOptions
		{
			BackAndForth = 0,
			Loop = 1,
			OnlyOnce = 2
		}

		public enum MovementDirection
		{
			Ascending = 0,
			Descending = 1
		}

		[Serializable]
		public struct Data
		{
			public Vector3 Center;

			public Vector3[] Offsets;

			public float Delay;

			public CycleOptions Cycle;

			public MovementDirection Direction;

			public static Data ForwardLoopingPath(Vector3 ctr, Vector3[] vtx, float wait)
			{
				return default(Data);
			}

			public static Data ForwardBackAndForthPath(Vector3 ctr, Vector3[] vtx, float wait)
			{
				return default(Data);
			}

			public static Data ForwardOnlyOncePath(Vector3 ctr, Vector3[] vtx, float wait)
			{
				return default(Data);
			}
		}

		[CompilerGenerated]
		private sealed class _003CGetPathEnumerator_003Ed__42 : IEnumerator<Vector3>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private Vector3 _003C_003E2__current;

			public MMPath _003C_003E4__this;

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
			public _003CGetPathEnumerator_003Ed__42(int _003C_003E1__state)
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
		public MovementDirection LoopInitialMovementDirection;

		public List<MMPathMovementElement> PathElements;

		public MMPath ReferenceMMPath;

		public bool AbsoluteReferencePath;

		public float MinDistanceToGoal;

		[Header("Gizmos")]
		public bool LockHandlesOnXAxis;

		public bool LockHandlesOnYAxis;

		public bool LockHandlesOnZAxis;

		protected Vector3 _originalTransformPosition;

		protected bool _originalTransformPositionStatus;

		protected bool _active;

		protected IEnumerator<Vector3> _currentPoint;

		protected int _direction;

		protected Vector3 _initialPosition;

		protected Vector3 _initialPositionThisFrame;

		protected Vector3 _finalPosition;

		protected Vector3 _previousPoint;

		protected int _currentIndex;

		protected float _distanceToNextPoint;

		protected bool _endReached;

		public bool EndReached => false;

		public virtual bool CanMove { get; set; }

		public virtual bool Initialized { get; set; }

		public virtual int Direction => 0;

		protected virtual void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		public int CurrentIndex()
		{
			return 0;
		}

		public Vector3 CurrentPoint()
		{
			return default(Vector3);
		}

		public Vector3 CurrentPositionRelative()
		{
			return default(Vector3);
		}

		protected virtual void Update()
		{
		}

		protected virtual void ComputePath()
		{
		}

		[IteratorStateMachine(typeof(_003CGetPathEnumerator_003Ed__42))]
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

		public void SetPath(in Data configuration)
		{
		}
	}
}
