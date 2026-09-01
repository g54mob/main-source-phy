using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class PrinterAlertDismisser : MonoBehaviour
{
	[Tooltip("The PrinterAlertSystem on this printer prefab to dismiss when the player enters.\nDrag the printer root (or whichever GameObject holds PrinterAlertSystem) here.")]
	public PrinterAlertSystem alertSystem;

	[Tooltip("Tag the entering GameObject must have to trigger a dismissal.\nMust match exactly — tags are case-sensitive. Default: 'Player'.")]
	public string playerTag;

	private void OnTriggerEnter(Collider other)
	{
	}
}
