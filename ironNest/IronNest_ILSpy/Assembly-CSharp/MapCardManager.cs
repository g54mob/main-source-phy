using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;

public class MapCardManager : MonoBehaviour
{
	public OperationGraph Campaign;

	public bool ForceAllShown;

	public List<MapCard> MapCards;

	public unsafe void UpdateMapCards()
	{
		//IL_0092: Expected O, but got Ref
		if (MapCards != null)
		{
			List<MapCard> mapCards = MapCards;
			if (mapCards._size > 0)
			{
				goto IL_0061;
			}
		}
		MapCard[] componentsInChildren = GetComponentsInChildren<MapCard>(includeInactive: true);
		List<MapCard> mapCards2 = Enumerable.ToList(componentsInChildren);
		MapCards = mapCards2;
		goto IL_0061;
		IL_0061:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MapCard>.Enumerator enumerator = default(List<MapCard>.Enumerator);
		MapCard mapCard = default(MapCard);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = (object)mapCard == null;
				List<MapCard> list = (List<MapCard>)(&enumerator);
				if (flag)
				{
					break;
				}
				mapCard.Campaign = Campaign;
				mapCard.Init(mapCard.Mission);
				continue;
			}
			enumerator.Dispose();
			if (ForceAllShown)
			{
				ForceRevealAll();
			}
			return;
		}
		throw new NullReferenceException();
	}

	public unsafe void ForceRevealAll()
	{
		//IL_0036: Expected O, but got Ref
		//IL_0055: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MapCard>.Enumerator enumerator = default(List<MapCard>.Enumerator);
		object obj = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				UnityEvent unityEvent = (UnityEvent)(&enumerator);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ stack_8_v3+58]");
				((UnityEvent)0).Invoke();
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public unsafe void ForceRevealAllComplete()
	{
		//IL_0036: Expected O, but got Ref
		//IL_0055: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<MapCard>.Enumerator enumerator = default(List<MapCard>.Enumerator);
		object obj = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				UnityEvent unityEvent = (UnityEvent)(&enumerator);
				if (flag)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ stack_8_v3+60]");
				((UnityEvent)0).Invoke();
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public MapCardManager()
	{
		List<MapCard> mapCards = new List<MapCard>();
		MapCards = mapCards;
		base._002Ector();
	}
}
