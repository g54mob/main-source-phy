using UnityEngine;

public class LoadInitializer : MonoBehaviour
{
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
