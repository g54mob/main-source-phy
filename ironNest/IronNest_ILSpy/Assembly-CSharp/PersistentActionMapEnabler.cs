using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PersistentActionMapEnabler : MonoBehaviour
{
	private InputActionAsset inputActions;

	private string[] persistentMapNames = new string[1] { "Universal" };

	private void Awake()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_004b: Expected O, but got I4
		//IL_0055: Expected O, but got I4
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		if (inputActions != null)
		{
			string[] array = persistentMapNames;
			object obj = persistentMapNames + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < array.Length)
			{
				if (!string.IsNullOrWhiteSpace((string)obj))
				{
					InputActionMap inputActionMap = inputActions.FindActionMap((string)obj);
					if (inputActionMap != null)
					{
						if (!inputActionMap.enabled)
						{
							inputActionMap.Enable();
							obj2++;
							obj += 8;
							obj3 = obj2;
							continue;
						}
					}
					else
					{
						string text = inputActions.name;
						string message = "[PersistentActionMapEnabler] ActionMap \"" + (string)obj + "\" not found in asset \"" + text + "\". Check the name is spelled correctly (case-sensitive).";
						Debug.LogError(message, this);
					}
				}
				obj2++;
				obj += 8;
				obj3 = obj2;
			}
		}
		else
		{
			Debug.LogError("[PersistentActionMapEnabler] Input Actions asset is not assigned. No maps will be enabled.", this);
		}
	}
}
