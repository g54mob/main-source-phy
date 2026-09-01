using UnityEngine;

public class UnlockableSceneObject : MonoBehaviour
{
	public string ObjectID;

	private void Awake()
	{
	}

	[Button(null)]
	public void Unlock()
	{
	}

	[Button(null)]
	public void RefreshState()
	{
	}

	public static void RefreshAll()
	{
	}
}
