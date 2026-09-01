using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

[Serializable]
public class LocationFilter
{
	public enum LocationTypes
	{
		Any,
		GridLocation,
		Zone,
		Relative
	}

	public enum RelativeReferenceTypes
	{
		Self,
		EntityFromFilter,
		AllEntitiesFromFilter,
		GridLocation,
		Zone,
		ContextLocation,
		ContextEntity,
		Turret
	}

	public enum RelativeDirections
	{
		Distance
	}

	private sealed class _003C_003Ec__DisplayClass12_0
	{
		public LocationFilter _003C_003E4__this;

		public StateNode.NodeExecutionState state;

		public FireMission fireMission;

		internal bool _003CResolve_003Eb__0(Zone x)
		{
			//IL_0074: Expected I4, but got O
			if (x != null)
			{
				LocationFilter locationFilter = _003C_003E4__this;
				if (_003C_003E4__this != null)
				{
					return x.ID == locationFilter.ZoneID;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CResolve_003Eb__1(Zone x)
		{
			//IL_0074: Expected I4, but got O
			if (x != null)
			{
				LocationFilter locationFilter = _003C_003E4__this;
				if (_003C_003E4__this != null)
				{
					return x.ID == locationFilter.ZoneID;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CResolve_003Eb__2(MapEntity x)
		{
			//IL_007a: Expected I4, but got O
			LocationFilter locationFilter = _003C_003E4__this;
			if (_003C_003E4__this != null && locationFilter.TargetFilter != null)
			{
				return locationFilter.TargetFilter.Resolve(x, state);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass12_1
	{
		public Vector2 locationPos;

		public _003C_003Ec__DisplayClass12_0 CS_0024_003C_003E8__locals1;

		internal unsafe bool _003CResolve_003Eb__3(MapEntity x)
		{
			//IL_0116: Expected I4, but got O
			//IL_007a: Expected O, but got Ref
			//IL_00f2: Invalid comparison between F4 and O
			_003C_003Ec__DisplayClass12_0 obj = CS_0024_003C_003E8__locals1;
			if (CS_0024_003C_003E8__locals1 != null && x != null && (object)obj.fireMission != null)
			{
				object obj2 = default(object);
				Vector2 vector = obj.fireMission.ToLocalSpace((Vector3)(&obj2));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
				_003C_003Ec__DisplayClass12_0 obj3 = CS_0024_003C_003E8__locals1;
				if (CS_0024_003C_003E8__locals1 != null)
				{
					LocationFilter locationFilter = obj3._003C_003E4__this;
					if (obj3._003C_003E4__this != null)
					{
						float distance = locationFilter.Distance;
						object obj4 = default(object);
						bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)distance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4);
						return !flag;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass13_0
	{
		public LocationFilter _003C_003E4__this;

		public StateNode.NodeExecutionState state;

		internal bool _003CResolveRelative_003Eb__0(MapEntity x)
		{
			//IL_007a: Expected I4, but got O
			LocationFilter locationFilter = _003C_003E4__this;
			if (_003C_003E4__this != null && locationFilter.TargetFilter != null)
			{
				return locationFilter.TargetFilter.Resolve(x, state);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public LocationTypes LocationType;

	public ContextVariableOrInline_GridRefence GridLocation;

	public string ZoneID;

	public FilterEntitySet TargetFilter;

	public ContextKey_Location ContextLocationKey;

	public ContextKey_Entity ContextEntityKey;

	public RelativeReferenceTypes RelativeTo;

	public RelativeDirections RelativeDirection;

	public float Distance;

	public unsafe bool Resolve(GridReference location, FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		//IL_05ab: Expected I4, but got O
		//IL_005d: Expected O, but got I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0357: Expected O, but got Ref
		//IL_03c2: Expected O, but got Ref
		//IL_0198: Expected O, but got Ref
		_003C_003Ec__DisplayClass12_0 CS_0024_003C_003E8__locals31 = new _003C_003Ec__DisplayClass12_0();
		if (CS_0024_003C_003E8__locals31 != null)
		{
			CS_0024_003C_003E8__locals31._003C_003E4__this = this;
			StateNode.NodeExecutionState state2 = default(StateNode.NodeExecutionState);
			CS_0024_003C_003E8__locals31.state = state2;
			CS_0024_003C_003E8__locals31.fireMission = fireMission;
			bool flag = LocationType == LocationTypes.Any;
			if (flag)
			{
				return true;
			}
			object obj = LocationType - 1;
			object obj4 = default(object);
			if (!flag)
			{
				object obj2 = obj - 1;
				object obj6 = default(object);
				if (!flag)
				{
					if ((nint)obj2 == 1)
					{
						Vector3[] gridBounds2 = default(Vector3[]);
						if (RelativeTo == RelativeReferenceTypes.AllEntitiesFromFilter)
						{
							_003C_003Ec__DisplayClass12_1 CS_0024_003C_003E8__locals27 = new _003C_003Ec__DisplayClass12_1();
							if (CS_0024_003C_003E8__locals27 != null)
							{
								CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1 = CS_0024_003C_003E8__locals31;
								_003C_003Ec__DisplayClass12_0 obj3 = CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1;
								if (CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1 != null && location != null)
								{
									Vector3 location2 = location.GetLocation(gridBounds2);
									if ((object)obj3.fireMission != null)
									{
										Vector2 locationPos = obj3.fireMission.ToLocalSpace((Vector3)(&obj4));
										_003C_003Ec__DisplayClass12_0 obj5 = CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1;
										CS_0024_003C_003E8__locals27.locationPos = locationPos;
										if (CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1 != null)
										{
											FireMission fireMission2 = obj5.fireMission;
											if ((object)obj5.fireMission != null && fireMission2.Entities != null)
											{
												Dictionary<string, MapEntity>.ValueCollection values = fireMission2.Entities.Values;
												Func<MapEntity, bool> predicate = delegate(MapEntity x)
												{
													//IL_007a: Expected I4, but got O
													LocationFilter locationFilter = CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1._003C_003E4__this;
													if (CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1._003C_003E4__this == null || locationFilter.TargetFilter == null)
													{
														NullReferenceException ex2 = new NullReferenceException();
														return (byte)(int)ex2 != 0;
													}
													return locationFilter.TargetFilter.Resolve(x, CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1.state);
												};
												IEnumerable<MapEntity> source = Enumerable.Where(values, predicate);
												Func<MapEntity, bool> predicate2 = delegate(MapEntity x)
												{
													//IL_0116: Expected I4, but got O
													//IL_007a: Expected O, but got Ref
													//IL_00f2: Invalid comparison between F4 and O
													_003C_003Ec__DisplayClass12_0 obj7 = CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1;
													if (CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1 != null && x != null && (object)obj7.fireMission != null)
													{
														object obj8 = default(object);
														Vector2 vector3 = obj7.fireMission.ToLocalSpace((Vector3)(&obj8));
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
														_003C_003Ec__DisplayClass12_0 obj9 = CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1;
														if (CS_0024_003C_003E8__locals27.CS_0024_003C_003E8__locals1 != null)
														{
															LocationFilter locationFilter = obj9._003C_003E4__this;
															if (obj9._003C_003E4__this != null)
															{
																float distance = locationFilter.Distance;
																object obj10 = default(object);
																bool flag4 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)distance) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10);
																return !flag4;
															}
														}
													}
													NullReferenceException ex2 = new NullReferenceException();
													return (byte)(int)ex2 != 0;
												};
												return Enumerable.Any(source, predicate2);
											}
										}
									}
								}
							}
						}
						else
						{
							if (RelativeTo == RelativeReferenceTypes.Zone)
							{
								if (obj6 != null)
								{
									Func<Zone, bool> func = delegate(Zone x)
									{
										//IL_0074: Expected I4, but got O
										if (x != null)
										{
											LocationFilter locationFilter = CS_0024_003C_003E8__locals31._003C_003E4__this;
											if (CS_0024_003C_003E8__locals31._003C_003E4__this != null)
											{
												return x.ID == locationFilter.ZoneID;
											}
										}
										NullReferenceException ex2 = new NullReferenceException();
										return (byte)(int)ex2 != 0;
									};
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
									Zone zone = default(Zone);
									if (zone != null)
									{
										float num = Zone.DistanceToZone(location, zone, CS_0024_003C_003E8__locals31.fireMission, gridBounds2);
										bool flag2 = Distance < num;
										return !flag2;
									}
								}
								goto IL_0525;
							}
							MissionGraph missionGraph2 = default(MissionGraph);
							Vector3[] gridBounds3 = default(Vector3[]);
							GridReference gridReference = ResolveRelative(CS_0024_003C_003E8__locals31.fireMission, self, CS_0024_003C_003E8__locals31.state, missionGraph2, gridBounds3);
							if (location != null)
							{
								Vector3 location3 = location.GetLocation(gridBounds2);
								if ((object)CS_0024_003C_003E8__locals31.fireMission != null)
								{
									Vector2 vector = CS_0024_003C_003E8__locals31.fireMission.ToLocalSpace((Vector3)(&obj4));
									if (gridReference != null)
									{
										Vector3 location4 = gridReference.GetLocation(gridBounds2);
										if ((object)CS_0024_003C_003E8__locals31.fireMission != null)
										{
											float num2 = default(float);
											Vector2 vector2 = CS_0024_003C_003E8__locals31.fireMission.ToLocalSpace((Vector3)(&num2));
											if (RelativeDirection == RelativeDirections.Distance)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
												bool flag3 = Distance < location4.x;
												return !flag3;
											}
											goto IL_0525;
										}
									}
								}
							}
						}
						goto IL_059d;
					}
				}
				else if (obj6 != null)
				{
					Func<Zone, bool> func2 = delegate(Zone x)
					{
						//IL_0074: Expected I4, but got O
						if (x != null)
						{
							LocationFilter locationFilter = CS_0024_003C_003E8__locals31._003C_003E4__this;
							if (CS_0024_003C_003E8__locals31._003C_003E4__this != null)
							{
								return x.ID == locationFilter.ZoneID;
							}
						}
						NullReferenceException ex2 = new NullReferenceException();
						return (byte)(int)ex2 != 0;
					};
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
					Zone zone2 = default(Zone);
					if (zone2 != null)
					{
						return zone2.Contains(location);
					}
				}
				goto IL_0525;
			}
			if (GridLocation != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
				if (location != null)
				{
					return location.Equals(obj4);
				}
			}
		}
		goto IL_059d;
		IL_0525:
		return false;
		IL_059d:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private GridReference ResolveRelative(FireMission fireMission, MapEntity self, StateNode.NodeExecutionState state, MissionGraph missionGraph, Vector3[] gridBounds)
	{
		//IL_0057: Expected O, but got I8
		//IL_0071: Expected O, but got I8
		_003C_003Ec__DisplayClass13_0 obj = new _003C_003Ec__DisplayClass13_0();
		if (obj != null)
		{
			obj._003C_003E4__this = this;
			obj.state = state;
			RelativeReferenceTypes relativeTo = RelativeTo;
			if (RelativeTo <= RelativeReferenceTypes.Turret)
			{
				object obj2 = 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rdx_v6+54E9F0+v76 @ rax_v8 (LocationFilter+RelativeReferenceTypes)*4]");
				object obj3 = 0 + 6442450944L;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v133 @ rcx_v9 (should have been resolved before IL gen)");
			}
			if (GridLocation != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18081E100");
				GridReference result = default(GridReference);
				return result;
			}
		}
		return (GridReference)(object)new NullReferenceException();
	}
}
