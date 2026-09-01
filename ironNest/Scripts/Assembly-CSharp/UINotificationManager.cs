using UnityEngine;

[DisallowMultipleComponent]
public class UINotificationManager : MonoBehaviour
{
	[Header("References")]
	public Transform notificationRoot;

	public UINotification notificationPrefab;

	[Header("Defaults")]
	public float defaultLifetime;

	public Color defaultBorderColor;

	public static UINotificationManager Instance { get; private set; }

	public static bool HasInstance => false;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public static UINotification ShowNotification(string title, string description, float lifetime = -1f, Color? borderColor = null)
	{
		return null;
	}

	private UINotification Spawn(string title, string description, float lifetime, Color? borderColor)
	{
		return null;
	}
}
