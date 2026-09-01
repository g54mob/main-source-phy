using UnityEngine;

[AddComponentMenu("Levels/Set Level To Load")]
public class SetObjectiveText : MonoBehaviour
{
	public string nextLevelName;

	private StartGameButton nextLevelCode;

	private void Start()
	{
		if (SingleInstanceFindOnly<WinScreen>.Instance != null)
		{
			SingleInstanceFindOnly<WinScreen>.Instance.SetZoneToLoad(nextLevelName);
		}
	}
}
