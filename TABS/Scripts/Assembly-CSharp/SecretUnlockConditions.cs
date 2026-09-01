using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TABS/Secret Unlock Conditions", fileName = "SecretUnlockConditions")]
public class SecretUnlockConditions : ScriptableObject
{
	public SecretUnlockCondition[] m_unlockConditions;

	public List<SecretUnlockCondition> CheckUnlockCondition(List<string> unlockedBefore)
	{
		List<SecretUnlockCondition> list = new List<SecretUnlockCondition>();
		if (m_unlockConditions == null)
		{
			return list;
		}
		SecretUnlockCondition[] unlockConditions = m_unlockConditions;
		for (int i = 0; i < unlockConditions.Length; i++)
		{
			SecretUnlockCondition item = unlockConditions[i];
			if (unlockedBefore.Contains(item.m_unlock))
			{
				continue;
			}
			bool flag = true;
			string[] conditionUnlocks = item.m_conditionUnlocks;
			foreach (string item2 in conditionUnlocks)
			{
				if (!unlockedBefore.Contains(item2))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list.Add(item);
			}
		}
		return list;
	}
}
