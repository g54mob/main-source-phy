using UnityEngine;

public class DisableLocalisation : MonoBehaviour
{
	private void Awake()
	{
		Object.Destroy(this);
	}
}
