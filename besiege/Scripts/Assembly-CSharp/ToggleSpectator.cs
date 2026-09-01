using Localisation;
using UnityEngine;

public class ToggleSpectator : ClickBehaviour
{
	public Material redMaterial;

	public Material darkMaterial;

	public Renderer bgRenderer;

	private void OnEnable()
	{
		Set();
	}

	public override void OnClicked()
	{
		if (StatMaster.isMP && !StatMaster.waitingForServerResponse)
		{
			SendToggleSpectatorMessage();
		}
	}

	private void SendToggleSpectatorMessage()
	{
		PlayerData localPlayer = PlayerData.localPlayer;
		localPlayer.wantSpectator = !localPlayer.isSpectator;
		byte[] messageData = new byte[2]
		{
			(byte)(localPlayer.wantSpectator ? 1u : 0u),
			0
		};
		StatMaster.SetSimulationState((!localPlayer.wantSpectator) ? SimulationState.SwitchingToBuildMode : SimulationState.SwitchingToSpectator);
		NetworkAuxAddPiece instance = NetworkAuxAddPiece.Instance;
		instance.SetLoadingText(LocalisationManager.GetTranslation((!localPlayer.wantSpectator) ? 2952 : 2954));
		StatMaster.WaitForServerResponse(StatMaster.ServerResponseType.SpectatorToggle, true);
		instance.SendServerMessage(RPCMessageType.ToggleSpectator, messageData);
	}

	public void Set()
	{
		if (PlayerData.localPlayer.isSpectator)
		{
			bgRenderer.material = redMaterial;
		}
		else
		{
			bgRenderer.material = darkMaterial;
		}
	}
}
