using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using Localisation;
using UnityEngine;

namespace SleepyNodes;

public class State_SpawnMapEntity : StateNode
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ShellDefinition, string> _003C_003E9__19_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003COnEnter_003Eb__19_1(ShellDefinition x)
		{
			if ((object)x != null)
			{
				return x.ShellId;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public int entityIDIndex;

		public State_SpawnMapEntity _003C_003E4__this;

		internal bool _003COnEnter_003Eb__0(MapEntity x)
		{
			//IL_00d1: Expected I4, but got O
			//IL_00af: Expected O, but got I4
			if (x != null)
			{
				State_SpawnMapEntity state_SpawnMapEntity = _003C_003E4__this;
				if ((object)_003C_003E4__this != null && x.RawID != null)
				{
					bool flag = x.RawID.Equals(state_SpawnMapEntity.ID, StringComparison.OrdinalIgnoreCase);
					if (!flag)
					{
						return flag;
					}
					object obj = x.IDIndex - entityIDIndex;
					return obj == null;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public StateNode To;

	public bool SetContextVariable;

	public EntityContextKeys LastSpawnedEntity;

	public string ID = "Target";

	public TextIdentifier DisplayName;

	public bool PresetIcon;

	public MapEntityIcon Icon;

	public Sprite IconRaw;

	public EntityRoles Role;

	public MapEntityStates StartingState;

	public int Health;

	public int Armour;

	public int Stars;

	public int Scale;

	public List<ShellDefinition> ImmuneShells;

	public int NumberToSpawn;

	public LocationSelection LocationToSpawn;

	private int lastSpawnedId;

	public override void ResetNode()
	{
		//IL_00b6: Expected O, but got I
		//IL_00c6: Expected O, but got I
		lastSpawnedId = 0;
		string text2;
		if (ID != null)
		{
			string[] array = ID.Split('#');
			if (array != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
				string text = default(string);
				if (text != null)
				{
					char[] trimChars = "0123456789".ToCharArray();
					text2 = text.TrimEnd(trimChars);
					if (text2 != null)
					{
						goto IL_00fd;
					}
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v102 @ rax_v5+B8]");
		object obj2 = 0;
		text2 = (string)obj2;
		goto IL_00fd;
		IL_00fd:
		ID = text2;
	}

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_008f: Expected O, but got I4
		//IL_0473: Expected O, but got I4
		//IL_0115: Expected O, but got I
		//IL_01fb: Expected I, but got O
		//IL_0203: Expected I, but got O
		//IL_0213: Expected O, but got I
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0293: Expected O, but got I4
		//IL_024f: Expected O, but got I
		//IL_04fd: Expected O, but got I4
		//IL_04fd: Expected I4, but got O
		//IL_0285: Expected O, but got I4
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_041b: Expected O, but got I4
		base.OnEnter(state);
		GameObject gameObject = GameObject.FindWithTag("MissionParent");
		if (!(gameObject != null))
		{
			return;
		}
		UnityEngine.Object obj = FireMission._003CInstance_003Ek__BackingField;
		if (!(FireMission._003CInstance_003Ek__BackingField != null))
		{
			return;
		}
		Vector3[] array = new Vector3[4];
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
		RectTransform rectTransform = default(RectTransform);
		rectTransform.GetWorldCorners(array);
		bool flag = NumberToSpawn <= 0;
		object obj2 = 0;
		NodeExecutionState state2 = state;
		if (flag)
		{
			return;
		}
		string id = default(string);
		MissionGraph missionGraph = default(MissionGraph);
		Vector3[] array2 = default(Vector3[]);
		int health = default(int);
		int armour = default(int);
		float x = default(float);
		bool flag3;
		Vector3 location = default(Vector3);
		object arg = default(object);
		do
		{
			_003C_003Ec__DisplayClass19_0 CS_0024_003C_003E8__locals10 = new _003C_003Ec__DisplayClass19_0();
			CS_0024_003C_003E8__locals10._003C_003E4__this = this;
			int entityIDIndex = ((lastSpawnedId <= 0) ? 1 : (lastSpawnedId + 1));
			CS_0024_003C_003E8__locals10.entityIDIndex = entityIDIndex;
			object obj3 = 100;
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r15_v2 (UnityEngine.Object)+78]");
				Dictionary<string, MapEntity>.ValueCollection values = ((Dictionary<string, MapEntity>)0).Values;
				Func<MapEntity, bool> predicate = delegate(MapEntity mapEntity2)
				{
					//IL_00d1: Expected I4, but got O
					//IL_00af: Expected O, but got I4
					if (mapEntity2 != null)
					{
						State_SpawnMapEntity state_SpawnMapEntity = CS_0024_003C_003E8__locals10._003C_003E4__this;
						if ((object)CS_0024_003C_003E8__locals10._003C_003E4__this != null && mapEntity2.RawID != null)
						{
							bool flag4 = mapEntity2.RawID.Equals(state_SpawnMapEntity.ID, StringComparison.OrdinalIgnoreCase);
							if (!flag4)
							{
								return flag4;
							}
							object obj8 = mapEntity2.IDIndex - CS_0024_003C_003E8__locals10.entityIDIndex;
							return obj8 == null;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				if (!Enumerable.Any(values, predicate))
				{
					break;
				}
				object obj4 = obj3 - 1;
				if ((nint)obj3 <= 0)
				{
					break;
				}
				int entityIDIndex2 = CS_0024_003C_003E8__locals10.entityIDIndex + 1;
				CS_0024_003C_003E8__locals10.entityIDIndex = entityIDIndex2;
				obj3 = obj4;
			}
			NodeGraph nodeGraph = graph;
			if ((object)graph == null)
			{
				goto IL_0478;
			}
			nint num = (nint)typeof(MissionGraph);
			nint num2 = (nint)nodeGraph;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v834 @ r8_v28 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v835 @ r9_v19 (Il2CppClass<SleepyNodes.NodeGraph>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v834 @ r8_v28 (Il2CppClass<SleepyNodes.MissionGraph>)+130]");
			object obj7;
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v835 @ r9_v19 (Il2CppClass<SleepyNodes.NodeGraph>)+C8]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ rax_v85+FFFFFFF8+v836 @ rax_v80*8]");
				if (0 == (nint)typeof(MissionGraph))
				{
					obj7 = 1;
					goto IL_04a8;
				}
			}
			obj7 = 0;
			goto IL_04a8;
			IL_04c0:
			MapEntity mapEntity = FireMission._003CInstance_003Ek__BackingField.CreateMapEntity(id, DisplayName, CS_0024_003C_003E8__locals10.entityIDIndex, (Vector3)missionGraph, (EntityRoles)array2, health, armour, (int)(&x), (MapEntityStates)Role, (string)Health);
			mapEntity.Scale = Scale;
			Func<ShellDefinition, string> selector = _003C_003Ec._003C_003E9__19_1;
			if (_003C_003Ec._003C_003E9__19_1 == null)
			{
				selector = (_003C_003Ec._003C_003E9__19_1 = (ShellDefinition shellDefinition) => (string)(((object)shellDefinition != null) ? ((object)shellDefinition.ShellId) : ((object)new NullReferenceException())));
			}
			IEnumerable<string> source = Enumerable.Select(ImmuneShells, selector);
			List<string> immuneShells = Enumerable.ToList(source);
			mapEntity.ImmuneShells = immuneShells;
			if (!PresetIcon)
			{
				mapEntity.IconRaw = IconRaw;
			}
			FireMission._003CInstance_003Ek__BackingField.RegisterMapEntity(mapEntity);
			bool flag2 = !SetContextVariable;
			lastSpawnedId = CS_0024_003C_003E8__locals10.entityIDIndex;
			if (!flag2)
			{
				state.Set(LastSpawnedEntity, mapEntity);
			}
			obj2++;
			flag3 = (nint)obj2 < NumberToSpawn;
			x = location.x;
			rectTransform = (RectTransform)CS_0024_003C_003E8__locals10.entityIDIndex;
			state2 = state;
			continue;
			IL_04a8:
			if (obj7 == null)
			{
				goto IL_0478;
			}
			goto IL_04c0;
			IL_0478:
			GridReference gridReference = LocationToSpawn.Resolve(FireMission._003CInstance_003Ek__BackingField, null, state2, missionGraph, array2);
			LocationSelection locationToSpawn = LocationToSpawn;
			location = gridReference.GetLocation(array, locationToSpawn.FuzzyLocation);
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			id = $"{ID}#{arg}";
			if (!(Icon != null))
			{
			}
			goto IL_04c0;
		}
		while (flag3);
	}

	public override void OnExecute(NodeExecutionState state)
	{
		//IL_0038: Expected I, but got O
		//IL_0048: Expected O, but got I
		//IL_0058: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A770]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_SpawnMapEntity>)+218]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ r9_v1 (Il2CppClass<SleepyNodes.State_SpawnMapEntity>)+220]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v34 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public State_SpawnMapEntity()
	{
		TextIdentifier displayName = new TextIdentifier();
		DisplayName = displayName;
		PresetIcon = true;
		Role = EntityRoles.Target;
		Health = 1;
		Scale = 1;
		ImmuneShells = new List<ShellDefinition>();
		NumberToSpawn = 1;
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
