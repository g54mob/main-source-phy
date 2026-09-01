using System.Collections.Generic;
using UnityEngine;

public class RealtimeUpdater : MonoBehaviour
{
	public List<Transform> boxes = new List<Transform>();

	public int activeBox;

	public float spaceBetween;

	public float lerpSpeed;

	public static RealtimeUpdater Instance;

	private float startPos;

	private bool allFaded;

	private bool updatePosition;

	private Queue<BoxEntry> entries = new Queue<BoxEntry>();

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		startPos = boxes[0].localPosition.x;
		for (int i = 0; i < boxes.Count; i++)
		{
			entries.Enqueue(new BoxEntry(boxes[i]));
		}
	}

	private void Update()
	{
		if (updatePosition)
		{
			int num = entries.Count - 1;
			foreach (BoxEntry entry in entries)
			{
				Transform transform = entry.transform;
				Vector3 localPosition = transform.localPosition;
				float b = startPos + (float)num * spaceBetween;
				float num2 = Mathf.Lerp(localPosition.x, b, TimeSlider.Instance.deltaTime * lerpSpeed);
				transform.localPosition = new Vector3(num2, localPosition.y, localPosition.z);
				if (num == 0 && Mathf.Approximately(num2, b))
				{
					updatePosition = false;
				}
				num--;
			}
		}
		if (!StatMaster.levelSimulating)
		{
			if (!allFaded)
			{
				FadeAllOut();
			}
		}
		else
		{
			allFaded = false;
		}
	}

	public void AddBox(string shortName, string fullName, InjuryType info, string text)
	{
		BoxEntry boxEntry = entries.Dequeue();
		boxEntry.transform.localPosition = new Vector3(startPos - spaceBetween, boxEntry.transform.localPosition.y, boxEntry.transform.localPosition.z);
		boxEntry.infoBoxController.SetInfo(shortName, fullName, info, text);
		boxEntry.infoBoxController.FadeIn();
		entries.Enqueue(boxEntry);
		entries.Peek().infoBoxController.FadeOut();
		updatePosition = true;
	}

	public void AddBox(string shortName, string fullName, InjuryType info)
	{
		BoxEntry boxEntry = entries.Dequeue();
		boxEntry.transform.localPosition = new Vector3(startPos - spaceBetween, boxEntry.transform.localPosition.y, boxEntry.transform.localPosition.z);
		boxEntry.infoBoxController.SetInfo(shortName, fullName, info);
		boxEntry.infoBoxController.FadeIn();
		entries.Enqueue(boxEntry);
		entries.Peek().infoBoxController.FadeOut();
		updatePosition = true;
	}

	private void FadeAllOut()
	{
		foreach (BoxEntry entry in entries)
		{
			entry.infoBoxController.FadeOut();
		}
		allFaded = true;
	}
}
