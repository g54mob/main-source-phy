using System.Collections;
using UnityEngine;

namespace LevelCreator
{
	public class VulcanoHazard : LandscapeHazard
	{
		[SerializeField]
		private string m_brushKey = string.Empty;

		private Brush m_impactBrush;

		private Brush m_materialBrush;

		[SerializeField]
		private int m_addItterations = 10;

		[SerializeField]
		private float m_itterationDelay = 0.05f;

		[Space]
		[SerializeField]
		private float m_projectilesHeightOffset = 7f;

		[SerializeField]
		private GameObject m_eruptionEffect;

		private void Start()
		{
			m_impactBrush = brushTable.GetBrush(m_brushKey);
			for (int i = 0; i < m_impactBrush.Size.z; i++)
			{
				for (int j = 0; j < m_impactBrush.Size.y; j++)
				{
					for (int k = 0; k < m_impactBrush.Size.x; k++)
					{
						m_impactBrush.Field[i, j, k] *= 10f;
					}
				}
			}
			int size = Mathf.Max(m_impactBrush.Size.x * Level.MaterialChunk.noOfCells.x / Level.VoxelChunk.noOfCells.x, m_impactBrush.Size.y * Level.MaterialChunk.noOfCells.y / Level.VoxelChunk.noOfCells.y);
			int height = m_impactBrush.Size.z * Level.MaterialChunk.noOfCells.z / Level.VoxelChunk.noOfCells.z;
			m_materialBrush = VolumeBrushes.CreateCylinderBrush(size, height, 0f, 0f);
			StartCoroutine(ExecuteHazard());
		}

		private IEnumerator ExecuteHazard()
		{
			Shake();
			for (int i = 0; i < m_addItterations; i++)
			{
				float lerpIntensity = 0.35f;
				DMEditor.Instance.VolumeRootObject.AddVolume(base.transform.position, m_impactBrush, lerpIntensity);
				DMEditor.Instance.VolumeRootObject.LerpMaterial(base.transform.position, m_materialBrush, 1f / 3f, lerpIntensity);
				yield return new WaitForSeconds(m_itterationDelay);
			}
			DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(base.transform.position, m_impactBrush));
			if (TakeSnapshot)
			{
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
			}
			Object.Instantiate(m_eruptionEffect, base.transform.position + Vector3.up * m_projectilesHeightOffset * 0.8f, Quaternion.Euler(-90f, 0f, 0f));
		}
	}
}
