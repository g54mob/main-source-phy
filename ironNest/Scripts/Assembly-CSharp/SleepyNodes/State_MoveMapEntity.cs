using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SleepyNodes
{
	[CreateNodeMenu("Entity/Move Entity")]
	[NodeWidth(400)]
	[NodeName("Move Entity")]
	public class State_MoveMapEntity : StateNode
	{
		[CompilerGenerated]
		private sealed class _003CStateCheck_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public double endsAt;

			public MapEntity entity;

			public State_MoveMapEntity _003C_003E4__this;

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
			public _003CStateCheck_003Ed__12(int _003C_003E1__state)
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

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode To;

		public TargetSelection EntityToMove;

		public LocationSelection LocationToMoveTo;

		public bool ShouldUpdateState;

		public MapEntityStates StateToAdd;

		[Tooltip("0 = don't switch back automatically")]
		public float SecondsForState;

		[Tooltip("Should this node wait for the state to switch back before continuing")]
		public bool WaitForState;

		public bool UseSmoothMovement;

		[Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Override, backingValue = ShowBackingValue.Never)]
		public StateNode OnStateReset;

		public EntityContextKeys EntityStateReset;

		public override void ResetNode()
		{
		}

		public override void OnEnter(NodeExecutionState state)
		{
		}

		[IteratorStateMachine(typeof(_003CStateCheck_003Ed__12))]
		private IEnumerator StateCheck(MapEntity entity, double endsAt)
		{
			return null;
		}

		public override void OnExecute(NodeExecutionState state)
		{
		}
	}
}
