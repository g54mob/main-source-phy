using Landfall.TABS;
using UnityEngine;

public class DestructibleObject : Damagable
{
	[SerializeField]
	private float m_minimumDamage;

	[SerializeField]
	private float m_damageThreshold = 100f;

	[SerializeField]
	private GameObject[] m_objectsToActivate;

	[SerializeField]
	private GameObject[] m_objectsToDisable;

	[SerializeField]
	private float m_forceExplosion = 10f;

	private float m_currentDamage;

	public override void TakeDamage(float damage, Vector3 direction, Unit unit, DamageType damageType = DamageType.Default)
	{
		m_currentDamage += damage;
		if (m_currentDamage >= m_damageThreshold && damage > m_minimumDamage)
		{
			Destruct(direction * m_forceExplosion);
		}
	}

	private void Destruct(Vector3 force)
	{
		for (int i = 0; i < m_objectsToActivate.Length; i++)
		{
			m_objectsToActivate[i].SetActive(value: true);
			Rigidbody[] componentsInChildren = m_objectsToActivate[i].GetComponentsInChildren<Rigidbody>();
			if (componentsInChildren != null)
			{
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					Vector3 force2 = force * Random.Range(-1f, 1f);
					componentsInChildren[j].AddForce(force2, ForceMode.Impulse);
				}
			}
		}
		for (int k = 0; k < m_objectsToDisable.Length; k++)
		{
			m_objectsToDisable[k].SetActive(value: false);
		}
	}

	private void Reset()
	{
	}
}
