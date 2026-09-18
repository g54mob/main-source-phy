using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationSpriteActivator : MonoBehaviour
{
	[SerializeField]
	private Sprite defaultSprite;

	[SerializeField]
	private List<LocalizedSprite> localizedSpritesList;

	private Image image;

	private SpriteRenderer spriteRenderer;

	private LocalizationSystem ls;

	private Image connctedImage
	{
		get
		{
			if (image == null)
			{
				image = GetComponent<Image>();
			}
			return image;
		}
	}

	private SpriteRenderer ConnectedSR
	{
		get
		{
			if (spriteRenderer == null)
			{
				spriteRenderer = GetComponent<SpriteRenderer>();
			}
			return spriteRenderer;
		}
	}

	private LocalizationSystem localizationSystem
	{
		get
		{
			if (ls == null)
			{
				ls = Singleton<LocalizationSystem>.Instance;
			}
			return ls;
		}
	}

	private void OnEnable()
	{
		localizationSystem.OnLanguageChange += UpdateImage;
		UpdateImage();
	}

	private void OnDisable()
	{
		localizationSystem.OnLanguageChange -= UpdateImage;
	}

	public void UpdateImage()
	{
		Languages currentLanguage = localizationSystem.GetCurrentLanguage();
		foreach (LocalizedSprite localizedSprites in localizedSpritesList)
		{
			if (localizedSprites.language == currentLanguage)
			{
				SetSprite(localizedSprites.sprite);
				return;
			}
		}
		SetSprite(defaultSprite);
	}

	private void SetSprite(Sprite sprite)
	{
		if (connctedImage != null)
		{
			connctedImage.sprite = sprite;
		}
		if (ConnectedSR != null)
		{
			ConnectedSR.sprite = sprite;
		}
	}
}
