using System.Collections;
using UnityEngine;

public class EventSequence : MonoBehaviour
{
	public bool playOnAwake;

	public EventSequenceInstance[] events;

	private DataHandler data;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		if (playOnAwake)
		{
			Go();
		}
	}

	public void Go()
	{
		StartCoroutine(DoEvents());
	}

	private IEnumerator DoEvents()
	{
		for (int i = 0; i < events.Length; i++)
		{
			yield return new WaitForSeconds(events[i].timeBeforeCall);
			if (!data || !data.Dead)
			{
				events[i].eventToCall.Invoke();
				continue;
			}
			break;
		}
	}
}
