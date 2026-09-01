using System;
using UnityEngine;

[Serializable]
public struct SecretUnlockCondition
{
	public string[] m_conditionUnlocks;

	public string m_unlock;

	public string m_unlockDescription;

	public Sprite m_unlockImage;
}
