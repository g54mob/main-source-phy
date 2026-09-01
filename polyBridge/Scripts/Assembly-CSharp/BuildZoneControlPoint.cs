using System;
using UnityEngine;

public class BuildZoneControlPoint : MonoBehaviour
{
	public SpriteRenderer m_SpriteRenderer;

	[NonSerialized]
	public BuildZoneControlPointRestriction m_Restriction;

	[NonSerialized]
	public BuildZoneRectHandleType m_RectHandleType;

	public void SetColor(Color color)
	{
		m_SpriteRenderer.color = color;
	}
}
