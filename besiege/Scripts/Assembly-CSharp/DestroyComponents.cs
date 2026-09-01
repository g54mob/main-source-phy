using UnityEngine;

public class DestroyComponents : MonoBehaviour
{
	public Component[] components;

	private void Awake()
	{
		for (int i = 0; i < components.Length; i++)
		{
			Object.Destroy(components[i]);
		}
	}
}
