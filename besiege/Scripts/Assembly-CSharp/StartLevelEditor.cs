using UnityEngine;

public class StartLevelEditor : ClickBehaviour
{
	public bool triggerOnStart;

	public bool registerMouse = true;

	private bool wasTriggered;

	private void Awake()
	{
		if (triggerOnStart)
		{
			LoadLevel();
		}
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (registerMouse)
		{
			AudioSource component = GetComponent<AudioSource>();
			if (component != null)
			{
				component.Play();
			}
		}
	}

	public override void OnClickReleased()
	{
		if (registerMouse)
		{
			AudioSource component = GetComponent<AudioSource>();
			if (component != null)
			{
				component.Play();
			}
			LoadLevel();
		}
	}

	public void LoadLevel()
	{
		if (!wasTriggered)
		{
			Arguments args = new Arguments(new string[3] { "+dedicated", "3", "+leveleditor_only" });
			BesiegeEntryPoint.CreateEntryPoint(args);
			wasTriggered = true;
		}
	}
}
