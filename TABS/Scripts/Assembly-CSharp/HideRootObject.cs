using UnityEngine;

public class HideRootObject : MonoBehaviour
{
	public void Hide()
	{
		base.transform.root.gameObject.SetActive(value: false);
	}
}
