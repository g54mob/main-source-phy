using Localisation;
using Selectors;
using UnityEngine;

public class BlockLimitEntry : MonoBehaviour, ILocalisationAware
{
	public FilterRendererPair block;

	public TextMesh nameField;

	public UIButtonExtended banButton;

	public ValueHolderDefaulting maxValue;

	public GameObject noLimitIcon;

	public GameObject bannedIcon;

	protected BlockPrefab prefab;

	protected int value;

	protected bool noLimit = true;

	protected bool banned;

	private LevelSettingsScreen settingsScreen;

	private LevelEditor levelEditor;

	private bool wasSelected;

	public void SetupEntry(BlockPrefab p, LevelSettingsScreen settings, LevelEditor l, int stencil)
	{
		prefab = p;
		levelEditor = l;
		settingsScreen = settings;
		BlockButtonControl buttonIcon = p.GetButtonIcon();
		if (buttonIcon == null)
		{
			Debug.LogError("tried creating limit entry for incomplete prefab: " + p.name);
			return;
		}
		SetIconToMatch(block.renderer.transform, buttonIcon.Alignment);
		SetIconToVisual(block, stencil);
		CorrectScaleForOutlierSkinSizes(block.renderer);
		SetBlockEntryName();
		maxValue.ResetDelegate();
		banButton.ResetDelegates();
		OnUpdate(levelEditor.Settings.GetBlockLimit((BlockType)p.ID));
		maxValue.ValueChanged += OnMaxChanged;
		banButton.Down += ToggleBan;
	}

	private void SetBlockEntryName()
	{
		if (!(nameField == null) && prefab != null)
		{
			nameField.text = ReferenceMaster.TranslateBlockName((BlockType)prefab.ID).ToUpper();
		}
	}

	protected void SetIconToMatch(Transform ico, FauxTransform trans)
	{
		Vector3 localPosition = trans.localPosition;
		localPosition.z = -1f;
		ico.localPosition = localPosition;
		ico.localRotation = trans.localRotation;
		ico.localScale = trans.localScale;
	}

	protected void SetIconToVisual(FilterRendererPair ico, int stencil)
	{
		BlockSkinLoader.SkinPack.Skin defaultSkin = prefab.DefaultSkin;
		if (prefab.VisualController.CanChangeTexture)
		{
			Color color = defaultSkin.material.color;
			Material material = ico.renderer.material;
			material.color = new Color(color.r, color.g, color.b, material.color.a);
			material.mainTexture = defaultSkin.texture;
			if (defaultSkin.material.shader == PrefabMaster.BlockPrefabs[57].DefaultSkin.material.shader)
			{
				Color color2 = defaultSkin.material.GetColor("_Emission");
				material.color = (material.color + color2) / 2f;
				material.color += color2;
			}
			if (defaultSkin.material.HasProperty("_RimColor"))
			{
				material.SetColor("_RimColor", defaultSkin.material.GetColor("_RimColor"));
			}
			if (defaultSkin.material.HasProperty("_RimPower"))
			{
				material.SetFloat("_RimPower", defaultSkin.material.GetFloat("_RimPower"));
			}
			if (material.HasProperty("_StencilVal"))
			{
				material.SetInt("_StencilVal", stencil);
			}
		}
		if (prefab.VisualController.CanChangeMesh)
		{
			ico.filter.sharedMesh = defaultSkin.mesh;
			CorrectScaleForOutlierSkinSizes(ico.renderer);
		}
		else
		{
			ico.filter.sharedMesh = prefab.GetButtonIcon().myMeshFilter.sharedMesh;
		}
	}

	protected void CorrectScaleForOutlierSkinSizes(Renderer target)
	{
		Vector3 size = target.bounds.size;
		float magnitude = new Vector3(size.x, size.y, 0f).magnitude;
		float targetMag = prefab.GetButtonIcon().targetMag;
		if (magnitude != 0f && Mathf.Abs(targetMag - magnitude) > 0.6f * targetMag)
		{
			float num = targetMag / magnitude;
			target.transform.localScale *= num;
		}
	}

	public void OnMaxChanged(float value)
	{
		OnUpdate(value);
		settingsScreen.OnUpdateSettings();
	}

	public void SetBan(bool value)
	{
		banned = value;
		OnUpdate((!banned) ? (-1) : 0);
	}

	protected void ToggleBan()
	{
		OnUpdate(banButton.IsBGActive ? (-1) : 0);
		settingsScreen.OnUpdateSettings();
	}

	protected void OnUpdate(float value)
	{
		if (value != 0f)
		{
			this.value = (int)value;
		}
		levelEditor.Settings.SetBlockLimit((BlockType)prefab.ID, (int)value);
		noLimit = value < 0f;
		banned = value == 0f;
		maxValue.SetText(value);
		bannedIcon.SetActive(banned);
		noLimitIcon.SetActive(!banned && noLimit);
		maxValue.Hide(banned);
		banButton.ToggleBG(banned);
	}

	protected void Update()
	{
		if (maxValue.IsFocused && !wasSelected)
		{
			noLimitIcon.SetActive(false);
			bannedIcon.SetActive(false);
			wasSelected = true;
		}
		else if (!maxValue.IsFocused && wasSelected)
		{
			noLimitIcon.SetActive(!banned && noLimit);
			bannedIcon.SetActive(banned);
			wasSelected = false;
		}
	}

	public void OnLocalisationChange()
	{
		SetBlockEntryName();
	}
}
