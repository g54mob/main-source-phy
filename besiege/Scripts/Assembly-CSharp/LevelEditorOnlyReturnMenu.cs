using UnityEngine;

public class LevelEditorOnlyReturnMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject disconnectButtonObject;

	[SerializeField]
	private Transform objectToOffset;

	[SerializeField]
	private float yOffset = 0.78f;

	private void OnEnable()
	{
		bool isLevelEditorOnly = StatMaster.IsLevelEditorOnly;
		Vector3 localPosition = objectToOffset.localPosition;
		if (isLevelEditorOnly)
		{
			disconnectButtonObject.SetActive(false);
			localPosition.y = yOffset;
		}
		else
		{
			disconnectButtonObject.SetActive(true);
			localPosition.y = 0f;
		}
		objectToOffset.localPosition = localPosition;
	}
}
