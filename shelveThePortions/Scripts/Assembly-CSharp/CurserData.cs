using System;
using UnityEngine;

[Serializable]
public class CurserData
{
	public CurserTypes curserType;

	public Texture2D cursorTexture;

	public Vector2 hotSpot = Vector2.zero;
}
