using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CustomShapeTexture", menuName = "Game/CustomShapeTexture", order = 10)]
public class CustomShapeTexture : ScriptableObject
{
	public string m_ID;

	public string m_DisplayNameLocID;

	public Texture m_Texture;
}
