using UnityEngine;

public class DisableGameObjects : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Objects to disable in Awake.")]
	protected GameObject[] disableOnAwake;

	[SerializeField]
	[Tooltip("Objects to disable in Start.")]
	protected GameObject[] disableOnStart;

	private void Awake()
	{
		DisableObjects(disableOnAwake);
	}

	private void Start()
	{
		DisableObjects(disableOnStart);
	}

	private void DisableObjects(GameObject[] objects)
	{
		if (objects == null || objects.Length == 0)
		{
			return;
		}
		int i = 0;
		for (int num = objects.Length; i < num; i++)
		{
			GameObject gameObject = objects[i];
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
	}
}
