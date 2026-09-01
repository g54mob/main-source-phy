using UnityEngine;

public class UnlockableSceneObject : MonoBehaviour
{
	public string ObjectID;

	private void Awake()
	{
		RefreshState();
	}

	public void Unlock()
	{
		if (!string.IsNullOrEmpty(ObjectID))
		{
			if (ProgressionManager._003CInstance_003Ek__BackingField != null)
			{
				bool flag = ProgressionManager._003CInstance_003Ek__BackingField.UnlockSceneObject(ObjectID);
				ProgressionManager._003CInstance_003Ek__BackingField.SaveProgression();
				GameObject gameObject = base.gameObject;
				gameObject.SetActive(value: true);
			}
			else
			{
				string message = "[UnlockableSceneObject] ProgressionManager missing for '" + ObjectID + "'.";
				Debug.LogWarning(message, this);
				GameObject gameObject2 = base.gameObject;
				gameObject2.SetActive(value: true);
			}
		}
		else
		{
			string text = base.name;
			string message2 = "[UnlockableSceneObject] '" + text + "' has no ObjectID.";
			Debug.LogWarning(message2, this);
		}
	}

	public void RefreshState()
	{
		string text;
		string text2;
		string text3;
		if (!string.IsNullOrEmpty(ObjectID))
		{
			if (!(ProgressionManager._003CInstance_003Ek__BackingField == null))
			{
				GameObject gameObject = base.gameObject;
				bool active = ProgressionManager._003CInstance_003Ek__BackingField.IsSceneObjectUnlocked(ObjectID);
				gameObject.SetActive(active);
				return;
			}
			text = ObjectID;
			text2 = "'.";
			text3 = "[UnlockableSceneObject] ProgressionManager missing for '";
		}
		else
		{
			string text4 = base.name;
			text = text4;
			text2 = "' has no ObjectID.";
			text3 = "[UnlockableSceneObject] '";
		}
		string message = text3 + text + text2;
		Debug.LogWarning(message, this);
		GameObject gameObject2 = base.gameObject;
		gameObject2.SetActive(value: true);
	}

	public static void RefreshAll()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_003d: Expected O, but got I4
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		UnlockableSceneObject[] array = Object.FindObjectsByType<UnlockableSceneObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < array.Length)
		{
			((UnlockableSceneObject)obj).RefreshState();
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}
}
