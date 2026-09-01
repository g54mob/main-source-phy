using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ZVehicleStub", menuName = "Game/ZVehicleStub", order = 5)]
public class ZedAxisVehicleStub : ScriptableObject
{
	public ZedAxisVehicleType m_Type;

	public string m_DisplayNameLocID;

	public string m_PrefabAddress;

	public Sprite m_Icon;

	public float m_Mass;

	public bool m_UGC;

	public bool m_Legacy;
}
