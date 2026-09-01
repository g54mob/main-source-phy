using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class FpsController : ImmediateModeShapeDrawer
	{
		[CompilerGenerated]
		private sealed class _003CFixedSteps_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public FpsController _003C_003E4__this;

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
			public _003CFixedSteps_003Ed__23(int _003C_003E1__state)
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

		public Transform head;

		public Camera cam;

		public Crosshair crosshair;

		public ChargeBar chargeBar;

		public AmmoBar ammoBar;

		public Compass compass;

		public Transform crosshairTransform;

		[Header("Player Movement")]
		[Range(0.8f, 1f)]
		public float smoof;

		public float moveSpeed;

		public float lookSensitivity;

		private float yaw;

		private float pitch;

		private Vector2 moveInput;

		private Vector3 moveVel;

		[Header("Sidebar Style")]
		[Range(0f, (float)Math.PI)]
		public float ammoBarAngularSpanRad;

		[Range(0f, 0.05f)]
		public float ammoBarOutlineThickness;

		[Range(0f, 0.2f)]
		public float ammoBarThickness;

		[Range(0f, 0.2f)]
		public float ammoBarRadius;

		[Header("Animation")]
		[Range(0f, 0.3f)]
		public float fireSidebarRadiusPunchAmount;

		public AnimationCurve shakeAnimX;

		public AnimationCurve shakeAnimY;

		private bool InputFocus
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		public override void DrawShapes(Camera cam)
		{
		}

		[IteratorStateMachine(typeof(_003CFixedSteps_003Ed__23))]
		private IEnumerator FixedSteps()
		{
			return null;
		}

		public static void DrawRoundedArcOutline(Vector2 origin, float radius, float thickness, float outlineThickness, float angStart, float angEnd)
		{
		}

		public Vector2 GetShake(float speed, float amp)
		{
			return default(Vector2);
		}

		private void FixedUpdateManual()
		{
		}

		private void Update()
		{
		}
	}
}
