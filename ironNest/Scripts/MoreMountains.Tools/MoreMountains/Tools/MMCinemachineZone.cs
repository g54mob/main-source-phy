using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[AddComponentMenu(null)]
	[ExecuteAlways]
	public abstract class MMCinemachineZone : MonoBehaviour
	{
		public enum Modes
		{
			Enable = 0,
			Priority = 1
		}

		[CompilerGenerated]
		private sealed class _003CEnableCamera_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMCinemachineZone _003C_003E4__this;

			public int frames;

			public bool state;

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
			public _003CEnableCamera_003Ed__28(int _003C_003E1__state)
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

		[Header("Virtual Camera")]
		[Tooltip("whether to enable/disable virtual cameras, or to play on their priority for transitions")]
		public Modes Mode;

		[Tooltip("whether or not the camera in this zone should start active")]
		public bool CameraStartsActive;

		[Tooltip("the virtual camera associated to this zone (will try to grab one in children if none is set)")]
		public CinemachineCamera VirtualCamera;

		[Tooltip("when in priority mode, the priority this camera should have when the zone is active")]
		[MMEnumCondition("Mode", new int[] { 1 })]
		public int EnabledPriority;

		[Tooltip("when in priority mode, the priority this camera should have when the zone is inactive")]
		[MMEnumCondition("Mode", new int[] { 1 })]
		public int DisabledPriority;

		[Header("Collisions")]
		[Tooltip("a layermask containing all the layers that should activate this zone")]
		public LayerMask TriggerMask;

		[Header("Confiner Setup")]
		[Tooltip("whether or not the zone should auto setup its camera's confiner on start - alternative is to manually click the ManualSetupConfiner, or do your own setup")]
		public bool SetupConfinerOnStart;

		[MMInspectorButton("ManualSetupConfiner")]
		public bool GenerateConfinerSetup;

		[Header("State")]
		[Tooltip("whether this room is the current room or not")]
		[MMReadOnly]
		public bool CurrentRoom;

		[Tooltip("whether this room has already been visited or not")]
		public bool RoomVisited;

		[Header("Events")]
		[Tooltip("a UnityEvent to trigger when entering the zone for the first time")]
		public UnityEvent OnEnterZoneForTheFirstTimeEvent;

		[Tooltip("a UnityEvent to trigger when entering the zone")]
		public UnityEvent OnEnterZoneEvent;

		[Tooltip("a UnityEvent to trigger when exiting the zone")]
		public UnityEvent OnExitZoneEvent;

		[Header("Activation")]
		[Tooltip("a list of gameobjects to enable when entering the zone, and disable when exiting it")]
		public List<GameObject> ActivationList;

		[Header("Debug")]
		[Tooltip("whether or not to draw shape gizmos to help visualize the zone's bounds")]
		public bool DrawGizmos;

		[Tooltip("the color of the gizmos to draw in edit mode")]
		public Color GizmosColor;

		protected GameObject _confinerGameObject;

		protected Vector3 _gizmoSize;

		protected virtual void Awake()
		{
		}

		protected virtual void AlwaysInitialization()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Start()
		{
		}

		protected abstract void InitializeCollider();

		protected abstract void SetupConfiner();

		protected virtual void ManualSetupConfiner()
		{
		}

		protected virtual void SetupConfinerGameObject()
		{
		}

		protected virtual bool TestCollidingGameObject(GameObject collider)
		{
			return false;
		}

		[IteratorStateMachine(typeof(_003CEnableCamera_003Ed__28))]
		protected virtual IEnumerator EnableCamera(bool state, int frames)
		{
			return null;
		}

		protected virtual void EnterZone()
		{
		}

		protected virtual void ExitZone()
		{
		}

		protected virtual void Reset()
		{
		}
	}
}
