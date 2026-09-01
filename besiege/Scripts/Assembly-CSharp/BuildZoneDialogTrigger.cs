using System.Collections;
using UnityEngine;

public class BuildZoneDialogTrigger : MonoBehaviour
{
	public BuildZoneObject zoneObject;

	private SetBuildZoneDialog zoneDialog;

	private bool isInitialized;

	private NetworkAddPiece addPiece;

	public void UpdateTeam(MPTeam team)
	{
		zoneDialog.UpdateTeam(zoneObject, team);
	}

	protected void Awake()
	{
		zoneDialog = SetBuildZoneDialog.Instance;
		isInitialized = true;
	}

	protected void Start()
	{
		addPiece = NetworkAddPiece.Instance;
	}

	public void ShowDialog()
	{
		if (isInitialized)
		{
			zoneDialog.SetZone(zoneObject);
			zoneDialog.UpdateTeam(zoneObject, zoneObject.Team);
		}
	}

	public void HideDialog()
	{
		if (isInitialized)
		{
			zoneDialog.Cancel(zoneObject);
		}
	}

	protected void OnMouseEnter()
	{
		if (!zoneObject.hasZone && !StatMaster.levelSimulating)
		{
			ShowDialog();
		}
	}

	protected IEnumerator OnMouseExit()
	{
		yield return null;
		if (!addPiece.hudOccluding)
		{
			HideDialog();
		}
	}
}
