using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "VehicleStub", menuName = "Game/VehicleStub", order = 4)]
public class VehicleStub : ScriptableObject
{
	public string m_DisplayNameLocID;

	public Sprite m_Icon;

	public string m_PrefabAddress;

	public string m_ModId;

	public VehicleSkin[] m_Skins;

	public float m_Mass;

	public bool m_UGC;

	public bool m_CanBeAvatar;

	public bool m_ExcludeFromRandomSpawning;

	public bool m_Legacy;
}
