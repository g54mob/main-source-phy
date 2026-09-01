using UnityEngine;

public class UnlockSceneObjectRelay : MonoBehaviour
{
	[SerializeField]
	private string _objectId;

	[SerializeField]
	private bool _refreshImmediately;

	[SerializeField]
	private bool _requireMissionComplete;

	public void UnlockSceneObject()
	{
	}

	private bool CanBeUnlocked()
	{
		return false;
	}
}
