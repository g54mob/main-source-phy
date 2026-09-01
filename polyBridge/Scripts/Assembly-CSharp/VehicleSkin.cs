using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "VehicleSkin", menuName = "Game/VehicleSkin", order = 11)]
public class VehicleSkin : ScriptableObject
{
	public string m_VehicleAddressableName;

	public string m_DisplayNameLocID;

	public Texture m_Texture;

	public Sprite m_Icon;

	[ColorUsage(true, true)]
	public Color m_FlagColor;

	public Color m_UIColor;

	[NonSerialized]
	public string m_ID;

	[NonSerialized]
	public string m_PathToTexture;

	[NonSerialized]
	public bool m_IsMod;

	[NonSerialized]
	public int m_RefCount;

	public Color GetColorForUI()
	{
		if (m_UIColor.r == 0f && m_UIColor.g == 0f && m_UIColor.b == 0f)
		{
			return m_FlagColor;
		}
		return m_UIColor;
	}

	public void DoOnEnable()
	{
		m_ID = m_VehicleAddressableName + "_" + m_DisplayNameLocID;
	}

	public void DoOnDestroy()
	{
		if (m_IsMod && m_Texture != null)
		{
			UnityEngine.Object.Destroy(m_Texture);
			m_Texture = null;
		}
		VehicleSkins.Remove(this);
	}
}
