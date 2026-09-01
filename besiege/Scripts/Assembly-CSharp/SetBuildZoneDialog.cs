using System;
using Localisation;
using UnityEngine;

public class SetBuildZoneDialog : MonoBehaviour
{
	public static SetBuildZoneDialog Instance;

	public Camera hudCamera;

	public UIButton setButton;

	public GameObject go;

	public Renderer[] bgRenderers;

	private Transform goTransform;

	private BuildZoneObject currentZone;

	private Transform zoneTransform;

	private Camera mainCamera;

	public void UpdateTeam(BuildZoneObject zoneObj, MPTeam team)
	{
		if (!(zoneObj != currentZone))
		{
		}
	}

	public void SetZone(BuildZoneObject zoneObj)
	{
		currentZone = zoneObj;
		zoneTransform = currentZone.transform;
		if (!go.activeSelf)
		{
			go.SetActive(true);
		}
	}

	public void Cancel(BuildZoneObject zoneObj)
	{
		if (!(zoneObj != currentZone) && go.activeSelf)
		{
			go.SetActive(false);
		}
	}

	protected void Awake()
	{
		setButton.Click += OnSet;
		go.SetActive(false);
		mainCamera = Camera.main;
		goTransform = go.transform;
		Instance = this;
	}

	private bool SetDisabled()
	{
		if (!PlayerData.hasLocalPlayer || StatMaster.levelSimulating || StatMaster.waitingForServerResponse)
		{
			return true;
		}
		PlayerData localPlayer = PlayerData.localPlayer;
		return !localPlayer.isSpectator && localPlayer.machine.isSimulating;
	}

	protected void LateUpdate()
	{
		if (!go.activeSelf)
		{
			return;
		}
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		if ((instance.hudOccluding && instance.hudHit.collider != null && !instance.hudHit.collider.transform.IsChildOf(base.transform)) || SetDisabled())
		{
			go.SetActive(false);
		}
		Vector3 position = zoneTransform.position;
		float num = Vector3.Dot(mainCamera.transform.forward, position - mainCamera.transform.position);
		bool flag = false;
		if (num > 0f)
		{
			Vector2 vector = mainCamera.WorldToScreenPoint(position);
			if (vector.x > 0f && vector.x < (float)Screen.width && vector.y > 0f && vector.y < (float)Screen.height)
			{
				Vector3 vector2 = hudCamera.ScreenToWorldPoint(vector);
				goTransform.position = new Vector3(vector2.x, vector2.y, goTransform.position.z);
			}
		}
	}

	private void OnSet()
	{
		if (!PlayerData.hasLocalPlayer || SetDisabled())
		{
			return;
		}
		NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
		PlayerData localPlayer = PlayerData.localPlayer;
		byte[] identifierBytes = currentZone.GetIdentifierBytes();
		localPlayer.wantSpectator = false;
		if (localPlayer.isSpectator)
		{
			if (!instance.PlayersLimited)
			{
				byte[] array = new byte[2 + LevelEntity.ID_LENGTH];
				array[0] = ((!localPlayer.isSpectator) ? ((byte)1) : ((byte)0));
				array[1] = 1;
				Buffer.BlockCopy(identifierBytes, 0, array, 2, identifierBytes.Length);
				StatMaster.SetSimulationState(SimulationState.SwitchingToBuildMode);
				instance.SetLoadingText(LocalisationManager.GetTranslation((!localPlayer.wantSpectator) ? 2952 : 2954));
				StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.SpectatorToggle, true);
				instance.SendServerMessage(RPCMessageType.ToggleSpectator, array);
			}
			else
			{
				string text = string.Format(LocalisationManager.GetTranslation(3334));
				SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(text, 3f);
			}
		}
		else
		{
			instance.SetLoadingText(LocalisationManager.GetTranslation(2952));
			StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.SetSpawnZone, true);
			instance.SendServerMessage(RPCMessageType.SetSpawnZone, identifierBytes);
		}
	}

	public void Toggle(bool toggle)
	{
		base.enabled = toggle;
		if (!toggle)
		{
			go.SetActive(false);
		}
	}
}
