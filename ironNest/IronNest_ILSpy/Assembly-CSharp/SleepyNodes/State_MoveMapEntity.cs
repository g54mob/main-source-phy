using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_MoveMapEntity : StateNode
{
	private sealed class _003CStateCheck_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public double endsAt;

		public MapEntity entity;

		public State_MoveMapEntity _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStateCheck_003Ed__12(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_009f: Expected I4, but got I8
			//IL_00cc: Expected O, but got I4
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Expected O, but got Unknown
			//IL_0015: Expected O, but got I4
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected I4, but got Unknown
			//IL_0130: Expected I4, but got O
			//IL_02bb: Expected I4, but got O
			Node node = _003C_003E4__this;
			int num = _003C_003E1__state ^ _003C_003E1__state;
			int num2 = _003C_003E1__state & num;
			bool flag = num2 < 0;
			bool flag2 = _003C_003E1__state < 0;
			bool flag3 = _003C_003E1__state == 0;
			if (!flag3)
			{
				object obj = _003C_003E1__state - 1;
				int num3 = _003C_003E1__state ^ 1;
				int num4 = _003C_003E1__state ^ obj;
				int num5 = num3 & num4;
				flag = num5 < 0;
				flag2 = (nint)obj < 0;
				flag3 = obj == null;
				if (_003C_003E1__state != 1)
				{
					goto IL_02a7;
				}
			}
			_003C_003E1__state = -1;
			double timeAsDouble = Time.timeAsDouble;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm0\"");
			bool flag4 = flag2 == flag;
			object obj2 = !flag4;
			object obj3 = obj2 | flag3;
			if (obj3 == null)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D74B0");
			MapEntityStates newState = default(MapEntityStates);
			FireMission._003CInstance_003Ek__BackingField.SetEntityState(entity, newState);
			object obj4 = default(object);
			object arg = (MapEntityStates)obj4;
			MapEntity mapEntity = entity;
			MapEntityStates mapEntityStates = default(MapEntityStates);
			object arg2 = mapEntityStates;
			string message = $"[ENTITY] Removed State: {arg} For {mapEntity.ID} | {arg2}";
			Debug.Log(message);
			StateNode connectedNode = _003C_003E4__this.GetConnectedNode<StateNode>("OnStateReset");
			if (connectedNode != null)
			{
				NodeExecutionState newState2 = NodeExecutionState.NewState;
				if (newState2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ rsi_v1 (SleepyNodes.Node)+70]");
					newState2.Set(EntityContextKeys.EntityTarget, entity);
					if ((object)connectedNode != null)
					{
						connectedNode.OnEnter(newState2);
						goto IL_02a7;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_02a7;
			IL_02a7:
			return false;
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

	public TargetSelection EntityToMove;

	public LocationSelection LocationToMoveTo;

	public bool ShouldUpdateState;

	public MapEntityStates StateToAdd = MapEntityStates.Moving;

	public float SecondsForState = 30f;

	public bool WaitForState;

	public bool UseSmoothMovement;

	public StateNode OnStateReset;

	public EntityContextKeys EntityStateReset;

	public override void ResetNode()
	{
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_007f: Invalid comparison between I4 and F4
		//IL_009f: Expected F4, but got I4
		//IL_0144: Expected O, but got Ref
		//IL_0182: Expected I, but got O
		//IL_0190: Expected I, but got O
		//IL_01a0: Expected O, but got I
		//IL_01dc: Expected O, but got I
		//IL_0201: Expected O, but got I4
		//IL_03c8: Expected O, but got Ref
		//IL_03e3: Expected O, but got F4
		//IL_0408: Expected F8, but got O
		//IL_0408: Expected F4, but got O
		//IL_0408: Expected O, but got Ref
		//IL_0417: Expected O, but got F4
		//IL_0302: Invalid comparison between F4 and I4
		//IL_0315: Expected O, but got I4
		//IL_035e: Expected O, but got F4
		base.OnEnter(state);
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null) || !(FireMission._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		double timeAsDouble = Time.timeAsDouble;
		float num = SecondsForState;
		if (0f > SecondsForState)
		{
			num = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm8\"");
		Vector3[] array = new Vector3[4];
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		rectTransform.GetWorldCorners(array);
		List<MapEntity> list = EntityToMove.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		NodeGraph nodeGraph = null;
		Vector3[] gridBounds = array;
		float num3 = default(float);
		float num2 = num3;
		List<MapEntity>.Enumerator enumerator2 = default(List<MapEntity>.Enumerator);
		List<MapEntity>.Enumerator enumerator = enumerator2;
		NodeExecutionState state2 = state;
		List<MapEntity>.Enumerator enumerator3 = default(List<MapEntity>.Enumerator);
		MapEntity mapEntity = default(MapEntity);
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] array2 = default(Vector3[]);
		FireMission fireMission = default(FireMission);
		MapEntityStates newState = default(MapEntityStates);
		MapEntityStates stateToAdd = default(MapEntityStates);
		MapEntityStates state3 = default(MapEntityStates);
		object obj3 = default(object);
		List<MapEntity>.Enumerator enumerator4 = default(List<MapEntity>.Enumerator);
		double endsAt = default(double);
		NodeExecutionState nodeExecutionState = default(NodeExecutionState);
		while (true)
		{
			NodeGraph nodeGraph3;
			if (enumerator3.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				NodeGraph nodeGraph2 = graph;
				bool flag = LocationToMoveTo == null;
				LocationSelection locationSelection = (LocationSelection)(&enumerator3);
				if (!flag)
				{
					if ((object)graph == null)
					{
						goto IL_04d0;
					}
					nint num4 = (nint)nodeGraph2;
					nint num5 = (nint)typeof(MissionGraph);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v618 @ r8_v29 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v617 @ r9_v23 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v618 @ r8_v29 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
					if (num6 >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v617 @ r9_v23 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v667 @ rax_v65+FFFFFFF8+v619 @ rax_v60*8]");
						bool flag2 = 0 == (nint)typeof(MissionGraph);
						nodeGraph3 = (NodeGraph)1;
						if (flag2)
						{
							goto IL_0515;
						}
					}
					nodeGraph3 = nodeGraph;
					goto IL_0515;
				}
				throw new NullReferenceException();
			}
			enumerator3.Dispose();
			return;
			IL_04d0:
			GridReference gridReference = LocationToMoveTo.Resolve(FireMission._003CInstance_003Ek__BackingField, mapEntity, state2, missionGraph, array2);
			if (gridReference != null)
			{
				LocationSelection locationSelection;
				if (ShouldUpdateState)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
					bool flag3 = mapEntity == null;
					locationSelection = null;
					if (flag3)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D71C0");
					if ((object)fireMission == null)
					{
						break;
					}
					fireMission.SetEntityState(mapEntity, newState);
					object arg = stateToAdd;
					object arg2 = state3;
					string message = $"[ENTITY] Set State: {arg} For {mapEntity.ID} | {arg2}";
					Debug.Log(message);
					bool flag4 = !(SecondsForState > 0f);
					rectTransform = (RectTransform)StateToAdd;
					if (!flag4)
					{
						IEnumerator routine = StateCheck(mapEntity, num);
						Coroutine coroutine = FireMission._003CInstance_003Ek__BackingField.StartCoroutine(routine);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763370");
						rectTransform = (RectTransform)num;
					}
					state3 = mapEntity.State;
					stateToAdd = StateToAdd;
					nodeGraph = null;
					gridBounds = array;
				}
				LocationSelection locationToMoveTo = LocationToMoveTo;
				if (LocationToMoveTo == null)
				{
					throw new NullReferenceException();
				}
				Vector3 location = gridReference.GetLocation(gridBounds, locationToMoveTo.FuzzyLocation);
				num2 = SecondsForState;
				bool flag5 = (object)FireMission._003CInstance_003Ek__BackingField == null;
				locationSelection = (LocationSelection)(&obj3);
				if (flag5)
				{
					throw new NullReferenceException();
				}
				enumerator = (List<MapEntity>.Enumerator)location.x;
				FireMission._003CInstance_003Ek__BackingField.MoveMapEntity(mapEntity, (Vector3)(&enumerator4), UseSmoothMovement, (float)missionGraph, (double)array2, endsAt);
				enumerator4 = (List<MapEntity>.Enumerator)location.x;
			}
			state2 = nodeExecutionState;
			continue;
			IL_0515:
			if ((object)nodeGraph3 == null)
			{
			}
			goto IL_04d0;
		}
		throw new NullReferenceException();
	}

	private IEnumerator StateCheck(MapEntity entity, double endsAt)
	{
		_003CStateCheck_003Ed__12 obj = new _003CStateCheck_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.entity = entity;
		obj.endsAt = endsAt;
		return obj;
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_0016: Expected O, but got I4
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_009f: Expected O, but got I4
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		if (ShouldUpdateState)
		{
			object obj = WaitForState ^ WaitForState;
			object obj2 = WaitForState & obj;
			bool flag = (nint)obj2 < 0;
			bool flag2 = (WaitForState ? 1 : 0) < (false ? 1 : 0);
			bool flag3 = !WaitForState;
			if (!flag3)
			{
				double timeAsDouble = Time.timeAsDouble;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180763230");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm6\"");
				bool flag4 = flag2 == flag;
				object obj3 = !flag3;
				object obj4 = flag4 & obj3;
				if (obj4 != null)
				{
					return;
				}
			}
		}
		base.OnExit(state, "To");
	}

	public State_MoveMapEntity()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
