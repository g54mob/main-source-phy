using System.Linq;
using UnityEngine;

public class SetupSkinPackWindow : MonoBehaviour
{
	public GameObject skinListEntryPrefab;

	public TextMesh nameText;

	public MeshFilter previewerFilter;

	public MeshRenderer previewerRenderer;

	public SkinFileSlotPlateau skinPlateau;

	public UIButton paint;

	public UIButton select;

	public UIButton weGameUploadButton;

	public UIButton steamUploadButton;

	public UIButton modioUploadButton;

	public GameObject bin;

	public UIButton deleteSkin;

	public GameObject highlightObject;

	public Transform slotParent;

	public FileBrowserView fileBrowserView;

	private UIButton uploadButton;

	protected BlockSkinLoader.SkinPack myPack;

	protected BlockSkinLoader.SkinPack.Skin focusedSkin;

	private void Awake()
	{
		paint.Click += PaintMachineWithPack;
		select.Click += SetAllButtonsToPack;
		steamUploadButton.gameObject.SetActive(false);
		weGameUploadButton.gameObject.SetActive(false);
		modioUploadButton.gameObject.SetActive(false);
		weGameUploadButton.GetComponent<Tooltip>().tooltipParent.gameObject.SetActive(false);
		uploadButton = steamUploadButton;
		uploadButton.Click += UploadPackToPlatform;
		deleteSkin.Click += DeletePack;
		BlockSkinLoader.SkinModified += UpdatePreviewer;
	}

	private void OnDestroy()
	{
		paint.Click -= PaintMachineWithPack;
		select.Click -= SetAllButtonsToPack;
		uploadButton.Click -= UploadPackToPlatform;
		deleteSkin.Click -= DeletePack;
		BlockSkinLoader.SkinModified -= UpdatePreviewer;
	}

	public void SetupWindow(BlockSkinLoader.SkinPack pack)
	{
		ToggleObjectOnClick component = bin.GetComponent<ToggleObjectOnClick>();
		if (bin != null && component != null && component.objToDisable != null)
		{
			component.objToDisable.gameObject.SetActive(false);
		}
		base.gameObject.SetActive(true);
		myPack = pack;
		SetupTitle(myPack);
		GenerateListOfBlocks(myPack);
		UpdatePackButtons();
		if (myPack.skins.Any())
		{
			SetPreviewerTo(myPack.skins[0]);
		}
		else
		{
			SetPreviewerTo(null);
		}
	}

	private void SetupTitle(BlockSkinLoader.SkinPack pack)
	{
		string text = pack.name.ToUpper();
		if (text.EndsWith("SKIN"))
		{
			text = text.Replace("SKIN", string.Empty);
		}
		else if (text.EndsWith("SKIN PACK"))
		{
			text = text.Replace("SKIN PACK", string.Empty);
		}
		else if (text.EndsWith("SKINPACK"))
		{
			text = text.Replace("SKINPACK", string.Empty);
		}
		else if (text.EndsWith("PACK"))
		{
			text = text.Replace("PACK", string.Empty);
		}
		else if (text.EndsWith("PACKAGE"))
		{
			text = text.Replace("PACKAGE", string.Empty);
		}
		nameText.text = " : " + text;
	}

	private void GenerateListOfBlocks(BlockSkinLoader.SkinPack pack)
	{
		ClearListOfBlocks();
		ResetScrollbar();
		for (int i = 0; i < pack.skins.Count; i++)
		{
			SpawnEntry(i, pack);
		}
	}

	private void SpawnEntry(int id, BlockSkinLoader.SkinPack pack)
	{
		Vector3 position = slotParent.position + Vector3.down * 0.175f + Vector3.down * 0.35f * id;
		Transform transform = (Object.Instantiate(skinListEntryPrefab, position, Quaternion.identity) as GameObject).transform;
		transform.parent = slotParent;
		transform.GetComponent<SkinPackListEntry>().Setup(this, myPack.skins[id % myPack.skins.Count]);
	}

	private void ResetScrollbar()
	{
	}

	public void SetHighlight(Transform listEntryTransform)
	{
		Vector3 position = listEntryTransform.position;
		Vector3 position2 = new Vector3(highlightObject.transform.position.x, position.y + 0.05f, highlightObject.transform.position.z);
		highlightObject.transform.position = position2;
		Renderer component = highlightObject.GetComponent<Renderer>();
		if (!(component == null))
		{
			Bounds bounds = component.bounds;
			Bounds contentBounds = BoundsHelper.GetContentBounds(listEntryTransform);
			float num = (contentBounds.size.x + 0.3f) / bounds.size.x;
			Vector3 localScale = component.transform.localScale;
			localScale.x *= num;
			component.transform.localScale = localScale;
			position2.x = contentBounds.center.x;
			highlightObject.transform.position = position2;
		}
	}

