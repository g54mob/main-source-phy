using UnityEngine;

public class EnableOnSteam : MonoBehaviour
{
	private void Awake()
	{
		base.gameObject.SetActive(true);
	}
}
