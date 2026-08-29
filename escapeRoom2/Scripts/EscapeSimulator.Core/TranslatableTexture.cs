using System;
using UnityEngine;

public class TranslatableTexture : MonoBehaviour
{
	[NonSerialized]
	public Texture[] originalTextures;

	[NonSerialized]
	public Texture[] originalNormalMaps;

	[NonSerialized]
	public Texture[] originalEmissionMaps;

	[NonSerialized]
	public Texture[] originalMetallicMaps;

	[NonSerialized]
	public bool[] originalEmissionOn;

	public bool[] shouldTranslate;

	[NonSerialized]
	public Sprite originalSprite;

	[NonSerialized]
	public Texture2D originalSpriteMaterialTexture;

	[HideInInspector]
	public string[] ids;

	public bool clearNormalmapOnNonEnglish;

	public bool clearEmissionmapOnNonEnglish = true;

	public bool clearMetallicmapOnNonEnglish;

	public bool useBaseAsEmission;

	public bool useCustomTextureKeyword;

	public string customTextureKeyword = "_MainTexture";

	public bool getShouldTranslate(int index)
	{
		if (shouldTranslate != null && shouldTranslate.Length != 0)
		{
			return shouldTranslate[index];
		}
		return true;
	}
}