	private void ClearListOfBlocks()
	{
		for (int i = 0; i < slotParent.childCount; i++)
		{
			if (slotParent.GetChild(i).gameObject != highlightObject)
			{
				Object.Destroy(slotParent.GetChild(i).gameObject);
			}
		}
	}

	private void UpdatePreviewer(BlockSkinLoader.SModifier m)
	{
		if (m != null && m is BlockSkinLoader.SkinPack)
		{
			BlockSkinLoader.SkinPack skinPack = m as BlockSkinLoader.SkinPack;
			if (skinPack != null && focusedSkin != null && skinPack.deleted && skinPack == focusedSkin.pack)
			{
				focusedSkin = null;
				Close();
				return;
			}
		}
		SetPreviewerTo(focusedSkin);
	}

	private void UpdatePackButtons()
	{
		skinPlateau.Setup(myPack.type);
		paint.gameObject.SetActive(false);
		select.gameObject.SetActive(false);
		bin.SetActive(false);
		bin.GetComponent<Tooltip>().Start();
		if (myPack.type != PackType.Official)
		{
			bin.gameObject.SetActive(true);
		}
		if (myPack.hasValidSkins)
		{
			paint.gameObject.SetActive(true);
			select.gameObject.SetActive(true);
		}
		bool flag = ReferenceMaster.IsPlatformReady();
		bool active = !myPack.hasInvalidSkins && myPack.type == PackType.Local && flag;
		uploadButton.gameObject.SetActive(active);
	}

	public void SetPreviewerTo(BlockSkinLoader.SkinPack.Skin skin)
	{
		if (skin == null || skin._isInvalidSkin)
		{
			focusedSkin = null;
			previewerFilter.sharedMesh = null;
			return;
		}
		focusedSkin = skin;
		if (!skin.prefab.HasButtonIcons())
		{
			return;
		}
		BlockButtonControl blockButtonControl = null;
		for (int i = 0; i < skin.prefab.ButtonIconCount(); i++)
		{
			blockButtonControl = skin.prefab.GetButtonIcon(i);
			if (blockButtonControl != null)
			{
				break;
			}
			if (i + 1 == skin.prefab.ButtonIconCount())
			{
				return;
			}
		}
		previewerRenderer.transform.localPosition = new Vector3(blockButtonControl.myRenderer.transform.localPosition.x, blockButtonControl.myRenderer.transform.localPosition.y, 0f);
		previewerRenderer.transform.localRotation = blockButtonControl.myRenderer.transform.localRotation;
		previewerRenderer.transform.localScale = blockButtonControl.myRenderer.transform.localScale;
		if (skin.prefab.CanChangeMesh)
		{
			previewerFilter.sharedMesh = skin.mesh;
		}
		else if (blockButtonControl.myMeshFilter != null)
		{
			previewerFilter.sharedMesh = blockButtonControl.myMeshFilter.sharedMesh;
		}
		previewerRenderer.material.mainTexture = skin.texture;
	}

	private void PaintMachineWithPack()
	{
		base.transform.root.GetComponent<AudioSource>().Play();
		if (myPack != null)
		{
			BlockSkinLoader.SetAllBlocksToPack(myPack, Machine.Active());
		}
		Close();
	}

	private void SetAllButtonsToPack()
	{
		base.transform.root.GetComponent<AudioSource>().Play();
		if (myPack != null)
		{
			BlockSkinLoader.SetAllPrefabsToPack(myPack);
		}
		Close();
	}

	private void UploadPackToPlatform()
	{
		if (myPack != null)
		{
			fileBrowserView.UploadSkin(myPack);
			Close();
		}
	}

	private void DeletePack()
	{
		if (myPack != null)
		{
			myPack.Delete();
		}
	}

	private int FindAvailableIcon(BlockSkinLoader.SkinPack pack)
	{
		if (pack.skins.Count <= 0)
		{
			return -1;
		}
		int num = -1;
		int i;
		for (i = 0; i < pack.skins.Count; i++)
		{
			if (pack.skins[i].enabled)
			{
				if (pack.skins[i].prefab.ID != 0)
				{
					break;
				}
				num = i;
			}
			else if (i + 1 == pack.skins.Count)
			{
				if (num == -1)
				{
					return -1;
				}
				i = num;
			}
		}
		return i;
	}

	public void Close()
	{
		base.gameObject.SetActive(false);
	}
}
