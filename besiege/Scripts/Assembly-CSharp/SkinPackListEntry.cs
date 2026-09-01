using UnityEngine;

public class SkinPackListEntry : MonoBehaviour
{
	public TextMesh text;

	public UIButton tickButton;

	public Renderer tickIcon;

	public SimpleUIButton dlcMissingButton;

	public Color invalidSkinColor;

	protected SetupSkinPackWindow windowControl;

	protected BlockSkinLoader.SkinPack.Skin mySkin;

	public void Setup(SetupSkinPackWindow wc, BlockSkinLoader.SkinPack.Skin skin)
	{
		windowControl = wc;
		if (skin != mySkin)
		{
			if (mySkin != null)
			{
				mySkin.Unregister(this);
			}
			if (skin != null)
			{
				mySkin = skin.Register(this);
			}
			else
			{
				mySkin = null;
			}
		}
		text.text = ReferenceMaster.TranslateBlockName((BlockType)mySkin.ID);
		if (skin._isInvalidSkin)
		{
			Transform parent = tickIcon.transform.parent;
			parent.gameObject.SetActive(false);
			Renderer component = text.GetComponent<MeshRenderer>();
			component.material.color = invalidSkinColor;
		}
		else
		{
			tickIcon.enabled = mySkin.enabled;
			tickButton.Click += ToggleSkin;
			tickButton.MouseEnter += OnMouseEnter;
		}
	}

	private void OnDestroy()
	{
		if (mySkin != null)
		{
			mySkin.Unregister(this);
			mySkin = null;
		}
	}

	private void ToggleSkin()
	{
		if (mySkin.enabled)
		{
			BlockSkinLoader.DisableBlockSkin(mySkin);
		}
		else
		{
			BlockSkinLoader.EnableBlockSkin(mySkin);
		}
		tickIcon.enabled = mySkin.enabled;
	}

	private void OnMouseEnter()
	{
		windowControl.SetPreviewerTo(mySkin);
		windowControl.SetHighlight(base.transform);
	}
}
