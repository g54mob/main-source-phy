using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public class State_ImpactStart : StateNodeEntry
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<EntityLocation, MapEntity> _003C_003E9__8_1;

		public static Func<EntityLocation, MapEntity> _003C_003E9__8_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal MapEntity _003CStartImpact_003Eb__8_1(EntityLocation x)
		{
			if ((object)x != null)
			{
				return x.Entity;
			}
			return (MapEntity)(object)new NullReferenceException();
		}

		internal MapEntity _003CStartImpact_003Eb__8_0(EntityLocation x)
		{
			if ((object)x != null)
			{
				return x.Entity;
			}
			return (MapEntity)(object)new NullReferenceException();
		}
	}

	public bool OverrideRadius;

	public float MinRadius = 0.1f;

	public float MaxRadius = 0.5f;

	public FilterEntitySet EntityConditions;

	public StateNode ForEachEntityHit;

	public EntityContextKeys EntityHit = EntityContextKeys.EntityEffected;

	public LocationContextKeys ClosestGridLocation;

	private Vector2 lastImpactLocation;

	public unsafe List<MapEntity> StartImpact(StateNode.NodeExecutionState state, ShellDefinition shell, Vector2 impactLocation)
	{
		//IL_007b: Expected F4, but got I4
		//IL_009e: Expected O, but got I
		//IL_0126: Invalid comparison between F4 and I4
		//IL_0138: Expected O, but got I4
		//IL_096e: Expected O, but got I4
		//IL_0189: Expected O, but got I4
		//IL_0581: Expected O, but got I
		//IL_01f3: Expected O, but got I4
		//IL_0787: Expected O, but got I
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Expected O, but got Unknown
		//IL_027a: Expected O, but got I
		//IL_0696: Expected O, but got I
		//IL_0299: Expected O, but got I
		//IL_02d3: Expected O, but got Ref
		//IL_02ef: Expected O, but got Ref
		//IL_033a: Expected O, but got I
		//IL_0359: Expected O, but got Ref
		//IL_0446: Expected O, but got I
		//IL_037d: Expected O, but got I
		//IL_046f: Invalid comparison between F4 and O
		//IL_048f: Invalid comparison between O and F4
		//IL_03c3: Expected O, but got Ref
		//IL_041e: Expected F4, but got Ref
		lastImpactLocation = impactLocation;
		float maxRadius;
		State_ImpactStart state_ImpactStart;
		if (OverrideRadius)
		{
			maxRadius = MaxRadius;
		}
		else
		{
			bool flag = (object)shell == null;
			state_ImpactStart = this;
			if (flag)
			{
				goto IL_087b;
			}
			maxRadius = shell.ImpactRadius;
		}
		MaxRadius = maxRadius;
		float minRadius = ((!OverrideRadius) ? 0f : MinRadius);
		MinRadius = minRadius;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		string text = guid2.ToString();
		FireMission fireMission = UnityEngine.Object.FindFirstObjectByType<FireMission>();
		bool flag2 = (object)fireMission == null;
		state_ImpactStart = (State_ImpactStart)0;
		GridReference value;
		List<EntityLocation> list;
		float? distanceFromNearestTarget;
		string shellInstanceId;
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!flag2)
		{
			value = GridReference.FromLocalSpace(impactLocation, fireMission.cellWidth, fireMission.cellHeight, fireMission.yIncreasesUp);
			bool flag3 = state == null;
			state_ImpactStart = (State_ImpactStart)impactLocation;
			if (!flag3)
			{
				state.Set(ClosestGridLocation, value);
				list = new List<EntityLocation>();
				bool flag4 = !(MaxRadius > 0f);
				distanceFromNearestTarget = (float?)(object)0;
				shellInstanceId = text;
				Vector2 vector = impactLocation;
				if (!flag4)
				{
					bool flag5 = ImpactTracker.EntityLocations == null;
					distanceFromNearestTarget = (float?)(object)0;
					shellInstanceId = text;
					vector = impactLocation;
					if (!flag5)
					{
						bool flag6 = ImpactTracker.EntityLocations == null;
						state_ImpactStart = (State_ImpactStart)(object)ImpactTracker.EntityLocations;
						if (!flag6)
						{
							int count = ImpactTracker.EntityLocations.Count;
							bool flag7 = count <= 0;
							distanceFromNearestTarget = (float?)(object)0;
							shellInstanceId = text;
							vector = impactLocation;
							if (flag7)
							{
								goto IL_0b7a;
							}
							bool flag8 = ImpactTracker.EntityLocations == null;
							state_ImpactStart = (State_ImpactStart)(object)ImpactTracker.EntityLocations;
							if (!flag8)
							{
								Dictionary<string, EntityLocation>.ValueCollection values = ImpactTracker.EntityLocations.Values;
								bool flag9 = values == null;
								state_ImpactStart = (State_ImpactStart)(object)ImpactTracker.EntityLocations;
								if (!flag9)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
									distanceFromNearestTarget = (float?)(object)0;
									vector = impactLocation;
									Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
									Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator2 = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
									object obj4 = default(object);
									Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator3 = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
									Dictionary<string, EntityLocation>.ValueCollection.Enumerator enumerator4 = default(Dictionary<string, EntityLocation>.ValueCollection.Enumerator);
									while (enumerator.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										if (!(obj != null))
										{
											continue;
										}
										bool flag10 = (object)obj == null;
										FilterEntitySet filterEntitySet = (FilterEntitySet)(object)obj;
										if (!flag10)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
											bool flag11 = (nint)0 == 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
											filterEntitySet = (FilterEntitySet)0;
											if (!flag11)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
												if (!((MapEntity)0).IsAlive)
												{
													continue;
												}
												Vector2 localPosition = ((EntityLocation)obj).LocalPosition;
												((StateNode.NodeExecutionState)(&enumerator2)).Set<GridReference>(LocationContextKeys.LocationEffected, null);
												bool flag12 = (object)shell == null;
												filterEntitySet = (FilterEntitySet)(&enumerator2);
												if (!flag12)
												{
													bool flag13 = shell.IgnoreInTrackingShotsFired;
													vector = localPosition;
													if (!flag13)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
														object obj2 = 0;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
														bool flag14 = (nint)0 == 0;
														state_ImpactStart = (State_ImpactStart)(&enumerator2);
														if (flag14)
														{
															throw new NullReferenceException();
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v435 @ rax_v116+40]");
														object obj3 = (nint)0 & (nint)1;
														bool flag15 = obj3 == null;
														vector = localPosition;
														if (!flag15)
														{
															nint num = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1921 @ rdx_v60 (Il2CppClass<System.Nullable`1<System.Single>>)+80]");
															((StateNode.NodeExecutionState)(&distanceFromNearestTarget)).Set<GridReference>(LocationContextKeys.LocationEffected, null);
															if (obj4 != null)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
																bool flag16 = System.Runtime.CompilerServices.Unsafe.As<Dictionary<string, EntityLocation>.ValueCollection.Enumerator, UIntPtr>(ref enumerator3) <= System.Runtime.CompilerServices.Unsafe.As<Dictionary<string, EntityLocation>.ValueCollection.Enumerator, UIntPtr>(ref enumerator4);
																vector = localPosition;
																if (flag16)
																{
																	goto IL_0a00;
																}
															}
															float? num2 = (nint)(&vector);
															distanceFromNearestTarget = num2;
														}
													}
													goto IL_0a00;
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
										IL_0a00:
										filterEntitySet = EntityConditions;
										if (EntityConditions != null)
										{
											FilterEntitySet entityConditions = EntityConditions;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
											if (!entityConditions.Resolve((MapEntity)0, state))
											{
												continue;
											}
											float maxRadius2 = MaxRadius;
											if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)maxRadius2) >= System.Runtime.CompilerServices.Unsafe.As<Dictionary<string, EntityLocation>.ValueCollection.Enumerator, UIntPtr>(ref enumerator4) && System.Runtime.CompilerServices.Unsafe.As<Dictionary<string, EntityLocation>.ValueCollection.Enumerator, UIntPtr>(ref enumerator4) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)MinRadius))
											{
												if (list == null)
												{
													throw new NullReferenceException();
												}
												list.Add((EntityLocation)obj);
											}
											continue;
										}
										throw new NullReferenceException();
									}
									enumerator.Dispose();
									shellInstanceId = text;
									goto IL_0b7a;
								}
							}
						}
						goto IL_087b;
					}
				}
				goto IL_0b7a;
			}
		}
		goto IL_087b;
		IL_0b7a:
		MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
		if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
		{
			MissionManager.MissionState currentMissionState = missionManager.CurrentMissionState;
			if (missionManager.CurrentMissionState != null)
			{
				MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
				if (currentMissionState.TrackingValues != null && trackingValues.Data_ShellsFired != null)
				{
					state_ImpactStart = (State_ImpactStart)(object)MissionManager._003CInstance_003Ek__BackingField;
					if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v404 @ rcx_v5 (SleepyNodes.State_ImpactStart)+68]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v404 @ rcx_v5 (SleepyNodes.State_ImpactStart)+68]");
						if ((nint)0 != 0)
						{
							MedalTrackedValues.Data_ShellFired data_ShellFired = new MedalTrackedValues.Data_ShellFired();
							if (data_ShellFired != null)
							{
								data_ShellFired.ShellInstanceId = shellInstanceId;
								data_ShellFired.Shell = shell;
								float time = Time.time;
								data_ShellFired.ShotAtTime = time;
								data_ShellFired.DistanceFromNearestTarget = distanceFromNearestTarget;
								Func<EntityLocation, MapEntity> selector = _003C_003Ec._003C_003E9__8_1;
								if (_003C_003Ec._003C_003E9__8_1 == null)
								{
									selector = (_003C_003Ec._003C_003E9__8_1 = (EntityLocation x) => (MapEntity)(((object)x != null) ? ((object)x.Entity) : ((object)new NullReferenceException())));
								}
								IEnumerable<MapEntity> source = Enumerable.Select(list, selector);
								List<MapEntity> hits = Enumerable.ToList(source);
								data_ShellFired.Hits = hits;
								state_ImpactStart = (State_ImpactStart)(data_ShellFired + 32);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rax_v70+28]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rax_v70+28]");
									((MedalTrackedValues)0).TrackShell(data_ShellFired);
									goto IL_069b;
								}
							}
						}
					}
					goto IL_087b;
				}
			}
		}
		goto IL_069b;
		IL_069b:
		StateNode connectedNode = GetConnectedNode<StateNode>("ForEachEntityHit");
		if (connectedNode != null)
		{
			if (list == null)
			{
				goto IL_087b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<EntityLocation>.Enumerator enumerator5 = default(List<EntityLocation>.Enumerator);
			while (enumerator5.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
				bool flag17 = newState == null;
				StateNode.NodeExecutionState nodeExecutionState = null;
				if (!flag17)
				{
					newState.Set("ShellInstanceId", text);
					if ((object)obj != null)
					{
						EntityContextKeys entityHit = EntityHit;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ stack_-D8_v10 (UnityEngine.Object)+B0]");
						newState.Set(entityHit, (MapEntity)0);
						newState.Set(ClosestGridLocation, value);
						if ((object)connectedNode != null)
						{
							connectedNode.OnEnter(newState);
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator5.Dispose();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A73D]");
		Node node;
		if ((nint)0 == 0)
		{
			_ = 1;
			node = this;
		}
		else
		{
			node = this;
		}
		StateNode connectedNode2 = node.GetConnectedNode<StateNode>("To", out var connectedField);
		StateNode.NodeExecutionState nodeExecutionState2 = default(StateNode.NodeExecutionState);
		nodeExecutionState2.lastFieldPort = connectedField;
		if (connectedNode2 != null)
		{
			if ((object)connectedNode2 == null)
			{
				return (List<MapEntity>)(object)new NullReferenceException();
			}
			connectedNode2.OnEnter(nodeExecutionState2);
		}
		Func<EntityLocation, MapEntity> selector2 = _003C_003Ec._003C_003E9__8_0;
		if (_003C_003Ec._003C_003E9__8_0 == null)
		{
			selector2 = (_003C_003Ec._003C_003E9__8_0 = (EntityLocation x) => (MapEntity)(((object)x != null) ? ((object)x.Entity) : ((object)new NullReferenceException())));
		}
		IEnumerable<MapEntity> source2 = Enumerable.Select(list, selector2);
		return Enumerable.ToList(source2);
		IL_087b:
		throw new NullReferenceException();
	}
}
