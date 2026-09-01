using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;

namespace SleepyNodes;

public class Event_EntityDestroyed : EventNode
{
	public enum LookupTypes
	{
		Any,
		Count,
		All
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MapEntity, bool> _003C_003E9__9_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CShouldRun_003Eb__9_1(MapEntity x)
		{
			//IL_004b: Expected I4, but got O
			if (x != null)
			{
				bool isAlive = x.IsAlive;
				return (byte)((isAlive ? 1u : 0u) ^ 1u) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public Event_EntityDestroyed _003C_003E4__this;

		public NodeExecutionState state;

		internal bool _003CShouldRun_003Eb__0(MapEntity x)
		{
			//IL_007a: Expected I4, but got O
			Event_EntityDestroyed event_EntityDestroyed = _003C_003E4__this;
			if ((object)_003C_003E4__this != null && event_EntityDestroyed.EntityFilter != null)
			{
				return event_EntityDestroyed.EntityFilter.Resolve(x, state);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public FilterEntitySet EntityFilter;

	public LookupTypes LookupType;

	public int Amount = 1;

	public bool ResetCountAfterTrigger = true;

	public EntityContextKeys EntityDestroyed = EntityContextKeys.EntityEffected;

	private int numberOfEntitiesSeen;

	private MapEntity cachedEntity;

	public override void ResetNode()
	{
		base.AlreadyTriggered = false;
		numberOfEntitiesSeen = 0;
		cachedEntity = null;
	}

	protected override bool ShouldRun(EventData data)
	{
		//IL_0368: Expected I4, but got O
		//IL_003a: Expected I, but got O
		//IL_0042: Expected I, but got O
		//IL_0052: Expected O, but got I
		//IL_008e: Expected O, but got I
		//IL_0115: Expected O, but got I
		//IL_0144: Expected O, but got I
		//IL_01af: Expected O, but got I4
		//IL_023a: Expected O, but got I
		_003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals7 = new _003C_003Ec__DisplayClass9_0();
		if (CS_0024_003C_003E8__locals7 != null)
		{
			CS_0024_003C_003E8__locals7._003C_003E4__this = this;
			if (data != null)
			{
				nint num = (nint)typeof(EventData_EntityDestroyed);
				nint num2 = (nint)data;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rdx_v5 (Il2CppClass<SleepyNodes.EventData_EntityDestroyed>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ r8_v3 (Il2CppClass<SleepyNodes.EventNode+EventData>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rdx_v5 (Il2CppClass<SleepyNodes.EventData_EntityDestroyed>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ r8_v3 (Il2CppClass<SleepyNodes.EventNode+EventData>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v10+FFFFFFF8+v160 @ rax_v9*8]");
					if (0 == (nint)typeof(EventData_EntityDestroyed))
					{
						NodeExecutionState newState = NodeExecutionState.NewState;
						CS_0024_003C_003E8__locals7.state = newState;
						if (EntityFilter == null)
						{
							goto IL_035a;
						}
						FilterEntitySet entityFilter = EntityFilter;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+18]");
						if (entityFilter.Resolve((MapEntity)0, CS_0024_003C_003E8__locals7.state))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [data @ rdx (SleepyNodes.EventNode+EventData)+18]");
							cachedEntity = (MapEntity)0;
							if (FireMission._003CInstance_003Ek__BackingField != null)
							{
								int num4 = ++numberOfEntitiesSeen;
								bool flag = LookupType == LookupTypes.Any;
								if (flag)
								{
									goto IL_0394;
								}
								object obj3 = LookupType - 1;
								if (!flag)
								{
									if ((nint)obj3 == 1)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
										object obj4 = default(object);
										if (obj4 != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v21+78]");
											if ((nint)0 != 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v21+78]");
												Dictionary<string, MapEntity>.ValueCollection values = ((Dictionary<string, MapEntity>)0).Values;
												Func<MapEntity, bool> predicate = delegate(MapEntity x)
												{
													//IL_007a: Expected I4, but got O
													Event_EntityDestroyed event_EntityDestroyed = CS_0024_003C_003E8__locals7._003C_003E4__this;
													if ((object)CS_0024_003C_003E8__locals7._003C_003E4__this == null || event_EntityDestroyed.EntityFilter == null)
													{
														NullReferenceException ex2 = new NullReferenceException();
														return (byte)(int)ex2 != 0;
													}
													return event_EntityDestroyed.EntityFilter.Resolve(x, CS_0024_003C_003E8__locals7.state);
												};
												IEnumerable<MapEntity> source = Enumerable.Where(values, predicate);
												List<MapEntity> list = Enumerable.ToList(source);
												if (list != null)
												{
													if (list._size > 0)
													{
														Func<MapEntity, bool> predicate2 = _003C_003Ec._003C_003E9__9_1;
														if (_003C_003Ec._003C_003E9__9_1 == null)
														{
															predicate2 = (_003C_003Ec._003C_003E9__9_1 = delegate(MapEntity x)
															{
																//IL_004b: Expected I4, but got O
																if (x == null)
																{
																	NullReferenceException ex2 = new NullReferenceException();
																	return (byte)(int)ex2 != 0;
																}
																bool isAlive = x.IsAlive;
																return (byte)((isAlive ? 1u : 0u) ^ 1u) != 0;
															});
														}
														if (!Enumerable.All(list, predicate2))
														{
															goto IL_02e5;
														}
													}
													goto IL_0394;
												}
											}
										}
										goto IL_035a;
									}
								}
								else if (Amount > 0 && num4 >= Amount)
								{
									if (ResetCountAfterTrigger)
									{
										numberOfEntitiesSeen = 0;
									}
									goto IL_0394;
								}
							}
						}
					}
				}
			}
			goto IL_02e5;
		}
		goto IL_035a;
		IL_035a:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0394:
		return true;
		IL_02e5:
		return false;
	}

	public override void Run(NodeExecutionState state)
	{
		state.Set(EntityDestroyed, cachedEntity);
		base.Run(state);
	}

	public Event_EntityDestroyed()
	{
		EnableOnStart = true;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
