using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRandomSelector : MonoBehaviour
{
	[SerializeField]
	private bool randomFlipX;

	[SerializeField]
	private bool randomFlipY;

	[SerializeField]
	private List<Sprite> spritesList;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		if (spritesList.Count > 0)
		{
			spriteRenderer.sprite = spritesList[Random.Range(0, spritesList.Count)];
		}
		if (randomFlipX)
		{
			spriteRenderer.flipX = Random.value > 0.5f;
		}
		if (randomFlipY)
		{
			spriteRenderer.flipY = Random.value > 0.5f;
		}
	}
}
