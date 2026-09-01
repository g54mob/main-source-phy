using UnityEngine;
using UnityEngine.Events;

public class DlcCheck : MonoBehaviour
{
	[SerializeField]
	private uint dlcAppId;

	[SerializeField]
	private UnityEvent dlcNotInstalledEvent;

	[SerializeField]
	private UnityEvent dlcInstalledEvent;

	private void Start()
	{
	}

	public bool CheckDlcStatus()
	{
		return false;
	}
}
