using System;
using Localisation;
using UnityEngine;

public class SkinFileBrowserSlot : FileBrowserSlot
{
	public Action<SkinFileBrowserSlot> PaintClicked;

	public Action<SkinFileBrowserSlot> SelectClicked;

	public Action<SkinFileBrowserSlot> ModifyClicked;

	[SerializeField]
	private Transform iconTransform;

	[SerializeField]
	private MeshFilter iconMeshFilter;

	[SerializeField]
	private MeshRenderer iconMeshRenderer;

	[SerializeField]
	private SkinFileSlotPlateau plateau;

	[SerializeField]
	private UIButton paintButton;

	[SerializeField]
	private UIButton selectButton;

	[SerializeField]
	private UIButton modifyButton;

	[SerializeField]
	private GameObject dlcMissingIcon;

	private bool isQuiting;

	private bool skinRegistered;

	private BlockSkinLoader.SkinPack skinPack;

	private BlockSkinLoader.SkinPack.Skin skin;

	private BlockButtonControl button;

	public BlockSkinLoader.SkinPack SkinPack
	{
		get
		{
			return skinPack;
		}
	}

	protected override void SetIsFolder(bool isFolder)
	{
	}

	public override void Initialize(FileBrowserView view, IVirtualObject virtualObject, WorkshopType workshopType)
	{
		base.Initialize(view, virtualObject, workshopType);
		paintButton.Click += PaintButtonClick;
		selectButton.Click += SelectButtonClick;
		modifyButton.Click += ModifyButtonClick;
		if (!virtualObject.IsFolder)
		{
			LocalSkinFile localSkinFile = virtualObject as LocalSkinFile;
			skinPack = localSkinFile.SkinPack;
			InitializeSkinPack();
		}
		IWorkshopItem workshopItem = virtualObject as IWorkshopItem;
		if (workshopItem != null && !workshopItem.AreDlcRequirementsMet)
		{
			dlcMissingIcon.SetActive(true);
		}
		else
		{
			dlcMissingIcon.SetActive(false);
		}
	}

	private void ModifyButtonClick()
	{
		if (ModifyClicked != null)
		{
			ModifyClicked(this);
		}
	}

	private void SelectButtonClick()
	{
		if (SelectClicked != null)
		{
			SelectClicked(this);
		}
	}

	private void PaintButtonClick()
	{
		if (PaintClicked != null)
		{
			PaintClicked(this);
		}
	}

	private void InitializeSkinPack()
	{
		string fileSuffix = string.Empty;
		if (skinPack != null)
		{
			fileSuffix = ((skinPack.type != PackType.Official) ? LocalisationManager.GetTranslation(2942) : LocalisationManager.GetTranslation(2942));
		}
		else
		{
			Debug.LogError("Trying to make slot for null pack");
		}
		plateau.Setup(skinPack.type);
		SetFileSuffix(fileSuffix);
		if (skinPack.type == PackType.Official)
		{
			DisableModifyButton();
		}
		if (!skinPack.hasValidSkins || skinPack.id == "3dprint")
		{
			selectButton.gameObject.SetActive(false);
			selectButton.GetComponent<Tooltip>().tooltipParent.gameObject.SetActive(false);
		}
		paintButton.gameObject.SetActive(skinPack.hasValidSkins);
		skin = skinPack.FindAvailableSkin();
		if (skin != null && !skin._isInvalidSkin)
		{
			UpdateSkin();
		}
		else
		{
			iconMeshFilter.mesh = null;
		}
	}

	protected override void SetThumbnailPath(IVirtualObject virtualObject)
	{
	}

	private void DisableModifyButton()
	{
		Transform parent = paintButton.transform.parent;
		Transform parent2 = selectButton.transform.parent;
		Transform parent3 = modifyButton.transform.parent;
		parent.position = new Vector3(parent.position.x, parent2.position.y + 0.04f, parent.position.z);
		parent2.position = new Vector3(parent2.position.x, parent3.position.y + 0.04f, parent2.position.z);
		modifyButton.gameObject.SetActive(false);
		modifyButton.GetComponent<Tooltip>().tooltipParent.gameObject.SetActive(false);
	}

	private void UpdateSkin()
	{
		if (!skin._isInvalidSkin)
		{
			BlockButtonControl buttonIcon = skin.prefab.GetButtonIcon();
			if (buttonIcon == null)
			{
				Debug.LogError("Making slot for pack " + skin.prefab.name + " but it has no Button to based off of");
				return;
			}
			iconMeshFilter.mesh = skin.mesh;
			iconMeshRenderer.material.mainTexture = skin.texture;
			button = buttonIcon;
			SetIconToMatch(button.Alignment);
			CorrectScaleForOutlierSkinSizes();
		}
	}

	private void OnSkinsModified(BlockSkinLoader.SModifier skinModifier)
	{
		if (skinRegistered && skin == skinModifier)
		{
			UpdateSkin();
		}
	}

	protected void SetIconToMatch(FauxTransform trans)
	{
		Vector3 localPosition = trans.localPosition;
		localPosition.z = 0f;
		iconTransform.localPosition = localPosition;
		iconTransform.localRotation = trans.localRotation;
		iconTransform.localScale = trans.localScale;
	}

	protected void CorrectScaleForOutlierSkinSizes()
	{
		Vector3 size = iconMeshRenderer.bounds.size;
		float magnitude = new Vector3(size.x, size.y, 0f).magnitude;
		float num = 1.9f * button.targetMag;
		if (magnitude != 0f && Mathf.Abs(num - magnitude) > 0.6f * num)
		{
			float num2 = num / magnitude;
			iconTransform.localScale *= num2;
		}
	}

	private void OnEnable()
	{
		if (skin != null)
		{
			skin.Register(this);
			skinRegistered = true;
		}
		BlockSkinLoader.SkinModified += OnSkinsModified;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		if (!isQuiting)
		{
			if (skin != null)
			{
				skinRegistered = false;
				skin.Unregister(this);
			}
			BlockSkinLoader.SkinModified -= OnSkinsModified;
		}
	}

	private void OnApplicationQuit()
	{
		isQuiting = true;
	}
}
