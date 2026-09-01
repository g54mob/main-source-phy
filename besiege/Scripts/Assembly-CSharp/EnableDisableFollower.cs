using UnityEngine;

public class EnableDisableFollower : MonoBehaviour
{
	public GameObject[] objects;

	public bool inverse;

	private bool deactivate = true;

	protected void OnEnable()
	{
		Toggle(true);
	}

	protected void OnDestroy()
	{
		deactivate = false;
	}

	protected void OnDisable()
	{
		if (deactivate)
		{
			Toggle(false);
		}
	}

	private void Toggle(bool toggle)
	{
		if (inverse)
		{
			toggle = !toggle;
		}
		for (int i = 0; i < objects.Length; i++)
		{
			GameObject gameObject = objects[i];
			if (gameObject != null && gameObject.activeSelf != toggle)
			{
				gameObject.SetActive(toggle);
			}
		}
	}
}
