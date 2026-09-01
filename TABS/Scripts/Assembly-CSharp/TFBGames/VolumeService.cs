using UnityEngine;

namespace TFBGames
{
	public class VolumeService : ServicePrefab
	{
		private const float DefaultVolume = 1f;

		private const float DefaultVolumePS4 = 2.5f;

		private float m_globalVolume;

		public float GlobalVolume => m_globalVolume;

		private void Awake()
		{
			m_globalVolume = 1f;
			AudioListener.volume = m_globalVolume;
		}
	}
}
