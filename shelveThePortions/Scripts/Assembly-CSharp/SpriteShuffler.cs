using System.Collections.Generic;
using UnityEngine;

public class SpriteShuffler : MonoBehaviour
{
	private int index = -1;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		AnimateFadingSprite();
	}

	private void AnimateFadingSprite()
	{
		spriteRenderer.sprite = GetNextSprite();
		AlphaSystem.Alphalizer(spriteRenderer, 1f, TweenDuration.Medium, IgnoreTimeScale: false, delegate
		{
			AlphaSystem.Alphalizer(spriteRenderer, 0f, TweenDuration.Medium, IgnoreTimeScale: false, AnimateFadingSprite);
		});
	}

	private Sprite GetNextSprite()
	{
		index++;
		if (index >= sprites.Count)
		{
			index = 0;
		}
		return sprites[index];
	}
}
