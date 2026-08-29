using UnityEngine;

public class DebugLogState : MonoBehaviour
{
	public bool alsoLogOnClones = true;

	private void OnEnable()
	{
		logIfPossible("<color=green>+OnEnable " + base.name + ": " + getGameName() + "</color> \r\n " + getParentName());
	}

	private void OnDisable()
	{
		logIfPossible("<color=orange>-OnDisable " + base.name + ": " + getGameName() + "</color> \r\n " + getParentName());
	}

	private void OnDestroy()
	{
		logIfPossible("<color=red>-OnDestroy " + base.name + ": " + getGameName() + "</color> \r\n " + getParentName());
	}

	private void OnTransformParentChanged()
	{
		logIfPossible("<color=cyan>OnTransformParentChanged " + base.name + ": " + getGameName() + "</color> \r\n " + getParentName());
	}

	private void logIfPossible(string message)
	{
		if (alsoLogOnClones || !base.name.Contains("(Clone)"))
		{
			message = Time.frameCount + ": " + message;
			Debug.Log(message, base.gameObject);
		}
	}

	private string getParentName()
	{
		if (base.transform.parent != null)
		{
			return "parent: " + base.transform.parent.name;
		}
		return "no parent";
	}

	private string getGameName()
	{
		GameObject[] rootGameObjects = base.gameObject.scene.GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			Game componentInChildren = rootGameObjects[i].GetComponentInChildren<Game>();
			if (componentInChildren != null)
			{
				return componentInChildren.name;
			}
		}
		return "";
	}
}
