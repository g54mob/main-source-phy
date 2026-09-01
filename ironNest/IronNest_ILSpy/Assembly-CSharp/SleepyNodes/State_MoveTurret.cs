using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_MoveTurret : StateNode
{
	private sealed class _003CStateCheck_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public State_MoveTurret _003C_003E4__this;

		private StateNode _003Cnode_003E5__2;

		private TurretController _003CturretController_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStateCheck_003Ed__8(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_00f6: Expected I4, but got I8
			//IL_01a0: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					StateNode connectedNode = _003C_003E4__this.GetConnectedNode<StateNode>("OnFinishedMoving");
					_003Cnode_003E5__2 = connectedNode;
					if (_003Cnode_003E5__2 != null)
					{
						TurretController turretController = UnityEngine.Object.FindFirstObjectByType<TurretController>();
						_003CturretController_003E5__3 = turretController;
						if (!(_003CturretController_003E5__3 == null))
						{
							goto IL_013d;
						}
					}
					goto IL_00bf;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_00bf;
				}
				_003C_003E1__state = -1;
				if ((object)_003CturretController_003E5__3 != null)
				{
					if (_003CturretController_003E5__3.IsMoving)
					{
						goto IL_013d;
					}
					NodeExecutionState newState = NodeExecutionState.NewState;
					if ((object)_003Cnode_003E5__2 != null)
					{
						_003Cnode_003E5__2.OnEnter(newState);
						return false;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_00bf:
			return false;
			IL_013d:
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public StateNode To;

	public LocationSelection LocationToMoveTo = new LocationSelection
	{
		LocationType = LocationSelection.LocationTypes.Relative,
		RelativeTo = LocationSelection.RelativeReferenceTypes.Self,
		RelativeDirection = LocationSelection.RelativeDirections.BearingDistance
	};

	public bool WaitTillMovementComplete;

	public StateNode OnStartedMoving;

	public StateNode OnFinishedMoving;

	public StateNode OnAttemptMoveOffMap;

	public override void ResetNode()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_00de: Expected O, but got F4
		//IL_012c: Expected I, but got O
		//IL_0134: Expected I, but got O
		//IL_0144: Expected O, but got I
		//IL_01c4: Expected O, but got I4
		//IL_0180: Expected O, but got I
		//IL_01f0: Expected O, but got Ref
		//IL_01b6: Expected O, but got I4
		base.OnEnter(state);
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null))
		{
			return;
		}
		TurretController turretController = UnityEngine.Object.FindFirstObjectByType<TurretController>();
		if (!(turretController != null) || !(FireMission._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		Vector3[] array = new Vector3[4];
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		rectTransform.GetWorldCorners(array);
		MapEntity mapEntity = new MapEntity();
		Vector3 localPosition = turretController.turretBase.localPosition;
		mapEntity.Position = (Vector3)localPosition.x;
		_ = localPosition.z;
		NodeGraph nodeGraph = graph;
		if ((object)graph == null)
		{
			goto IL_031c;
		}
		nint num = (nint)typeof(MissionGraph);
		nint num2 = (nint)nodeGraph;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ r8_v28 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v539 @ r9_v14 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ r8_v28 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
		object obj3;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v539 @ r9_v14 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v586 @ rax_v57+FFFFFFF8+v540 @ rax_v52*8]");
			if (0 == (nint)typeof(MissionGraph))
			{
				obj3 = 1;
				goto IL_034f;
			}
		}
		obj3 = 0;
		goto IL_034f;
		IL_0367:
		LocationSelection locationToMoveTo = LocationToMoveTo;
		if (locationToMoveTo.DidClampToMap)
		{
			StateNode connectedNode = GetConnectedNode<StateNode>("OnAttemptMoveOffMap");
			if (connectedNode != null)
			{
				NodeExecutionState newState = NodeExecutionState.NewState;
				connectedNode.OnEnter(newState);
			}
		}
		_003CStateCheck_003Ed__8 obj4 = new _003CStateCheck_003Ed__8(0);
		obj4._003C_003E1__state = 0;
		obj4._003C_003E4__this = this;
		Coroutine coroutine = FireMission._003CInstance_003Ek__BackingField.StartCoroutine(obj4);
		return;
		IL_031c:
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] gridBounds = default(Vector3[]);
		GridReference gridReference = LocationToMoveTo.Resolve(FireMission._003CInstance_003Ek__BackingField, mapEntity, state, missionGraph, gridBounds);
		LocationSelection locationToMoveTo2 = LocationToMoveTo;
		Vector3 location = gridReference.GetLocation(array, locationToMoveTo2.FuzzyLocation);
		object obj5 = default(object);
		turretController.MoveTurret((Vector3)(&obj5));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763370");
		StateNode connectedNode2 = GetConnectedNode<StateNode>("OnStartedMoving");
		if (connectedNode2 != null)
		{
			NodeExecutionState newState2 = NodeExecutionState.NewState;
			connectedNode2.OnEnter(newState2);
		}
		goto IL_0367;
		IL_034f:
		if (obj3 == null)
		{
			goto IL_031c;
		}
		goto IL_0367;
	}

	private IEnumerator StateCheck()
	{
		_003CStateCheck_003Ed__8 obj = new _003CStateCheck_003Ed__8(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public override void OnExecute(NodeExecutionState state)
	{
		if (WaitTillMovementComplete)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763230");
			object obj = default(object);
			if (obj != null && TurretController.Instance.IsMoving)
			{
				return;
			}
		}
		base.OnExit(state, "To");
	}
}
