using System.Collections;
using UnityEngine;

public class FacilitatorController : MonoBehaviour, IConnectionController
{
	private ExtendedNATHelper natHelper;

	private ulong facilitatorGuid;

	private IEnumerator connectFacilitatorCoroutine;

	public bool DoneTesting { get; private set; }

	public bool IsInitialized { get; private set; }

	public bool IsConnectedToFacilitator
	{
		get
		{
			return natHelper.isConnectedToFacilitator;
		}
	}

	public bool IsConnectingToFacilitator
	{
		get
		{
			return natHelper.isConnectingToFacilitator;
		}
	}

	public ulong FacilitatorGuid
	{
		get
		{
			return facilitatorGuid;
		}
	}

	public void Setup(ExtendedNATHelper natHelper)
	{
		this.natHelper = natHelper;
		natHelper.OnDoneConnectingToFacilitator += OnDoneConnectingToFacilitator;
		if (natHelper.isConnectedToFacilitator)
		{
			facilitatorGuid = natHelper.guid;
			DoneTesting = true;
		}
		else
		{
			connectFacilitatorCoroutine = natHelper.connectToNATFacilitator();
			StartCoroutine(connectFacilitatorCoroutine);
		}
		IsInitialized = true;
	}

	private void OnDoneConnectingToFacilitator(ulong guid)
	{
		if (guid == 0L)
		{
			DoneTesting = true;
			return;
		}
		facilitatorGuid = guid;
		DoneTesting = true;
	}

	private void UnhookNATHelper()
	{
		natHelper.OnDoneConnectingToFacilitator -= OnDoneConnectingToFacilitator;
		natHelper.RemoveAllPortMappings();
	}

	private void OnDestroy()
	{
		if (IsInitialized)
		{
			UnhookNATHelper();
		}
	}

	public void Retest()
	{
		StartCoroutine(IERetest());
	}

	private IEnumerator IERetest()
	{
		if (connectFacilitatorCoroutine != null)
		{
			StopCoroutine(connectFacilitatorCoroutine);
		}
		natHelper.DisconnectFromFacilitator(100u, true);
		yield return new WaitForSeconds(0.2f);
		connectFacilitatorCoroutine = natHelper.connectToNATFacilitator();
		natHelper.StartCoroutine(connectFacilitatorCoroutine);
	}
}
