using UnityEngine;

public class GlobalDefaultKeyMaps : MonoBehaviour
{
	public static string[] KeyMaps1;

	public static string[] KeyMaps2;

	public string[] keyMaps1;

	public string[] keyMaps2;

	private void Awake()
	{
		KeyMaps1 = keyMaps1;
		KeyMaps2 = keyMaps2;
	}
}
