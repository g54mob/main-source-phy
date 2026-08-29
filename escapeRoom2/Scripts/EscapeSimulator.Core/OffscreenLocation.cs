using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class OffscreenLocation : MonoBehaviour
{
	[SerializeField]
	private Vector2 offscreenOffsetLocation;

	[SerializeField]
	private Vector2 alternativeOffsetLocation;

	[NonSerialized]
	public Vector2 originalAnchorPosition;

	[NonSerialized]
	public bool alternateLocation;

	private void Awake()
	{
		originalAnchorPosition = GetComponent<RectTransform>().anchoredPosition;
	}

	public Vector2 calculateLocation(bool offscreen)
	{
		Vector2 vector = originalAnchorPosition;
		if (alternateLocation)
		{
			vector += alternativeOffsetLocation;
		}
		if (!offscreen)
		{
			return vector;
		}
		return vector + offscreenOffsetLocation;
	}
}
