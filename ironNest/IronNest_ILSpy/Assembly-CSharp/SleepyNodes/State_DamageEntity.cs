using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_DamageEntity : StateNode
{
	public StateNode To;

	public TargetSelection EntitiesToDamage;

	public int Damage;

	public ShellDefinition Shell;

	public StateNode OnEntityDestroyed;

	public EntityContextKeys EntityDestroyed;

	public override void ResetNode()
	{
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_01b0: Expected O, but got Ref
		//IL_0356: Expected O, but got I
		base.OnEnter(state);
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null) || !(FireMission._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		Vector3[] fourCornersArray = new Vector3[4];
		if ((object)gameObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			RectTransform rectTransform = default(RectTransform);
			if ((object)rectTransform != null)
			{
				rectTransform.GetWorldCorners(fourCornersArray);
				if (state != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180755750");
					if (EntitiesToDamage != null)
					{
						List<MapEntity> list = EntitiesToDamage.Resolve(FireMission._003CInstance_003Ek__BackingField, state);
						if (list == null || Enumerable.Count(list) == 0)
						{
							Debug.LogError("No Entities Found For Damaging");
							if (list == null)
							{
								goto IL_036a;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						string text = null;
						List<MapEntity>.Enumerator enumerator = default(List<MapEntity>.Enumerator);
						MapEntity mapEntity = default(MapEntity);
						object arg = default(object);
						string text2 = default(string);
						while (true)
						{
							if (enumerator.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								bool flag = mapEntity == null;
								NodeExecutionState nodeExecutionState = (NodeExecutionState)(&enumerator);
								if (!flag)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									ShellDefinition shell = Shell;
									if ((object)Shell != null)
									{
										string message = $"[NODES] Attempting to damage '{mapEntity.ID}' with {arg} ({shell.ShellId}) damage";
										Debug.Log(message);
										bool flag2 = (object)mapEntity.Location == null;
										nodeExecutionState = (NodeExecutionState)(object)mapEntity.Location;
										if (!flag2)
										{
											bool flag3 = mapEntity.Location.TakeDamage(Shell, Damage, text2);
											bool flag4 = !flag3;
											text = text2;
											if (flag4)
											{
												continue;
											}
											StateNode connectedNode = GetConnectedNode<StateNode>("OnEntityDestroyed");
											bool flag5 = connectedNode != null;
											bool flag6 = !flag5;
											text = text2;
											if (!flag6)
											{
												NodeExecutionState newState = NodeExecutionState.NewState;
												bool flag7 = newState == null;
												nodeExecutionState = null;
												if (flag7)
												{
													throw new NullReferenceException();
												}
												newState.Set(EntityDestroyed, mapEntity);
												bool flag8 = (object)connectedNode == null;
												nodeExecutionState = newState;
												if (flag8)
												{
													break;
												}
												connectedNode.OnEnter(newState);
												text = (string)0;
											}
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							enumerator.Dispose();
							base.OnExit(state, "To");
							return;
						}
						throw new NullReferenceException();
					}
				}
			}
		}
		goto IL_036a;
		IL_036a:
		throw new NullReferenceException();
	}

	public State_DamageEntity()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
