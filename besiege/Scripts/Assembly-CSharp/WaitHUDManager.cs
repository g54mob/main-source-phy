using System.Collections.Generic;
using UnityEngine;

public class WaitHUDManager : SingleInstanceFindOnly<WaitHUDManager>
{
	public WaitEventHUD prefab;

	public Transform parent;

	public List<WaitEventHUD> elements = new List<WaitEventHUD>();

	public Dictionary<EventContainer.WaitEvent, int> events = new Dictionary<EventContainer.WaitEvent, int>();

	protected Vector3 startPos;

	protected float width;

	protected float px = 0.015f;

	protected bool initd;

	public override string Name
	{
		get
		{
			return "WaitHUDManager";
		}
	}

	protected override void Awake()
	{
		base.Awake();
		ClearList();
		prefab.gameObject.SetActive(false);
	}

	private void Init()
	{
		startPos = prefab.transform.position;
		width = prefab.background.transform.localScale.x + px;
		initd = true;
	}

	public void ClearList()
	{
		elements.Clear();
		events.Clear();
	}

	public void AddElement(EventContainer.WaitEvent evnt)
	{
		if (!initd)
		{
			Init();
		}
		if (events.ContainsKey(evnt))
		{
			Debug.LogWarning("This event is already added to Wait HUD");
			return;
		}
		if (evnt.isDone)
		{
			Debug.LogWarning("This event is already finished no need to add to Wait HUD display, time: " + evnt.waitTime);
			return;
		}
		bool flag = false;
		for (int i = 0; i < elements.Count; i++)
		{
			if (!flag)
			{
				if (evnt.waitTime < elements[i].CurrentTime)
				{
					Vector3 position = startPos + width * (float)i * Vector3.right;
					WaitEventHUD waitEventHUD = Object.Instantiate(prefab, position, Quaternion.identity, parent) as WaitEventHUD;
					waitEventHUD.Setup(evnt, position, this);
					elements.Insert(i, waitEventHUD);
					events.Add(evnt, i);
					flag = true;
				}
			}
			else
			{
				Vector3 position = startPos + width * (float)i * Vector3.right;
				elements[i].UpdatePos(position);
				events.Remove(elements[i].Event);
				events.Add(elements[i].Event, i);
			}
		}
		if (!flag)
		{
			Vector3 position = startPos + width * (float)elements.Count * Vector3.right;
			WaitEventHUD waitEventHUD = Object.Instantiate(prefab, position, Quaternion.identity, parent) as WaitEventHUD;
			waitEventHUD.Setup(evnt, position, this);
			events.Add(evnt, elements.Count);
			elements.Add(waitEventHUD);
		}
	}

	public void ClearAll()
	{
		foreach (EventContainer.WaitEvent item in new List<EventContainer.WaitEvent>(events.Keys))
		{
			RemoveElement(item);
		}
		ClearList();
	}

	public void RemoveElement(EventContainer.WaitEvent evnt)
	{
		if (events.ContainsKey(evnt))
		{
			int num = events[evnt];
			elements[num].Terminate();
			elements.RemoveAt(num);
			events.Remove(evnt);
			for (int i = num; i < elements.Count; i++)
			{
				Vector3 position = startPos + width * (float)i * Vector3.right;
				elements[i].UpdatePos(position);
				events.Remove(elements[i].Event);
				events.Add(elements[i].Event, i);
			}
		}
	}
}
