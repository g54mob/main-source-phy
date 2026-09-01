using System.Collections;
using UnityEngine;

public class ActiveEntityWidget : MonoBehaviour
{
	private IEnumerator OnMouseEnter()
	{
		if (!InputManager.LeftMouseButtonHeld())
		{
			yield return null;
			ServerHealth.countDirty = true;
		}
	}
}
