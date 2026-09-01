using Cpp2ILInjected;
using UnityEngine;

public class PrinterAlertDismisser : MonoBehaviour
{
	public PrinterAlertSystem alertSystem;

	public string playerTag;

	private void OnTriggerEnter(Collider other)
	{
		if (alertSystem != null)
		{
			if (other.CompareTag(playerTag))
			{
				alertSystem.DismissAllAlerts();
			}
		}
		else
		{
			Debug.LogWarning("[PrinterAlertDismisser] No PrinterAlertSystem assigned.", this);
		}
	}

	public PrinterAlertDismisser()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A421]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		playerTag = "Player";
		base._002Ector();
	}
}
