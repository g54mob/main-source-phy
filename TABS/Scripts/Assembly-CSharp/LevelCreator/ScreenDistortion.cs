using UnityEngine;

namespace LevelCreator
{
	public class ScreenDistortion : MonoBehaviour
	{
		private static ScreenDistortion m_instance;

		[SerializeField]
		private Material m_material;

		private float m_timer = 1f;

		private float m_duration = 1f;

		private float m_speed;

		public static ScreenDistortion Instance
		{
			get
			{
				if (m_instance == null)
				{
					m_instance = DMEditor.Instance.playerCamera.GetComponent<ScreenDistortion>();
				}
				return m_instance;
			}
		}

		public static void RadialDistort(float speed = 1f)
		{
			Instance.m_timer = 0f;
			Instance.m_speed = speed;
		}

		private void Update()
		{
			if (m_timer <= m_duration)
			{
				m_timer += Time.deltaTime * m_speed;
				m_material.SetFloat("_Timer", m_timer);
			}
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (m_timer > m_duration || m_material == null)
			{
				Graphics.Blit(src, dest);
			}
			else
			{
				Graphics.Blit(src, dest, m_material);
			}
		}
	}
}
