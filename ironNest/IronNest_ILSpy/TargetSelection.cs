using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

[Serializable]
public class TargetSelection
{
	public enum SourceTypes
	{
		FromContext,
		FromFilter
	}

	public enum CountTypes
	{
		All,
		Count
	}

	public enum SortTypes
	{
		First,
		Random,
		DistanceFromEntity,
		DistanceFromLocation
	}

	private sealed class _003C_003Ec__DisplayClass11_0
	{
		public TargetSelection _003C_003E4__this;

		public StateNode.NodeExecutionState state;

		internal bool _003CResolve_003Eb__0(MapEntity x)
		{
			//IL_007a: Expected I4, but got O
			TargetSelection targetSelection = _003C_003E4__this;
			if (_003C_003E4__this != null && targetSelection.Filter != null)
			{
				return targetSelection.Filter.Resolve(x, state);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass11_1
	{
		public System.Random rng;

		internal int _003CResolve_003Eb__1(MapEntity x)
		{
			//IL_0049: Expected I4, but got O
			//IL_0031: Expected I, but got O
			System.Random random = rng;
			if (rng != null)
			{
				nint num = (nint)random;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<System.Random>)+188] (should have been resolved before IL gen)");
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	private sealed class _003C_003Ec__DisplayClass11_2
	{
		public Vector3 refPos;

		internal float _003CResolve_003Eb__2(MapEntity x)
		{
			//IL_0031: Expected O, but got I
			object obj = x.Position - refPos;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [x @ rdx (MapEntity)+4C]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TargetSelection+<>c__DisplayClass11_2)+18]");
			object obj2 = num - 0;
			object obj4 = default(object);
			object obj3 = obj4 - obj4;
			object obj5 = obj * obj;
			object obj6 = obj2 * obj2;
			object obj7 = obj3 * obj3;
			object obj8 = obj7 + obj5;
			return (float)obj8 + (float)obj6;
		}
	}

	private sealed class _003C_003Ec__DisplayClass11_3
	{
		public Vector2 localPos;

		internal float _003CResolve_003Eb__3(MapEntity x)
		{
			//IL_001d: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [x @ rdx (MapEntity)+48]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (TargetSelection+<>c__DisplayClass11_3)+14]");
			object obj = num - 0;
			object obj2 = (object)x.Position - (object)localPos;
			object obj3 = obj * obj;
			object obj4 = obj2 * obj2;
			return (float)obj3 + (float)obj4;
		}
	}

	public SourceTypes SourceType;

	public EntityContextKeys ContextKey;

	public FilterEntitySet Filter;

	public CountTypes CountType;

	public int Count = 1;

	public SortTypes SortType;

	public EntityContextKeys DistanceEntityKey;

	public LocationContextKeys DistanceLocationKey;

	public unsafe List<MapEntity> Resolve(FireMission fireMission, StateNode.NodeExecutionState state)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected Ref, but got Unknown
		//IL_023e: Expected O, but got I4
		//IL_0180: Expected O, but got I
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected Ref, but got Unknown
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Expected Ref, but got Unknown
		//IL_0157: Expected O, but got I
		//IL_044f: Expected O, but got I
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected Ref, but got Unknown
		//IL_0469: Expected O, but got I
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Expected O, but got Unknown
		//IL_034e: Expected O, but got I
		//IL_036d: Expected O, but got I
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Expected O, but got Unknown
		//IL_03c8: Expected O, but got I
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_003C_003Ec__DisplayClass11_0 CS_0024_003C_003E8__locals12 = new _003C_003Ec__DisplayClass11_0();
		CS_0024_003C_003E8__locals12._003C_003E4__this = this;
		CS_0024_003C_003E8__locals12.state = state;
		object obj2 = default(object);
		List<MapEntity> result;
		Dictionary<string, MapEntity>.ValueCollection source;
		Func<MapEntity, int> keySelector;
		Func<MapEntity, float> func2;
		if (SourceType == SourceTypes.FromContext)
		{
			Enum obj = (Enum)(obj2 - 24);
			_ = typeof(EntityContextKeys);
			_ = ContextKey;
			_ = -1;
			string key = obj.ToString();
			if (CS_0024_003C_003E8__locals12.state == null)
			{
				return (List<MapEntity>)(object)new NullReferenceException();
			}
			if (CS_0024_003C_003E8__locals12.state.TryGet<MapEntity>(key, out *(MapEntity*)(obj2 - 72)))
			{
				List<MapEntity> list = new List<MapEntity>();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-48]");
				list.Add((MapEntity)0);
				result = list;
				goto IL_018d;
			}
			Enum obj3 = (Enum)(obj2 - 24);
			_ = typeof(EntityContextKeys);
			_ = ContextKey;
			_ = -1;
			string key2 = obj3.ToString();
			if (CS_0024_003C_003E8__locals12.state.TryGet<List<MapEntity>>(key2, out *(List<MapEntity>*)(obj2 - 80)))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-50]");
				return (List<MapEntity>)0;
			}
		}
		else if (SourceType == SourceTypes.FromFilter)
		{
			Dictionary<string, MapEntity>.ValueCollection values = fireMission.Entities.Values;
			bool flag = Filter == null;
			source = values;
			if (!flag)
			{
				Func<MapEntity, bool> predicate = delegate(MapEntity x)
				{
					//IL_007a: Expected I4, but got O
					TargetSelection targetSelection = CS_0024_003C_003E8__locals12._003C_003E4__this;
					if (CS_0024_003C_003E8__locals12._003C_003E4__this == null || targetSelection.Filter == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					return targetSelection.Filter.Resolve(x, CS_0024_003C_003E8__locals12.state);
				};
				IEnumerable<MapEntity> enumerable = Enumerable.Where(values, predicate);
				source = (Dictionary<string, MapEntity>.ValueCollection)enumerable;
			}
			bool flag2 = SortType == SortTypes.First;
			if (!flag2)
			{
				object obj4 = SortType - 1;
				if (flag2)
				{
					_003C_003Ec__DisplayClass11_1 CS_0024_003C_003E8__locals14 = new _003C_003Ec__DisplayClass11_1();
					System.Random rng = new System.Random();
					CS_0024_003C_003E8__locals14.rng = rng;
					Func<MapEntity, int> func = delegate
					{
						//IL_0049: Expected I4, but got O
						//IL_0031: Expected I, but got O
						System.Random rng2 = CS_0024_003C_003E8__locals14.rng;
						if (CS_0024_003C_003E8__locals14.rng != null)
						{
							nint num2 = (nint)rng2;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<System.Random>)+188] (should have been resolved before IL gen)");
						}
						NullReferenceException ex = new NullReferenceException();
						return (int)ex;
					};
					keySelector = func;
					goto IL_05b6;
				}
				object obj5 = obj4 - 1;
				if (!flag2)
				{
					if ((nint)obj5 == 1 && CS_0024_003C_003E8__locals12.state.TryGet<GridReference>(DistanceLocationKey, out *(GridReference*)(obj2 - 64)))
					{
						_003C_003Ec__DisplayClass11_3 obj6 = new _003C_003Ec__DisplayClass11_3();
						GameObject gameObject = GameObject.FindWithTag("MissionParent");
						if (gameObject != null)
						{
							Vector3[] array = new Vector3[4];
							object obj7 = obj2 - 48;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-30]");
							((RectTransform)0).GetWorldCorners(array);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-40]");
							Vector3 location = ((GridReference)0).GetLocation(array);
							Vector3 worldPos = (Vector3)(obj2 - 48);
							_ = location.x;
							_ = location.z;
							Vector2 vector = fireMission.ToLocalSpace(worldPos);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-30]");
							obj6.localPos = (Vector2)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-2C]");
							_ = 0;
							func2 = null;
							nint num = 0;
							_003C_003Ec__DisplayClass11_3 obj8 = obj6;
							goto IL_05d4;
						}
					}
				}
				else if (CS_0024_003C_003E8__locals12.state.TryGet<MapEntity>(DistanceEntityKey, out *(MapEntity*)(obj2 - 32)))
				{
					_003C_003Ec__DisplayClass11_2 obj9 = new _003C_003Ec__DisplayClass11_2();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rsp-20]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v38+44]");
					obj9.refPos = (Vector3)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v38+4C]");
					_ = 0;
					func2 = null;
					nint num = 0;
					_003C_003Ec__DisplayClass11_3 obj8 = (_003C_003Ec__DisplayClass11_3)(object)obj9;
					goto IL_05d4;
				}
			}
			goto IL_0597;
		}
		List<MapEntity> list2 = new List<MapEntity>();
		result = list2;
		goto IL_018d;
		IL_018d:
		return result;
		IL_0597:
		if (CountType != CountTypes.All && CountType == CountTypes.Count)
		{
			IEnumerable<MapEntity> enumerable2 = Enumerable.Take(source, Count);
			source = (Dictionary<string, MapEntity>.ValueCollection)enumerable2;
		}
		return Enumerable.ToList(source);
		IL_05b6:
		Dictionary<string, MapEntity>.ValueCollection valueCollection = (Dictionary<string, MapEntity>.ValueCollection)(object)Enumerable.OrderBy(source, keySelector);
		source = valueCollection;
		goto IL_0597;
		IL_05d4:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180873A30");
		keySelector = (Func<MapEntity, int>)(object)func2;
		goto IL_05b6;
	}
}
