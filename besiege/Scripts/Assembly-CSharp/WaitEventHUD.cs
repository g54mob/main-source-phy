using System.Collections;
using UnityEngine;

public class WaitEventHUD : MonoBehaviour
{
	public TextMesh timer;

	public GameObject[] icons;

	public Renderer background;

	protected WaitHUDManager manager;

	protected EventContainer.WaitEvent myEvent;

	protected Coroutine coroutine;

	protected Coroutine bounce;

	protected Coroutine move;

	protected Coroutine crush;

	protected bool isDestroyed;

	public float CurrentTime
	{
		get
		{
			return myEvent.waitTime * (1f - myEvent.GetProgress());
		}
	}

	public EventContainer.WaitEvent Event
	{
		get
		{
			return myEvent;
		}
	}

	public void Setup(EventContainer.WaitEvent evnt, Vector3 position, WaitHUDManager m)
	{
		myEvent = evnt;
		SetActiveIcon(myEvent.icon);
		manager = m;
		base.gameObject.SetActive(true);
		Bounce(position);
		coroutine = StartCoroutine(UpdateText());
	}

	public void UpdatePos(Vector3 position)
	{
		if (isDestroyed)
		{
			Debug.LogError("trying to update a destroyed thing");
			return;
		}
		if (move != null)
		{
			StopCoroutine(move);
		}
		move = StartCoroutine(IELerpPos(position));
	}

	protected IEnumerator IELerpPos(Vector3 position)
	{
		float duration = 0.1f;
		Vector3 startPos = base.transform.position;
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float percentage = t / duration;
			base.transform.position = new Vector3(Mathf.Lerp(startPos.x, position.x, percentage), base.transform.position.y, base.transform.position.z);
			yield return null;
		}
		base.transform.position = new Vector3(position.x, base.transform.position.y, base.transform.position.z);
	}

	public void Terminate(bool anim = true)
	{
		if (isDestroyed)
		{
			Debug.LogError("trying to destroy a destroyed thing");
		}
		else if (crush == null)
		{
			if (anim)
			{
				crush = StartCoroutine(IECrush());
			}
			else
			{
				_Terminate();
			}
		}
	}

	protected IEnumerator IECrush()
	{
		float duration = 0.05f;
		Vector3 start = base.transform.localScale;
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float percentage = t / duration;
			base.transform.localScale = new Vector3(Mathf.Lerp(start.x, 0f, percentage), base.transform.localScale.y, base.transform.localScale.z);
			yield return null;
		}
		base.transform.localScale = new Vector3(0f, base.transform.localScale.y, base.transform.localScale.z);
		_Terminate();
	}

	protected void _Terminate()
	{
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}
		if (bounce != null)
		{
			StopCoroutine(bounce);
		}
		if (move != null)
		{
			StopCoroutine(move);
		}
		isDestroyed = true;
		Object.Destroy(base.gameObject);
	}

	protected void Bounce(Vector3 position)
	{
		bounce = StartCoroutine(IEBounce(position));
	}

	protected IEnumerator IEBounce(Vector3 position)
	{
		float duration = 0.15f;
		Vector3 vector = position + Vector3.up * 1f;
		base.transform.position = vector;
		Vector3 startPos = vector;
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float percentage = t / duration;
			base.transform.position = new Vector3(base.transform.position.x, Mathf.Lerp((startPos.y + base.transform.position.y) / 2f, position.y - 0.2f, percentage), base.transform.position.z);
			yield return null;
		}
		duration = 0.1f;
		for (float t2 = 0f; t2 < duration; t2 += Time.unscaledDeltaTime)
		{
			float percentage2 = t2 / duration;
			base.transform.position = new Vector3(base.transform.position.x, Mathf.Lerp(base.transform.position.y, position.y, percentage2), base.transform.position.z);
			yield return null;
		}
		base.transform.position = new Vector3(base.transform.position.x, position.y, base.transform.position.z);
	}

	protected void SetActiveIcon(int t)
	{
		for (int i = 0; i < icons.Length; i++)
		{
			icons[i].SetActive(i == t);
		}
	}

	protected IEnumerator UpdateText()
	{
		float currentTime = CurrentTime;
		while (!myEvent.isDone)
		{
			currentTime = CurrentTime;
			int minutes = Mathf.FloorToInt(currentTime / 60f);
			int seconds = Mathf.RoundToInt(currentTime - (float)minutes * 60f);
			if (seconds > 9)
			{
				timer.text = minutes + ":" + seconds;
			}
			else
			{
				timer.text = minutes + ":0" + seconds;
			}
			yield return null;
		}
		timer.text = "0:00";
		yield return new WaitForSecondsRealtime(0.1f);
		manager.RemoveElement(myEvent);
	}
}
