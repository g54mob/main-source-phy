using UnityEngine;

public class DontDestroyOnAwake : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
