using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "DecorStub", menuName = "Game/DecorStub", order = 3)]
public class DecorStub : ScriptableObject
{
	public DecorCatgory m_Category;

	public string m_DisplayNameLocID;

	public Sprite m_Sprite;

	public string m_PrefabAddress;

	public string m_ModId;
}
