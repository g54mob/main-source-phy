using Localisation;
using UnityEngine;

public class LevelMachineEntryButton : LevelMachineEntry
{
	public DynamicText machineName;

	public DynamicText blockCount;

	public Renderer buttonRenderer;

	public Material black;

	public Material red;

	public int index;

	public bool changeTextColor;

	public Color pickColor;

	public Color blockCountColor;

	private MachineInfo machineInfo;

	private NetworkHUD hud;

	private LevelSettings.LevelMachine levelMachine;

	public LevelSettings.LevelMachine LevelMachine
	{
		get
		{
			return levelMachine;
		}
		set
		{
			levelMachine = value;
			machineInfo = levelMachine.GetInfo();
			machineName.SetText(machineInfo.Name);
			string text = string.Format(LocalisationManager.GetTranslation(3292), machineInfo.Blocks.Count);
			blockCount.SetText(text);
			thumbnailCode.Initialize(levelMachine.thumbBytes, false);
			buttonRenderer.material = black;
			if (changeTextColor)
			{
				blockCount.color = blockCountColor;
			}
		}
	}

	public void Init(int i, NetworkHUD nHud)
	{
		index = i;
		hud = nHud;
	}

	public override void OnCursorOver()
	{
		buttonRenderer.material = red;
		if (changeTextColor)
		{
			blockCount.color = pickColor;
		}
		ReferenceMaster.SetDynamicText(blockCount, LocalisationManager.GetTranslation(2961));
		base.OnCursorOver();
	}

	private void OnCursorExit()
	{
		buttonRenderer.material = black;
		if (changeTextColor)
		{
			blockCount.color = blockCountColor;
		}
		string text = string.Format(LocalisationManager.GetTranslation(3292), machineInfo.Blocks.Count);
		ReferenceMaster.SetDynamicText(blockCount, text);
	}

	public override void OnClicked()
	{
		if (PlayerData.hasLocalPlayer && !StatMaster.waitingForServerResponse && !PlayerData.localPlayer.isSpectator)
		{
			NetworkAuxAddPiece.Instance.PickAllowedMachine(index);
			hud.CloseAllowedMachines();
		}
	}
}
