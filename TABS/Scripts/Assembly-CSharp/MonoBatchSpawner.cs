using UnityEngine;

public class MonoBatchSpawner : MonoBehaviour
{
	public GameObject m_batchedGoToSpawn;

	public int m_batchedCount;

	public GameObject m_goToSpawn;

	public int m_goCount;

	private void Start()
	{
		for (int i = 0; i < m_batchedCount; i++)
		{
			Object.Instantiate(m_batchedGoToSpawn);
		}
		for (int j = 0; j < m_goCount; j++)
		{
			Object.Instantiate(m_goToSpawn);
		}
	}
}
