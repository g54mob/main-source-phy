using UnityEngine;

public class DestroyWhenSceneObjectUnlocked : MonoBehaviour
{
	private string _objectId;

	private bool _checkOnStart;

	private void Start()
	{
		if (_checkOnStart && ProgressionManager._003CInstance_003Ek__BackingField.IsSceneObjectUnlocked(_objectId))
		{
			GameObject obj = base.gameObject;
			Object.Destroy(obj);
		}
	}

	public void Check()
	{
		if (ProgressionManager._003CInstance_003Ek__BackingField.IsSceneObjectUnlocked(_objectId))
		{
			GameObject obj = base.gameObject;
			Object.Destroy(obj);
		}
	}
}
