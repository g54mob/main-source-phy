using UnityEngine;

public class EditorTransformButton : MonoBehaviour
{
	public GameObject background;

	public bool offOnAwake = true;

	protected void Awake()
	{
		if (offOnAwake)
		{
			Toggle(false);
		}
	}

	public void Toggle(bool toggle)
	{
		background.SetActive(toggle);
	}
}
