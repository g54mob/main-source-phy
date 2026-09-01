using System.Collections.Generic;

public class TriggerCallbackManager
{
	public static bool doDelayAndSortTriggerEvents = true;

	public static List<TriggerEventInfo> eventsFromLastFrame = new List<TriggerEventInfo>();

	internal static bool sameElementComparision = false;

	public static void OnEnterSim()
	{
		eventsFromLastFrame.Clear();
	}

	public static void OnExitSim()
	{
		eventsFromLastFrame.Clear();
	}

	public static void OnStartSimulation()
	{
		eventsFromLastFrame.Clear();
	}

	public static void Restore()
	{
		eventsFromLastFrame.Clear();
	}

	public static void DestroyAll()
	{
		eventsFromLastFrame.Clear();
	}

	public static void SortAndProcessTriggerEvents()
	{
		if (!doDelayAndSortTriggerEvents)
		{
			return;
		}
		for (int num = eventsFromLastFrame.Count - 1; num >= 0; num--)
		{
			TriggerEventInfo triggerEventInfo = eventsFromLastFrame[num];
			if (!triggerEventInfo.triggerHandler.asObject || !triggerEventInfo.other)
			{
				eventsFromLastFrame.RemoveAt(num);
			}
		}
		eventsFromLastFrame.Sort();
		foreach (TriggerEventInfo item in eventsFromLastFrame)
		{
			item.triggerHandler.DoOnTriggerStay(item.other, item.isOnEnter);
		}
		eventsFromLastFrame.Clear();
	}
}
