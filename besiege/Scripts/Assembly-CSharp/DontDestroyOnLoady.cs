using UnityEngine;

public class DontDestroyOnLoady : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.transform.gameObject);
	}
}
