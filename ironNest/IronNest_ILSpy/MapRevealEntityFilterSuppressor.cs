using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

public class MapRevealEntityFilterSuppressor : MonoBehaviour
{
	public GameObject[] RevealVisuals;

	public bool DisableIfAnyEntityMatches;

	private bool _003CRevealVisualsBlocked_003Ek__BackingField;

	public FilterEntitySet EntityFilter;

	public bool RevealVisualsBlocked
	{
		get
		{
			return _003CRevealVisualsBlocked_003Ek__BackingField;
		}
		private set
		{
			_003CRevealVisualsBlocked_003Ek__BackingField = value;
		}
	}

	private void OnEnable()
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		_003CRevealVisualsBlocked_003Ek__BackingField = false;
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		Dictionary<string, MapEntity>.ValueCollection valueCollection;
		if ((object)FireMission._003CInstance_003Ek__BackingField != null && fireMission.Entities != null)
		{
			Dictionary<string, MapEntity>.ValueCollection values = fireMission.Entities.Values;
			valueCollection = values;
		}
		else
		{
			valueCollection = null;
		}
		if (!DisableIfAnyEntityMatches || valueCollection == null)
		{
			return;
		}
		Func<MapEntity, bool> predicate = delegate(MapEntity x)
		{
			//IL_0052: Expected I4, but got O
			StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
			if (EntityFilter == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return EntityFilter.Resolve(x, newState);
		};
		if (!Enumerable.Any(valueCollection, predicate))
		{
			return;
		}
		_003CRevealVisualsBlocked_003Ek__BackingField = true;
		if (RevealVisuals == null)
		{
			return;
		}
		GameObject[] revealVisuals = RevealVisuals;
		object obj = RevealVisuals + 32;
		Dictionary<string, MapEntity>.ValueCollection valueCollection2 = null;
		while ((nint)valueCollection2 < revealVisuals.Length)
		{
			if ((UnityEngine.Object)obj != null)
			{
				((GameObject)obj).SetActive(false);
			}
			valueCollection2 = (Dictionary<string, MapEntity>.ValueCollection)(valueCollection2 + 1);
			obj += 8;
		}
	}

	public void EvaluateEntityFilter()
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		_003CRevealVisualsBlocked_003Ek__BackingField = false;
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		Dictionary<string, MapEntity>.ValueCollection valueCollection;
		if ((object)FireMission._003CInstance_003Ek__BackingField != null && fireMission.Entities != null)
		{
			Dictionary<string, MapEntity>.ValueCollection values = fireMission.Entities.Values;
			valueCollection = values;
		}
		else
		{
			valueCollection = null;
		}
		if (!DisableIfAnyEntityMatches || valueCollection == null)
		{
			return;
		}
		Func<MapEntity, bool> predicate = delegate(MapEntity x)
		{
			//IL_0052: Expected I4, but got O
			StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
			if (EntityFilter == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			return EntityFilter.Resolve(x, newState);
		};
		if (!Enumerable.Any(valueCollection, predicate))
		{
			return;
		}
		_003CRevealVisualsBlocked_003Ek__BackingField = true;
		if (RevealVisuals == null)
		{
			return;
		}
		GameObject[] revealVisuals = RevealVisuals;
		object obj = RevealVisuals + 32;
		Dictionary<string, MapEntity>.ValueCollection valueCollection2 = null;
		while ((nint)valueCollection2 < revealVisuals.Length)
		{
			if ((UnityEngine.Object)obj != null)
			{
				((GameObject)obj).SetActive(false);
			}
			valueCollection2 = (Dictionary<string, MapEntity>.ValueCollection)(valueCollection2 + 1);
			obj += 8;
		}
	}

	private void SetRevealVisualsActive(bool active)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0028: Expected O, but got I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		if (RevealVisuals == null)
		{
			return;
		}
		GameObject[] revealVisuals = RevealVisuals;
		object obj = RevealVisuals + 32;
		object obj2 = 0;
		while ((nint)obj2 < revealVisuals.Length)
		{
			if ((UnityEngine.Object)obj != null)
			{
				((GameObject)obj).SetActive(active);
			}
			obj2++;
			obj += 8;
		}
	}

	public MapRevealEntityFilterSuppressor()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00f7: Expected I, but got O
		//IL_0107: Expected O, but got I
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		DisableIfAnyEntityMatches = true;
		FilterEntitySet filterEntitySet = new FilterEntitySet();
		FilterEntitySet.FilterEntityPair[] array = new FilterEntitySet.FilterEntityPair[1];
		FilterEntitySet.FilterEntityPair filterEntityPair = new FilterEntitySet.FilterEntityPair();
		bool flag = filterEntityPair == null;
		FilterEntity filterEntity = null;
		FilterEntitySet.FilterEntityPair filterEntityPair2 = filterEntityPair;
		if (!flag)
		{
			filterEntityPair.operation = FilterEntitySet.FilterEntityPair.Operation.Base;
			FilterEntity filterEntity2 = new FilterEntity();
			bool flag2 = filterEntity2 == null;
			filterEntity = null;
			filterEntityPair2 = (FilterEntitySet.FilterEntityPair)(object)filterEntity2;
			if (!flag2)
			{
				filterEntity2.FilterEntityType = FilterEntity.FilterEntityTypes.Role;
				filterEntityPair2 = (FilterEntitySet.FilterEntityPair)(filterEntityPair + 24);
				filterEntity2.Operation = FilterEntity.OperationTypes.Contains;
				filterEntity2.RoleValue = EntityRoles.AABattery;
				filterEntityPair.FilterEntity = filterEntity2;
				bool flag3 = array == null;
				filterEntity = filterEntity2;
				if (!flag3)
				{
					nint num = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rdx_v10 (Il2CppClass<FilterEntityPair[]>)+40]");
					filterEntity = (FilterEntity)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag4 = obj == null;
					filterEntityPair2 = filterEntityPair;
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj2 = default(object);
						throw obj2;
					}
					filterEntityPair2 = (FilterEntitySet.FilterEntityPair)(array + 32);
					array[0] = filterEntityPair;
					bool flag5 = filterEntitySet == null;
					filterEntity = (FilterEntity)(object)filterEntityPair;
					if (!flag5)
					{
						filterEntitySet.FilterEntitys = array;
						EntityFilter = filterEntitySet;
						base._002Ector();
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private bool _003CEvaluateEntityFilter_003Eb__8_0(MapEntity x)
	{
		//IL_0052: Expected I4, but got O
		StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
		if (EntityFilter != null)
		{
			return EntityFilter.Resolve(x, newState);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
