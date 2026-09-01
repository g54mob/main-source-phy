using Localisation;
using Selectors;
using UnityEngine;

public class ReloadEventDisplay : MachineEventDisplay, ILocalisationAware
{
	public DynamicText modeText;

	public UIButton prevMode;

	public UIButton nextMode;

	public UIButton modify;

	public GameObject[] modifyModes;

	public ValueHolder valueHolder;

	public UIButton changeAmmoButton;

	public MeshRenderer ammoIconRen;

	[SerializeField]
	protected Texture2D allAmmoIcon;

	[SerializeField]
	protected Texture2D arrowIcon;

	[SerializeField]
	protected Texture2D cannonBallIcon;

	[SerializeField]
	protected Texture2D fireIcon;

	[SerializeField]
	protected Texture2D randomIcon;

	private EventContainer.ReloadMachineEvent reloadEvent;

	protected override void Awake()
	{
		base.Awake();
		valueHolder.ValueChanged += OnReloadAmount;
		changeAmmoButton.Down += CycleAmmoType;
		modify.Down += ToggleModifier;
		prevMode.Down += ToggleMode;
		nextMode.Down += ToggleMode;
	}

	public override void UpdateVisual()
	{
		reloadEvent = currentEvent.eventData as EventContainer.ReloadMachineEvent;
		if (reloadEvent != null)
		{
			base.UpdateVisual();
			valueHolder.SetText(reloadEvent.reloadValue);
			UpdateAmmoIcon();
			UpdateModifierIcon();
			UpdateModeText();
		}
	}

	public void CycleAmmoType()
	{
		if (reloadEvent == null)
		{
			reloadEvent = currentEvent.eventData as EventContainer.ReloadMachineEvent;
			if (reloadEvent == null)
			{
				return;
			}
		}
		reloadEvent.ammoType = ((reloadEvent.ammoType != ReloadAmmoType.Random) ? ((reloadEvent.ammoType <= ReloadAmmoType.Fire) ? (reloadEvent.ammoType + 1) : ReloadAmmoType.Random) : ReloadAmmoType.All);
		UpdateAmmoIcon();
		OnEditEvent();
	}

	public void UpdateAmmoIcon()
	{
		Texture2D mainTexture = allAmmoIcon;
		switch (reloadEvent.ammoType)
		{
		case ReloadAmmoType.Cannon:
			mainTexture = cannonBallIcon;
			break;
		case ReloadAmmoType.Fire:
			mainTexture = fireIcon;
			break;
		case ReloadAmmoType.Arrow:
			mainTexture = arrowIcon;
			break;
		case ReloadAmmoType.Random:
			mainTexture = randomIcon;
			break;
		default:
			Debug.LogWarning("tried setting ammo to unknown state: " + reloadEvent.ammoType);
			break;
		case ReloadAmmoType.All:
			break;
		}
		ammoIconRen.material.mainTexture = mainTexture;
	}

	public void ToggleModifier()
	{
		reloadEvent.setAmmo = !reloadEvent.setAmmo;
		UpdateModifierIcon();
	}

	public void UpdateModifierIcon()
	{
		modifyModes[0].SetActive(!reloadEvent.setAmmo);
		modifyModes[1].SetActive(reloadEvent.setAmmo);
	}

	public void ToggleMode()
	{
		reloadEvent.eachBlock = !reloadEvent.eachBlock;
		UpdateModeText();
		OnEditEvent();
	}

	public void OnLocalisationChange()
	{
		UpdateModeText();
	}

	public void UpdateModeText()
	{
		ReferenceMaster.SetDynamicText(modeText, (!reloadEvent.eachBlock) ? LocalisationManager.GetTranslation(927) : LocalisationManager.GetTranslation(3180));
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		valueHolder.ValueChanged -= OnReloadAmount;
	}

	private void OnReloadAmount(float reloadAmount)
	{
		reloadEvent.reloadValue = reloadAmount;
		OnEditEvent();
	}

	protected override void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight + 0.15f + (float)pickWidgets.Count * pickSpacer, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}
}
