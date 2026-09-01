using BesiegeDlc;
using InternalModding.LevelEntities;
using Localisation;
using UnityEngine;

public class LevelPrefabButton : UIButton, ILocalisationAware
{
	[HideInInspector]
	public bool active;

	[HideInInspector]
	public LevelPrefab prefab;

	public Tooltip tooltip;

	public MeshRenderer icon;

	public MeshRenderer BG;

	public AudioSource clickAudio;

	public MeshRenderer tooltipIcon;

	public TextMesh nameText;

	public MeshRenderer destructableIcon;

	public MeshRenderer burnableIcon;

	public MeshRenderer damagerIcon;

	public GameObject dlcWater;

	public Color attributeEnabled;

	public Color attributeDisabled;

	protected ScaleOnMouseOver iconScaler;

	protected NullaBool greyed = NullaBool.Null;

	public void Start()
	{
		if (prefab != null)
		{
			UpdateBG();
		}
	}

	public void SetUp(LevelPrefab levelPrefab)
	{
		if (levelPrefab == null || SingleInstanceFindOnly<EntityLoader>.Instance.IsHiddenEntity(levelPrefab.ID))
		{
			levelPrefab = null;
		}
		prefab = levelPrefab;
		if (iconScaler == null)
		{
			iconScaler = base.gameObject.GetComponent<ScaleOnMouseOver>();
		}
		if (prefab == null)
		{
			active = false;
			buttonCollider.enabled = false;
			icon.gameObject.SetActive(false);
			BG.gameObject.SetActive(false);
			tooltip.tooltipParent.gameObject.SetActive(false);
			if ((bool)dlcWater)
			{
				dlcWater.SetActive(false);
			}
			return;
		}
		active = true;
		buttonCollider.enabled = true;
		icon.gameObject.SetActive(true);
		if ((bool)dlcWater)
		{
			DlcManager.DlcType dlcType;
			if (DlcManager.Instance.GetDlcType(levelPrefab.ID, out dlcType))
			{
				DlcManager.DlcType dlcType2 = dlcType;
				if (dlcType2 == DlcManager.DlcType.Water)
				{
					dlcWater.SetActive(true);
				}
			}
			else
			{
				dlcWater.SetActive(false);
			}
		}
		Texture mainTexture = ((!(prefab.icon != null)) ? ReferenceMaster.Instance.missingPrefabThumbnail : prefab.icon);
		if (SingleInstance<StatMaster>.Instance.LowViolence && prefab.tencentIcon != null)
		{
			mainTexture = prefab.tencentIcon;
		}
		icon.material.mainTexture = mainTexture;
		tooltipIcon.material.mainTexture = mainTexture;
		tooltip.renOrgColors[destructableIcon] = ((!prefab.destructable) ? attributeDisabled : attributeEnabled);
		tooltip.renOrgColors[burnableIcon] = ((!prefab.inflammable) ? attributeDisabled : attributeEnabled);
		tooltip.renOrgColors[damagerIcon] = ((!prefab.damager) ? attributeDisabled : attributeEnabled);
		nameText.text = LocalisationManager.GetTranslation(prefab.LocalisationID);
		UpdateBG(true);
	}

	protected override bool _InvokeOnDown()
	{
		if (!base._InvokeOnDown())
		{
			return false;
		}
		if (StatMaster.levelSimulating)
		{
			return false;
		}
		if (!active || !prefab)
		{
			return false;
		}
		if (clickAudio != null)
		{
			clickAudio.Stop();
			clickAudio.Play();
		}
		LevelEditor instance = LevelEditor.Instance;
		if (instance != null)
		{
			instance.SetPrefab(prefab);
		}
		return true;
	}

	public void UpdateBG()
	{
		UpdateBG(false);
	}

	public void UpdateBG(bool force)
	{
		bool flag = StatMaster.SelectedLevelPrefab == prefab && prefab != null;
		if (BG.gameObject.activeSelf != flag)
		{
			BG.gameObject.SetActive(flag);
		}
		Color color = icon.material.GetColor("_TintColor");
		if (StatMaster.levelSimulating)
		{
			if (greyed != NullaBool.True || force)
			{
				tooltip.tooltipParent.gameObject.SetActive(false);
				if (iconScaler != null)
				{
					iconScaler.enabled = false;
				}
				icon.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 0.2f));
				ToggleButton(false);
				greyed = NullaBool.True;
			}
		}
		else if (greyed != NullaBool.False || force)
		{
			tooltip.SetAllRenderersOff();
			if (iconScaler != null)
			{
				iconScaler.enabled = true;
			}
			icon.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, 1f));
			ToggleButton(true);
			greyed = NullaBool.False;
		}
	}

	public void OnLocalisationChange()
	{
		if (prefab != null)
		{
			nameText.text = LocalisationManager.GetTranslation(prefab.LocalisationID);
		}
	}
}
