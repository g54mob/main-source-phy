using UnityEngine;

public class TwitchTrigger : MonoBehaviour
{
	private TwitchHandler handler;

	public virtual void InputData(string name, string text)
	{
	}

	public virtual bool CheckData()
	{
		return false;
	}

	private void Awake()
	{
		HookupToEvents();
	}

	public void HookupToEvents()
	{
		handler = Object.FindObjectOfType<TwitchHandler>();
		if ((bool)handler)
		{
			handler.OnMessage.AddListener(HandleMessage);
		}
	}

	public virtual void HandleMessage(string name, string text)
	{
	}
}
