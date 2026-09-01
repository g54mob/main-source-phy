using UnityEngine;

public class ToggleActive : MonoBehaviour
{
	private GameObject target;

	public void Toggle()
	{
		GameObject gameObject;
		if (target != null)
		{
			gameObject = target;
		}
		else
		{
			GameObject gameObject2 = base.gameObject;
			gameObject = gameObject2;
		}
		bool activeSelf = gameObject.activeSelf;
		bool active = (byte)((activeSelf ? 1u : 0u) ^ 1u) != 0;
		gameObject.SetActive(active);
	}
}
