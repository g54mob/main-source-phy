using UnityEngine;

public class DestroySelf : MonoBehaviour
{
	private void Awake()
	{
		Object.Destroy(this);
	}
}
