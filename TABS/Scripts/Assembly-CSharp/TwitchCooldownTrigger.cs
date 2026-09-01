using UnityEngine;

public class TwitchCooldownTrigger : TwitchTrigger
{
	public string EventString = "DefaultEventString";

	public float Cooldown = 15f;

	private float cooldown;

	private string currText;

	private string currName;

	private bool hasData;

	public TwitchAction Action;

	private void Update()
	{
		if (cooldown >= 0f && !hasData)
		{
			cooldown -= Time.deltaTime;
		}
		if (CheckData())
		{
			Action.RunAction(currName, currText);
		}
	}

	public override void InputData(string name, string text)
	{
		if (cooldown <= 0f && !hasData && text.ToLower() == EventString.ToLower())
		{
			hasData = true;
			currName = name;
			currText = text;
			cooldown = Cooldown;
		}
	}

	public override bool CheckData()
	{
		if (hasData)
		{
			hasData = false;
			return true;
		}
		return false;
	}

	public override void HandleMessage(string name, string text)
	{
		InputData(name, text);
	}
}
